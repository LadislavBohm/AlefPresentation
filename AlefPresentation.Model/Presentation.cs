using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlefPresentation.Model
{
    public class Presentation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }


        public Lecturer Lecturer { get; set; }

        public ICollection<PresentationAttendee> Attendees { get; set; }
    }
}
