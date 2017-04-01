using HtmlAgilityPack;
using SOSdesaparecidos.Models;
using SOSdesaparecidos.ViewModels.Base;
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

        public HomeViewModel()
        {

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
            HtmlDocument document = new HtmlDocument();
            DesaparecidosMenores = new ObservableCollection<Desaparecido>();
            DesaparecidosMayores = new ObservableCollection<Desaparecido>();
            DesaparecidosAdultos = new ObservableCollection<Desaparecido>();

            var stream = new HttpClient().GetStringAsync("http://sosdesaparecidos.es/");
            document.LoadHtml(stream.Result);
            var container = document.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "galleryStyle mb15").ToList();
            if (container != null)
            {
                foreach (var item in container)
                {
                    var title = item.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "mb15");
                    var subjectItem = item.Descendants("img").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "lazy freewall").ToList();
                    if (subjectItem != null)
                    {
                        foreach (var desaparecido in subjectItem)
                        {
                            switch (title.InnerText)
                            {
                                case "Menores desaparecidos":
                                    DesaparecidosMenores.Add(new Desaparecido { Id = desaparecido.Attributes["alt"].Value, Image = "http://sosdesaparecidos.es/" + desaparecido.Attributes["data-original"].Value });
                                    break;
                                case "Mayores desaparecidos":
                                    DesaparecidosMayores.Add(new Desaparecido { Id = desaparecido.Attributes["alt"].Value, Image = "http://sosdesaparecidos.es/" + desaparecido.Attributes["data-original"].Value });
                                    break;
                                case "Adultos desaparecidos":
                                    DesaparecidosAdultos.Add(new Desaparecido { Id = desaparecido.Attributes["alt"].Value, Image = "http://sosdesaparecidos.es/" + desaparecido.Attributes["data-original"].Value });
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

               // var subjectItem = container.Where("img").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "lazy freewall").ToList();
                //if (subjectItem != null)
                //{
                //    foreach (var item in subjectItem)
                //    {
                //        DesaparecidosMenores.Add(new Desaparecido { Id = item.Attributes["alt"].Value, Image = "http://sosdesaparecidos.es/"+item.Attributes["data-original"].Value});
                //    }
                //    //Desaparecidos.Add(new Desaparecido { Id = subjectItem. Attributes["alt"].Value });
                //    //var subjectItemValue = subjectItem.Attributes["src"].Value;
                //}
            }

            //Desaparecidos = new ObservableCollection<Desaparecido>(result);
        } 
    }
}
