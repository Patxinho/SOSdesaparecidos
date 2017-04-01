using SOSdesaparecidos.Models.Rss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.Services.Rss
{
    public interface IRssService
    {
        Task<List<RSSFeedItem>> GetFeedAsync(string url);
        Task<List<RSSFeedItem>> ParseFeedAsync(string rss);
    }
}

