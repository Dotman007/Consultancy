using ConsultantPunctualityApp.DAL;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ConsultantPunctualityApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<ConsultantDB>(new DropCreateDatabaseIfModelChanges<ConsultantDB>());
            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            //ApplicationProfile.Run();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
    }
}
