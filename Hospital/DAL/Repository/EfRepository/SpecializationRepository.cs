using Hospital.Models;
using Hospital.Models.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital.DAL.Repository.EfRepository
{
    public class SpecializationRepository : IRepository<Specialization>
    {
        private HospitalContext db;

        public SpecializationRepository(HospitalContext repo)
        {
            db = repo;
        }

        public void Create(Specialization item)
        {
            db.Specializations.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Specialization spec = Get(id);
            db.Specializations.Remove(spec);
            db.SaveChanges();
        }

        public Specialization Get(int id)
        {
            return db.Specializations.First(s => s.Id == id);
        }

        public IEnumerable<Specialization> GetAll()
        {
            return db.Specializations.ToList();
        }

        public void Update(Specialization item)
        {
            var spec = Get(item.Id);
            db.Entry(spec).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}