using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.Models
{
    public class Desaparecido
    {
        public string Id { get; set; }

        public string Image { get; set; }

        [Version]
        public string AzureVersion { get; set; }
    }
}
