using System.Data.Entity;

namespace Hospital.Models
{
    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Specialization> Specializations { get; set; }
    }
}