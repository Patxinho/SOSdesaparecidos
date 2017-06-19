using Microsoft.WindowsAzure.MobileServices.Sync;
using SOSdesaparecidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.Services.Azure
{
    public interface IMobileService
    {
        

        Task<IEnumerable<Desaparecido>> ReadItemsAsync();

        Task AddOrUpdateItemAsync(Desaparecido Item);

        Task DeleteItemAsync(Desaparecido Item);


    

        

    }
}
