using Hospital.Models;
using Hospital.Models.DAL.Abstracts;
using Hospital.Models.DAL.Repository.EfRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.DAL.Repository.EfRepository
{
    public class HospitalDB : IDisposable
    {
        HospitalContext db = new HospitalContext();

        private DoctorRepository doctorRepository;
        private PatientRepository patientRepository;
        private SpecializationRepository specializationRepository;

        public IRepository<Doctor> Doctors
        {
            get
            {
                if (doctorRepository == null)
                {
                    doctorRepository = new DoctorRepository(db);
                }
                return doctorRepository;
            }
        }

        public IRepository<Patient> Patients
        {
            get
            {
                if (patientRepository == null)
                {
                    patientRepository = new PatientRepository(db);
                }
                return patientRepository;
            }
        }

        public IRepository<Specialization> Specializations
        {
            get
            {
                if (specializationRepository == null)
                {
                    specializationRepository = new SpecializationRepository(db);
                }
                return specializationRepository;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}