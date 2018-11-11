using Hospital.DAL.Abstracts;
using Hospital.Models;
using Hospital.Models.DAL.Abstracts;
using Ninject;

namespace Hospital.DAL.Repository.EfRepository
{
    public class HospitalDB : IDataBaseUnit
    {
        [Inject]
        public IRepository<Doctor> Doctors { get; set; }

        [Inject]
        public IRepository<Patient> Patients { get; set; }

        [Inject]
        public IRepository<Specialization> Specializations { get; set; }
    }
}