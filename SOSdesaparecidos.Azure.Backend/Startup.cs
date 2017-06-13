using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SOSdesaparecidos.Azure.Backend.Startup))]

namespace SOSdesaparecidos.Azure.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}