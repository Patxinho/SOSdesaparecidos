using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SOSdesaparecidos.Models;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace SOSdesaparecidos.Services.ParseHtml
{
    public class ParseHtmlService : IParseHtmlService
    {
        public async Task<ObservableCollection<Desaparecido>> GetMainMissing(string missing)
        {
            var Desaparecidos = new ObservableCollection<Desaparecido>();
            HtmlDocument document = new HtmlDocument();
            var stream =await new HttpClient().GetStringAsync("http://sosdesaparecidos.es/");
            document.LoadHtml(stream);
            var container = document.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "galleryStyle mb15").ToList();
            if (container != null)
            {
                foreach (var item in container)
                {
                    var title = item.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "mb15");
                    var subjectItem = item.Descendants("img").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "lazy freewall").ToList();
                    if (subjectItem != null && title.InnerText == missing)
                    {
                        switch (missing)
                        {
                            case "Menores desaparecidos":
                                foreach (var desaparecido in subjectItem)
                                {
                                    Desaparecidos.Add(new Desaparecido { Id_foto = desaparecido.Attributes["alt"].Value, Image = "http://sosdesaparecidos.es/" + desaparecido.Attributes["data-original"].Value });
                                }
                                break;
                            case "Mayores desaparecidos":
                                foreach (var desaparecido in subjectItem)
                                {
                                    Desaparecidos.Add(new Desaparecido { Id_foto = desaparecido.Attributes["alt"].Value, Image = "http://sosdesaparecidos.es/" + desaparecido.Attributes["data-original"].Value });
                                }
                                break;
                            case "Adultos desaparecidos":
                                foreach (var desaparecido in subjectItem)
                                {
                                    Desaparecidos.Add(new Desaparecido { Id_foto = desaparecido.Attributes["alt"].Value, Image = "http://sosdesaparecidos.es/" + desaparecido.Attributes["data-original"].Value });
                                }
                                break;
                            default:
                                break;
                        }
                        
                    }
                }

            }
            return Desaparecidos;
        }



        public async Task<ObservableCollection<Municipio>> GetListMissing()
        {
            var Municipios = new ObservableCollection<Municipio>();
            HtmlDocument document = new HtmlDocument();
            var stream = await new HttpClient().GetStreamAsync("http://sosdesaparecidos.es/");
            document.Load(stream, Encoding.GetEncoding("ISO-8859-1"));
            var container = document.DocumentNode.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("id") && x.Attributes["id"].Value == "subMenuLeft423");
            if (container != null)
            {
                var subjectItem = container.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "menu_lef_list").ToList();
                foreach (var item in subjectItem)
                {
                    var title = item.Descendants("a").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "submenu_left_link");
                    Municipios.Add(new Municipio { Name = title.InnerText, Url = title.Attributes["href"].Value });
                }
            }
            return Municipios;
        }


        public async Task<ObservableCollection<Desaparecido>> GetMunicipioMissing(string URL)
        {
            var Desaparecidos = new ObservableCollection<Desaparecido>();
            HtmlDocument document = new HtmlDocument();
            var stream = await new HttpClient().GetStringAsync("http://sosdesaparecidos.es"+URL);
            document.LoadHtml(stream);
            var container = document.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "galleryStyle mb15").ToList();
            if (container != null)
            {
                foreach (var item in container)
                {
                   
                    var subjectItem = item.Descendants("img").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "lazy freewall").ToList();
                    foreach (var desaparecido in subjectItem)
                    {
                        Desaparecidos.Add(new Desaparecido { Id_foto = desaparecido.Attributes["alt"].Value, Image = "http://sosdesaparecidos.es/" + desaparecido.Attributes["data-original"].Value });
                    }


                }

            }
            return Desaparecidos;
        }
    }

}
