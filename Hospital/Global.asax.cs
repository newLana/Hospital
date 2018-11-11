using Hospital.App_Start;
using Hospital.DAL.DependencyInjection;
using Hospital.DAL.Repository.EfRepository;
using Ninject;
using Ninject.Web.Mvc;
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

            var kernel = new StandardKernel(new NinjectRegistration());
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
