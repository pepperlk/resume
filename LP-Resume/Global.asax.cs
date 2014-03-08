using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using LP_Resume.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LP_Resume
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AzureCDNManager.Init(c =>
            {
                c.AzureStoregeConnectionString("DefaultEndpointsProtocol=http;AccountName=wyocode;AccountKey=yyBiCconwBI+X2QgeDX61XVeKmX5ExVafkehlLkqJtQi/dshNr+wQ6ELXbAg26Y5HLwG3cRWgved5Ycs6bfvew==;");
                // c.Endpoint("http://wyocode.blob.core.windows.net/");
                c.Folder("~/App");
                c.Folder("~/Content");
                //c.External("");

            });


            var builder = new ContainerBuilder();
            
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            AutofacBootstrap.Init(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
