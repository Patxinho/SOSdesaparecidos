using SOSdesaparecidos.Models;
using SOSdesaparecidos.Services.Menu;
using SOSdesaparecidos.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.ViewModels.Main
{
    public class MainMenuViewModel : ViewModelBase
    {
        private ObservableCollection<MenuItem> _menu;
        private MenuItem _selectedMenu;

        private IMenuService _menuService;

        public MainMenuViewModel(
            IMenuService menuService)
        {
            _menuService = menuService;
        }

        public ObservableCollection<MenuItem> Menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                OnPropertyChanged();
            }
        }

        public MenuItem SelectedMenu
        {
            get { return _selectedMenu; }
            set
            {
                _selectedMenu = value;

                if (_selectedMenu.PageType != null)
                    NavigationService.NavigateToAsync(_selectedMenu.PageType);
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            Menu = _menuService.LoadMenu();

            return base.InitializeAsync(navigationData);
        }
    }
}
