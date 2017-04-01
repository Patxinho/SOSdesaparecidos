using SOSdesaparecidos.ViewModels.Base;
using SOSdesaparecidos.ViewModels.Contact;
using SOSdesaparecidos.ViewModels.Home;
using SOSdesaparecidos.ViewModels.Main;
using SOSdesaparecidos.ViewModels.Missing;
using SOSdesaparecidos.ViewModels.News;
using SOSdesaparecidos.Views.Base;
using SOSdesaparecidos.Views.Contact;
using SOSdesaparecidos.Views.Home;
using SOSdesaparecidos.Views.Main;
using SOSdesaparecidos.Views.Missing;
using SOSdesaparecidos.Views.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SOSdesaparecidos.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(HomeViewModel), typeof(HomeView) },
            { typeof(ContactViewModel), typeof(ContactView) },
            { typeof(MissingViewModel), typeof(MissingView) },
            { typeof(NewsViewModel), typeof(NewsView) }
           
        };


        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get
            {
                return Application.Current;
            }
        }

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<MainViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return NavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return NavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return NavigateToAsync(viewModelType, null);
        }

        protected virtual async Task NavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is MainView)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is MainView)
            {
                var mainPage = CurrentApplication.MainPage as MainView;
                var navigationPage = mainPage.Detail as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    navigationPage = new CustomNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = ViewModelLocator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            page.Appearing += async (object sender, EventArgs e) =>
            {
                await viewModel.InitializeAsync(parameter);
            };

            return page;
        }
        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(ContactViewModel), typeof(ContactView));
            _mappings.Add(typeof(MissingViewModel), typeof(MissingView));
            _mappings.Add(typeof(NewsViewModel), typeof(NewsView));
            _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
            _mappings.Add(typeof(MainViewModel), typeof(MainView));
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }
    }
}
