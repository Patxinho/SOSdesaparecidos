
using SOSdesaparecidos.Models.Rss;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using SOSdesaparecidos.Helpers;

namespace SOSdesaparecidos.Services.Rss
{
    public class RssService : IRssService
    {
        public async Task<List<RSSFeedItem>> GetFeedAsync(string url)
        {
            var httpClient = new HttpClient();
            var feed = url;
            var responseString = await httpClient.GetStringAsync(feed);

            var rssItems = new List<RSSFeedItem>();
            var items = await ParseFeedAsync(responseString);
            foreach (var item in items)
            {
                rssItems.Add(item);
            }

            return rssItems;
        }

        public async Task<List<RSSFeedItem>> ParseFeedAsync(string rss)
        {
            return await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(rss);
                var id = 0;
                return (from item in xdoc.Descendants("item")
                        select new RSSFeedItem
                        {
                            Title = (string)item.Element("title"),
                            Description = (string)item.Element("description"),
                            //Category = (string)item.Element("category"),
                            Link = (string)item.Element("link"),
                            PublishDate = (string)item.Element("pubDate"),
                            //Author = (string)item.Element("author"),
                            Image = GetItemImage(item),
                            Id = id++
                        }).ToList();
            });
        }

        private static string GetItemImage(XElement item)
        {
            if (!string.IsNullOrEmpty(item.GetSafeElementString("image")))
            {
                return item.GetSafeElementString("image");
            }

            return item.GetImage();
        }
    }
}
