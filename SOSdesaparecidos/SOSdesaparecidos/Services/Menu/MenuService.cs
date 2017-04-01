using SOSdesaparecidos.Models;
using SOSdesaparecidos.ViewModels.Contact;
using SOSdesaparecidos.ViewModels.Home;
using SOSdesaparecidos.ViewModels.Main;
using SOSdesaparecidos.ViewModels.Missing;
using SOSdesaparecidos.ViewModels.News;
using System.Collections.ObjectModel;

namespace SOSdesaparecidos.Services.Menu
{
    public class MenuService : IMenuService
    {
        public ObservableCollection<MenuItem> LoadMenu()
        {
            return new ObservableCollection<MenuItem> {
                new MenuItem("Inicio", typeof(HomeViewModel)),
                new MenuItem("Noticias", typeof(NewsViewModel)),
                new MenuItem("Desaparecidos", typeof(MissingViewModel)),
                new MenuItem("Contacto", typeof(ContactViewModel))
            };
        }
    }
}
