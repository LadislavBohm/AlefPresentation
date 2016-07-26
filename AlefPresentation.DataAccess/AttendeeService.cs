using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlefPresentation.DataAccess.Interfaces;
using AlefPresentation.Model;

namespace AlefPresentation.DataAccess
{
    public class AttendeeService : ICrudService<PresentationAttendee,int>
    {
        public IEnumerable<PresentationAttendee> GetAll()
        {
            return Data;
        }

        public PresentationAttendee Add(PresentationAttendee item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            item.Id = Math.Abs(Guid.NewGuid().ToString().GetHashCode());

            Data.Add(item);

            return item;
        }

        public PresentationAttendee Update(PresentationAttendee item)
        {
            var toUpdate = GetById(item.Id);

            if (toUpdate != null)
                toUpdate = item;

            return toUpdate;
        }

        public void Delete(PresentationAttendee item)
        {
            var toDelete = GetById(item.Id);

            if (toDelete != null)
                Data.Remove(toDelete);
        }

        public PresentationAttendee GetById(int key)
        {
            return Data.FirstOrDefault(l => l.Id == key);
        }

        #region static data init

        protected static ICollection<PresentationAttendee> Data { get; } = new List<PresentationAttendee>
        {
            new PresentationAttendee { Id = 1, FullName = "Vojtech Hlavacka", JobPosition = "Programmer" },
            new PresentationAttendee { Id = 2, FullName = "Milan Husner", JobPosition = "Programmer" },
            new PresentationAttendee { Id = 3, FullName = "Tomas Adl", JobPosition = "Operations" },
            new PresentationAttendee { Id = 4, FullName = "Kristyna Valdova", JobPosition = "Tester" },
            new PresentationAttendee { Id = 5, FullName = "David Kriz", JobPosition = "Project manager" }
        };

        #endregion
    }
}
