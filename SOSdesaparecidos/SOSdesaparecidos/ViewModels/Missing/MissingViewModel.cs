using SOSdesaparecidos.Models;
using SOSdesaparecidos.Services.ParseHtml;
using SOSdesaparecidos.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SOSdesaparecidos.ViewModels.Missing
{
    public class MissingViewModel : ViewModelBase
    {

        private ObservableCollection<Desaparecido> _desaparecidosMenores;
        private ObservableCollection<Municipio> _municipios;
        private IParseHtmlService _parseHtml;
        private Desaparecido _miss;
        private Municipio _municipio;
        public ICommand ListCommand => new 
            Command(async () => await NavigationService.NavigateToAsync<MissingListViewModel>());

       
        public MissingViewModel(IParseHtmlService parseHtml)
        {
            _parseHtml = parseHtml;

        }

        public ObservableCollection<Desaparecido> DesaparecidosMenores
        {
            get { return _desaparecidosMenores; }
            set
            {
                _desaparecidosMenores = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Municipio> Municipios
        {
            get { return _municipios; }
            set
            {
                _municipios = value;
                OnPropertyChanged();
            }
        }

        public Desaparecido Miss
        {
            get { return _miss; }
            set
            {
                _miss = value;

                if (_miss != null)
                {
                    NavigationService.NavigateToAsync<Missing.MissingDetailViewModel>(_miss);
                }
            }
        }

        public Municipio Muni
        {
            get { return _municipio; }
            set
            {
                _municipio = value;

               
            }
        }

        public ICommand UpdateCommand => new Command(async () => await UpdateAsync());

        public override async Task InitializeAsync(object navigationData)
        {

            DesaparecidosMenores = new ObservableCollection<Desaparecido>();
            Municipios = new ObservableCollection<Municipio>();

            IsBusy = true;
            //DesaparecidosMenores = await _parseHtml.GetMainMissing("Menores desaparecidos");
            Municipios = await _parseHtml.GetListMissing();
            IsBusy = false;

            //Desaparecidos = new ObservableCollection<Desaparecido>(result);
        }

        public async Task UpdateAsync()
        {
            IsBusy = true;
            DesaparecidosMenores = new ObservableCollection<Desaparecido>();
            DesaparecidosMenores = await _parseHtml.GetMunicipioMissing(_municipio.Url);
            IsBusy = false;
        }

    }
}
