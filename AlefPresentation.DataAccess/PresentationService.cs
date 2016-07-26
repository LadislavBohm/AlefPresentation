using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlefPresentation.DataAccess.Interfaces;
using AlefPresentation.Model;

namespace AlefPresentation.DataAccess
{
    public class PresentationService : IPresentationService
    {
        public IEnumerable<Presentation> GetAll()
        {
            return Data;
        }

        public Presentation Add(Presentation item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            Data.Add(item);

            return item;
        }

        public Presentation Update(Presentation item)
        {
            var toUpdate = GetById(item.Id);

            if (toUpdate != null)
                toUpdate = item;

            return toUpdate;
        }

        public void Delete(Presentation item)
        {
            var toDelete = GetById(item.Id);

            if (toDelete != null)
                Data.Remove(toDelete);
        }

        public Presentation GetById(int key)
        {
            return Data.FirstOrDefault(p => p.Id == key);
        }

        public IEnumerable<Presentation> GetEmpty()
        {
            return Data.Where(p => p.Attendees == null || !p.Attendees.Any());
        }

        #region static data init

        protected static ICollection<Presentation> Data { get; } = CreateDefaultData();

        private static ICollection<Presentation> CreateDefaultData()
        {
            var result = new List<Presentation>();

            var k = new Lecturer { Id = 1, FirstName = "Jakub", LastName = "Kana" };
            var l = new Lecturer { Id = 2, FirstName = "Ladislav", LastName = "Bohm" };
            var o = new Lecturer { Id = 3, FirstName = "Ondrej", LastName = "Prochazka" };

            var v = new PresentationAttendee { Id = 1, FullName = "Vojtech Hlavacka", JobPosition = "Programmer" };
            var m = new PresentationAttendee { Id = 2, FullName = "Milan Husner", JobPosition = "Programmer" };
            var t = new PresentationAttendee { Id = 3, FullName = "Tomas Adl", JobPosition = "Operations" };
            var kv = new PresentationAttendee { Id = 4, FullName = "Kristyna Valdova", JobPosition = "Tester" };
            var c = new PresentationAttendee { Id = 5, FullName = "David Kriz", JobPosition = "Project manager" };

            result.Add(new Presentation
            {
                Id = 1,
                Title = "Cisco IOS",
                Description = "Zakladni prikazy, rozchozeni SSH, zmena a ulozeni konfigurace a zabezpeceni.",
                StartDate = DateTime.Now.AddDays(-14),
                Duration = TimeSpan.FromHours(1),
                Lecturer = k,
                Attendees = new List<PresentationAttendee> { v,m }
            });
            result.Add(new Presentation
            {
                Id = 2,
                Title = "IIS Authz/Authc",
                Description = "Autentizace a autorizace v prostredi IIS",
                StartDate = DateTime.Now.AddDays(-6),
                Duration = TimeSpan.FromMinutes(45),
                Lecturer = o,
                Attendees = new List<PresentationAttendee> { v,t,kv,c}
            });
            result.Add(new Presentation
            {
                Id = 3,
                Title = "Web API",
                Description = "Web API pro zelenace",
                StartDate = DateTime.Now.AddDays(-20),
                Duration = TimeSpan.FromMinutes(60),
                Lecturer = l,
                Attendees = new List<PresentationAttendee> { m, v, c, t }
            });
            result.Add(new Presentation
            {
                Id = 4,
                Title = "Nezajimava",
                Description = "Naprosto k nicemu.",
                StartDate = DateTime.Now.AddDays(-4),
                Duration = TimeSpan.FromMinutes(30),
                Lecturer = l,
                Attendees = new List<PresentationAttendee> { }
            });

            return result;
        }

        #endregion
    }
}
