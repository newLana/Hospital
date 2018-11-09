using Hospital.App_Start;
using Hospital.DAL.Repository.EfRepository;
using Hospital.Models;
using Hospital.Models.DAL.Abstracts;
using Hospital.Models.DAL.Repository.EfRepository;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hospital
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new HospitalDbInitializer());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);            
        }
    }
}
