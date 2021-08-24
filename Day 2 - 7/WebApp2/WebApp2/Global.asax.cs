using Autofac;
using Autofac.Integration.WebApi;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApp2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public IContainer Container { get; set; }
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new DIModuleService());
            builder.RegisterModule(new DIModuleRepository());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}
