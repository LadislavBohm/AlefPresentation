using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlefPresentation.DataAccess.Interfaces;
using AlefPresentation.Model;

namespace AlefPresentation.DataAccess
{
    public class LecturerService : ICrudService<Lecturer, int>
    {
        public IEnumerable<Lecturer> GetAll()
        {
            return Data;
        }

        public Lecturer Add(Lecturer item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            Data.Add(item);

            return item;
        }

        public Lecturer Update(Lecturer item)
        {
            var toUpdate = GetById(item.Id);

            if (toUpdate != null)
                toUpdate = item;

            return toUpdate;
        }

        public void Delete(Lecturer item)
        {
            var toDelete = GetById(item.Id);

            if (toDelete != null)
                Data.Remove(toDelete);
        }

        public Lecturer GetById(int key)
        {
            return Data.FirstOrDefault(l => l.Id == key);
        }

        #region static data init

        protected static ICollection<Lecturer> Data { get; } = new List<Lecturer>
        {
            new Lecturer { Id = 1, FirstName = "Jakub", LastName = "Kana" },
            new Lecturer { Id = 2, FirstName = "Ladislav", LastName = "Bohm" },
            new Lecturer { Id = 3, FirstName = "Ondrej", LastName = "Prochazka" }
        };

        #endregion
    }
}
