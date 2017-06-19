using SOSdesaparecidos.Models;
using SOSdesaparecidos.Services.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SOSdesaparecidos.ViewModels.Missing
{
    public class MissingDetailViewModel : ViewModels.Base.ViewModelBase
    {

        private Desaparecido _desaparecido;
        private IMobileService _mobileService;


        public MissingDetailViewModel(IMobileService MobileService)
        {
            _mobileService = MobileService;
        }
        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        public Desaparecido Desapa
        {
            get { return _desaparecido; }
            set
            {
                _desaparecido = value;
                OnPropertyChanged();
            }
        }
        public override async Task InitializeAsync(object navigationData)
        {
            Desapa = (navigationData as Desaparecido);
            

        }

        private async Task SaveAsync()
        {


            await _mobileService.AddOrUpdateItemAsync(Desapa);

            //IEnumerable<Desaparecido> prueba = await _mobileService.ReadItemsAsync();

            //Desapa = prueba.FirstOrDefault();
                

            
        }
    }
}
