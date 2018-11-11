using Hospital.DAL.Abstracts;
using Hospital.DAL.Repositories.EfRepository;
using Hospital.DAL.Repository.EfRepository;
using Hospital.Models;
using Hospital.Models.DAL.Abstracts;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Web.Mvc;

namespace Hospital.DAL.DependencyInjection
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Unbind<ModelValidatorProvider>();

            Bind<IDataBaseUnit>().To<HospitalDB>().InRequestScope();

            Bind(typeof(IRepository<>)).To(typeof(HospitalRepository<>));

            Bind<HospitalContext>().ToSelf().InRequestScope();
        }
    }
}