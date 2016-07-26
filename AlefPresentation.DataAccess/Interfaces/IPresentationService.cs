using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlefPresentation.Model;

namespace AlefPresentation.DataAccess.Interfaces
{
    public interface IPresentationService : ICrudService<Presentation,int>
    {
        IEnumerable<Presentation> GetEmpty();

    }
}
