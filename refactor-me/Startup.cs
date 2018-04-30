using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using refactor_me.Domain.Repository;
using refactor_me.Models;
using refactor_me.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly:
    OwinStartup(typeof(refactor_me.Startup))
]
namespace refactor_me
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {


            HttpConfiguration config = new HttpConfiguration();
           // Helpers.NewConnection();
            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);


            config.EnsureInitialized();

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //ToImimpent Authentication to autorise api endpoints

        }
    }
}