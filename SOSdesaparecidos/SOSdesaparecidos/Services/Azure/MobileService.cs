using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using SOSdesaparecidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SOSdesaparecidos.Services.Azure
{
    public class MobileService : Azure.IMobileService
    {
        private MobileServiceClient _client;
        private IMobileServiceSyncTable<Desaparecido> _desaparecidoItemTable;

        public MobileService()
        {
            _client = new MobileServiceClient(GlobalSettings.AzureUrl);
        }

        private async Task InitializeAsync()
        {
            if (_desaparecidoItemTable != null)
                return;

            // Inicialización de SQLite local
            var store = new MobileServiceSQLiteStore(GlobalSettings.Database);
            store.DefineTable<Desaparecido>();

            await _client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            _desaparecidoItemTable = _client.GetSyncTable<Desaparecido>();

            // Limpiar registros offline.
            await _desaparecidoItemTable.PurgeAsync(true);
        }

        public async Task<IEnumerable<Desaparecido>> ReadItemsAsync()
        {
            await InitializeAsync();
            await SynchronizeAsync();
            return await _desaparecidoItemTable.ToEnumerableAsync();
        }

        public async Task AddOrUpdateItemAsync(Desaparecido desaparecido)
        {
            await InitializeAsync();

            if (string.IsNullOrEmpty(desaparecido.Id))
            {
                await _desaparecidoItemTable.InsertAsync(desaparecido);
            }
            else
            {
                await _desaparecidoItemTable.UpdateAsync(desaparecido);
            }

            await SynchronizeAsync();
        }

        public async Task DeleteItemAsync(Desaparecido desaparecido)
        {
            await InitializeAsync();

            await _desaparecidoItemTable.DeleteAsync(desaparecido);

            await SynchronizeAsync();
        }

        private async Task SynchronizeAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return;

            try
            {
                // Subir cambios a la base de datos remota
                await _client.SyncContext.PushAsync();

                // El primer parámetro es el nombre de la query utilizada intermanente por el client SDK para implementar sync.
                // Utiliza uno diferente por cada query en la App
                await _desaparecidoItemTable.PullAsync($"all{nameof(Desaparecido)}", _desaparecidoItemTable.CreateQuery());
            }
            catch (MobileServicePushFailedException ex)
            {
                if (ex.PushResult.Status == MobileServicePushStatus.CancelledByAuthenticationError)
                {
                    await LoginAsync();
                    await SynchronizeAsync();
                    return;
                }

                if (ex.PushResult != null)
                    foreach (var result in ex.PushResult.Errors)
                        await ResolveErrorAsync(result);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await LoginAsync();
                    await SynchronizeAsync();
                    return;
                }

                throw;
            }
        }

        public async Task LoginAsync()
        {
            const string userIdKey = ":UserId";
            const string tokenKey = ":Token";

            if (CrossSecureStorage.Current.HasKey(userIdKey)
                && CrossSecureStorage.Current.HasKey(tokenKey))
            {
                string userId = CrossSecureStorage.Current.GetValue(userIdKey);
                string token = CrossSecureStorage.Current.GetValue(tokenKey);

                _client.CurrentUser = new MobileServiceUser(userId)
                {
                    MobileServiceAuthenticationToken = token
                };

                return;
            }

            var authService = DependencyService.Get<IAuthService>();
            await authService.LoginAsync(_client, MobileServiceAuthenticationProvider.Twitter);

            var user = _client.CurrentUser;

            if (user != null)
            {
                CrossSecureStorage.Current.SetValue(userIdKey, user.UserId);
                CrossSecureStorage.Current.SetValue(tokenKey, user.MobileServiceAuthenticationToken);
            }
        }

        private async Task ResolveErrorAsync(MobileServiceTableOperationError result)
        {
            // Ignoramos si no podemos validar ambas partes.
            if (result.Result == null || result.Item == null)
                return;

            var serverItem = result.Result.ToObject<Desaparecido>();
            var localItem = result.Item.ToObject<Desaparecido>();

            if (serverItem.Image == localItem.Image
                && serverItem.Id == localItem.Id)
            {
                // Los elementos sin iguales, ignoramos el conflicto
                await result.CancelAndDiscardItemAsync();
            }
            else
            {
                // Para nosotros, gana el cliente
               // localItem.AzureVersion = serverItem.AzureVersion;
                await result.UpdateOperationAsync(JObject.FromObject(localItem));
            }
        }
    }
}
