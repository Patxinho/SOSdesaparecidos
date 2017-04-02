using SOSdesaparecidos.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.ViewModels.Contact
{
    public class ContactViewModel : ViewModelBase
    {
        public const string ContactUrl = "http://sosdesaparecidos.es/contacto";

        public string ContactPageURL
        {
            get { return ContactUrl; }
        }
    }
}
