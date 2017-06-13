using SOSdesaparecidos.ViewModels.Base;
using SOSdesaparecidos.ViewModels.Home;
using SOSdesaparecidos.ViewModels.News;
using System.Threading.Tasks;

namespace SOSdesaparecidos.ViewModels.Main
{
    public class MainViewModel : ViewModelBase
    {
        private MainMenuViewModel _menuViewModel;

        public MainViewModel(MainMenuViewModel menuViewModel)
        {
            _menuViewModel = menuViewModel;
        }

        public MainMenuViewModel MenuViewModel
        {
            get
            {
                return _menuViewModel;
            }

            set
            {
                _menuViewModel = value;
                OnPropertyChanged();
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            return Task.WhenAll
                (
                    _menuViewModel.InitializeAsync(navigationData),
                    NavigationService.NavigateToAsync<NewsViewModel>()
                );
        }
    }
}
