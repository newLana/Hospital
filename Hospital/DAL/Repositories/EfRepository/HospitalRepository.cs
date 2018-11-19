using Hospital.Models;
using Hospital.Models.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital.DAL.Repositories.EfRepository
{
    public class HospitalRepository<T> : IRepository<T> where T : class
    {
        private HospitalContext db;

        public HospitalRepository(HospitalContext dbContext)
        {
            db = dbContext;
        }

        public T Create(T item)
        {
            T t = db.Set<T>().Add(item);
            db.SaveChanges();
            return t;
        }

        public void Delete(int id)
        {
            T entity = Get(id);
            db.Set<T>().Remove(entity);
            db.SaveChanges();
        }

        public T Get(int id)
        {
            return db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}