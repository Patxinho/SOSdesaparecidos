using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using SOSdesaparecidos.Services.Navigation;
using SOSdesaparecidos.ViewModels.Base;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SOSdesaparecidos
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Windows)
            {
                InitNavigation();
            }
        }

        protected override async void OnStart()
        {

            base.OnStart();

            //if (Device.RuntimePlatform == Device.Windows)
           // {
                await InitNavigation();
            //  }
            MobileCenter.Start("android=0fbb897f-f6aa-4745-8470-422c9a1c5cdd;" +
                   "ios={Your iOS App secret here}",
                   typeof(Analytics), typeof(Crashes));

        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
        }
    }
}
