using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlefPresentation.DataAccess.Interfaces
{
    public interface ICrudService<T, TKey> where T: class
    {
        IEnumerable<T> GetAll();

        T Add(T item);

        T Update(T item);

        void Delete(T item);

        T GetById(TKey key);
    }
}
