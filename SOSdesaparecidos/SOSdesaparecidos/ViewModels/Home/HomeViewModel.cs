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

namespace SOSdesaparecidos.ViewModels.Home
{
    public class HomeViewModel : ViewModelBase
    {

        private ObservableCollection<Desaparecido> _desaparecidosMenores;
        private ObservableCollection<Desaparecido> _desaparecidosMayores;
        private ObservableCollection<Desaparecido> _desaparecidosAdultos;
        private IParseHtmlService _parseHtml;

        public HomeViewModel(IParseHtmlService parseHtml)
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
            IsBusy = false;

            //Desaparecidos = new ObservableCollection<Desaparecido>(result);
        } 
    }
}
