using Hospital.Models.DAL.Abstracts;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Hospital.Models.DAL.Repository.EfRepository
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private HospitalContext db;

        public DoctorRepository(HospitalContext repo)
        {
            db = repo;
        }

        public void Create(Doctor item)
        {
            db.Doctors.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {            
            Doctor doctor = Get(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
        }

        public Doctor Get(int id)
        {
            return db.Doctors.First(d => d.Id == id);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return db.Doctors.ToList();
        }

        public void Update(Doctor item)
        {
            var doctor = Get(item.Id);
            db.Entry(doctor).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}