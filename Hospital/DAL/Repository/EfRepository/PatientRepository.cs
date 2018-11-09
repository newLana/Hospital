using Hospital.Models.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace Hospital.Models.DAL.Repository.EfRepository
{
    public class PatientRepository : IRepository<Patient>
    {
        HospitalContext db;

        public PatientRepository(HospitalContext repo)
        {
            db = repo;
        }

        public void Create(Patient item)
        {
            db.Patients.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Patient patient = Get(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
        }

        public Patient Get(int id)
        {
            return db.Patients.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<Patient> GetAll()
        {
            return db.Patients.ToList();
        }

        public void Update(Patient item)
        {
            Patient patient = Get(item.Id);
            db.Entry(patient).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}