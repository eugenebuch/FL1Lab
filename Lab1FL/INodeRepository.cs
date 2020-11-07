using System.Collections.Generic;

namespace Lab1FL
{
    interface INodeRepository<T> where T : class
    {   // дефолтный CRUD
        IEnumerable<T> GetList();
        void Create(T item);
        T GetItem(int id); // Read
        void Update(T item);
        void Delete(int id);
    }
}
