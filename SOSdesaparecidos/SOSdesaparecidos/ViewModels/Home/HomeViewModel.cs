using HtmlAgilityPack;
using SOSdesaparecidos.Models;
using SOSdesaparecidos.ViewModels.Base;
using SOSdesaparecidos.Services.ParseHtml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SOSdesaparecidos.Services.Azure;

namespace SOSdesaparecidos.ViewModels.Home
{
    public class HomeViewModel : ViewModelBase
    {

        private ObservableCollection<Desaparecido> _desaparecidosMenores;
        private ObservableCollection<Desaparecido> _desaparecidosMayores;
        private ObservableCollection<Desaparecido> _desaparecidosAdultos;
        private IParseHtmlService _parseHtml;
        private IMobileService _mobileService;

        public HomeViewModel(IParseHtmlService parseHtml, IMobileService mobileService)
        {
            _parseHtml = parseHtml;
            _mobileService = mobileService;
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
        public ObservableCollection<Desaparecido> DesaparecidosMayores
        {
            get { return _desaparecidosMayores; }
            set
            {
                _desaparecidosMayores = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Desaparecido> DesaparecidosAdultos
        {
            get { return _desaparecidosAdultos; }
            set
            {
                _desaparecidosAdultos = value;
                OnPropertyChanged();
            }
        }
        public override async Task InitializeAsync(object navigationData)
        {
            
            DesaparecidosMenores = new ObservableCollection<Desaparecido>();
            DesaparecidosMayores = new ObservableCollection<Desaparecido>();
            DesaparecidosAdultos = new ObservableCollection<Desaparecido>();

            IsBusy = true;
            DesaparecidosMenores = await _parseHtml.GetMainMissing("Menores desaparecidos");
            DesaparecidosMayores = await _parseHtml.GetMainMissing("Mayores desaparecidos");
            DesaparecidosAdultos = await _parseHtml.GetMainMissing("Adultos desaparecidos");
            var result = await _mobileService.ReadItemsAsync();
            IsBusy = false;

            //Desaparecidos = new ObservableCollection<Desaparecido>(result);
        } 
    }
}
