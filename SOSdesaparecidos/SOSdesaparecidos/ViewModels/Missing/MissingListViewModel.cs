using SOSdesaparecidos.Models;
using SOSdesaparecidos.Services.Azure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.ViewModels.Missing
{
    public class MissingListViewModel : ViewModels.Base.ViewModelBase
    {

        private ObservableCollection<Desaparecido> _desaparecido;
        private IMobileService _mobileService;

        public MissingListViewModel(IMobileService MobileService)
        {
            _mobileService = MobileService;
        }
        public ObservableCollection<Desaparecido> Desapa
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
           
            var result = await _mobileService.ReadItemsAsync();
            Desapa = new ObservableCollection<Desaparecido>(result);
            
        }
    }
}
