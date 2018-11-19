using System.Collections.Generic;

namespace Hospital.Models.DAL.Abstracts
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        T Create(T item);

        void Update(T item);

        void Delete(int id);
    }
}
