using SOSdesaparecidos.Models;
using SOSdesaparecidos.Services.ParseHtml;
using SOSdesaparecidos.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.ViewModels.Missing
{
    public class MissingViewModel : ViewModelBase
    {

        private ObservableCollection<Desaparecido> _desaparecidosMenores;
        private IList<String> _municipios;
        private IParseHtmlService _parseHtml;
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

        public IList<String> Municipios
        {
            get { return _municipios; }
            set
            {
                _municipios = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {

            DesaparecidosMenores = new ObservableCollection<Desaparecido>();
            Municipios = new List<String>();

            IsBusy = true;
            DesaparecidosMenores = await _parseHtml.GetMainMissing("Menores desaparecidos");
            Municipios = await _parseHtml.GetListMissing();
            IsBusy = false;

            //Desaparecidos = new ObservableCollection<Desaparecido>(result);
        }
    }
}
