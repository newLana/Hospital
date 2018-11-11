using Hospital.Models;
using Hospital.Models.DAL.Abstracts;

namespace Hospital.DAL.Abstracts
{
    public interface IDataBaseUnit
    {
        IRepository<Patient> Patients { get; set; }

        IRepository<Doctor> Doctors { get; set; }

        IRepository<Specialization> Specializations { get; set; }
    }
}
