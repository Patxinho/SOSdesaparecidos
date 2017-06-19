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
        private IMobileServiceTable<Desaparecido> _desaparecidoItemTable;

        public MobileServiceClient Client
        {
            get { return _client; }
        }

        public MobileService()
        {
            if (_client != null)
                return;
            _client = new MobileServiceClient(GlobalSettings.AzureUrl);
            _desaparecidoItemTable = _client.GetTable<Desaparecido>();
        }

        public Task<IEnumerable<Desaparecido>> ReadItemsAsync()
        {
            return _desaparecidoItemTable.ReadAsync();
        }

        public async Task AddOrUpdateItemAsync(Desaparecido Item)
        {
            if (string.IsNullOrEmpty(Item.Id))
            {
                await _desaparecidoItemTable.InsertAsync(Item);
            }
            else
            {
                await _desaparecidoItemTable.UpdateAsync(Item);
            }
        }

        public async Task DeleteItemAsync(Desaparecido Item)
        {
            await _desaparecidoItemTable.DeleteAsync(Item);
        }

    }
}
