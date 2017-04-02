using SOSdesaparecidos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.Services.ParseHtml
{
    public interface IParseHtmlService
    {
        Task<ObservableCollection<Desaparecido>> GetMainMissing(string missing);

        Task<IList<string>> GetListMissing();
    }
}
