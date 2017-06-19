using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using SOSdesaparecidos.Azure.Backend.DataObjects;
using SOSdesaparecidos.Azure.Backend.Models;
using Owin;

namespace SOSdesaparecidos.Azure.Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
           

            List<Desaparecido> todoDes= new List<Desaparecido>
            {
                new Desaparecido { Id = Guid.NewGuid().ToString(), Image = "First item",  Id_foto = "www.sosdesaparecidos.com/user_images/thumb428498be6925ac2b774733ac0558043e552843.jpg" },
                new Desaparecido { Id = Guid.NewGuid().ToString(), Image = "Second item", Id_foto = "www.sosdesaparecidos.com/user_images/thumb428498be6925ac2b774733ac0558043e552843.jpg" }
            };


            foreach (Desaparecido todoItem in todoDes)
            {
                context.Set<Desaparecido>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}

