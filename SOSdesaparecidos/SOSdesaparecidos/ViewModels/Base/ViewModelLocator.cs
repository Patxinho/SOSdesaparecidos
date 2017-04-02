using Microsoft.Practices.Unity;
using SOSdesaparecidos.Services.Menu;
using SOSdesaparecidos.Services.Navigation;
using SOSdesaparecidos.Services.ParseHtml;
using SOSdesaparecidos.Services.Rss;
using SOSdesaparecidos.ViewModels.Contact;
using SOSdesaparecidos.ViewModels.Home;
using SOSdesaparecidos.ViewModels.Main;
using SOSdesaparecidos.ViewModels.Missing;
using SOSdesaparecidos.ViewModels.News;
using System;

namespace SOSdesaparecidos.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _unityContainer;
        private static readonly ViewModelLocator _instance = new ViewModelLocator();

        public static ViewModelLocator Instance
        {
            get
            {
                return _instance;
            }
        }

        public ViewModelLocator()
        {
            _unityContainer = new UnityContainer();

            // ViewModels
            _unityContainer.RegisterType<MainMenuViewModel>();
            _unityContainer.RegisterType<MainViewModel>();
            _unityContainer.RegisterType<NewsViewModel>();
            _unityContainer.RegisterType<HomeViewModel>();
            _unityContainer.RegisterType<MissingViewModel>();
            _unityContainer.RegisterType<ContactViewModel>();
           

            // Services     
            _unityContainer.RegisterType<INavigationService, NavigationService>();
            _unityContainer.RegisterType<IMenuService, MenuService>();
            _unityContainer.RegisterType<IRssService, RssService>();
            _unityContainer.RegisterType<IParseHtmlService, ParseHtmlService>();
            //_unityContainer.RegisterType<ICourseService, CourseService>();
            //_unityContainer.RegisterType<ISqliteService, SqliteService>();
            //_unityContainer.RegisterType<IOpenUrlService, OpenUrlService>();
            //_unityContainer.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        public void Register<T>(T instance)
        {
            _unityContainer.RegisterInstance<T>(instance);
        }

        public void Register<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>();
        }

        public void RegisterSingleton<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
        }
    }
}
