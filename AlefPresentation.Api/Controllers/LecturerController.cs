using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using AlefPresentation.Api.ActionFilters;
using AlefPresentation.DataAccess;
using AlefPresentation.DataAccess.Interfaces;
using AlefPresentation.Model;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server.Attributes;
using WebApi.OutputCache.V2;

namespace AlefPresentation.Api.Controllers
{
    [RoutePrefix("api/lecturer")]
    public class LecturerController : ApiController
    {
        private readonly ICrudService<Lecturer,int> _lecturerService = new LecturerService();

        [HttpGet,CacheOutput(ServerTimeSpan = 6)]
        public IEnumerable<Lecturer> Get()
        {
            Thread.Sleep(2000);
            return _lecturerService.GetAll();
        }

        [HttpPut,CheckForNull]
        public IHttpActionResult Put(Lecturer updated)
        {
            var toUpdate = _lecturerService.GetById(updated.Id);
            if (toUpdate == null)
            {
                //nebo vratit HttpStatusCode.Gone...
                return NotFound();
            }

            return Ok(_lecturerService.Update(updated));
        }

        [HttpGet,CheckForNull, CacheOutput(ServerTimeSpan = 10)]
        public async Task<IEnumerable<Lecturer>> GetByName(string name)
        {
            await Task.Delay(2000).ConfigureAwait(false);

            return _lecturerService.GetAll().Where(l => l.LastName.Contains(name) || l.FirstName.Contains(name));
        }

        [HttpGet,Route("uncompressed")]
        [Compression(Enabled = false)]
        public IEnumerable<Lecturer> GetUncompressedData()
        {
            var result = new List<Lecturer>();

            do
            {
                result.AddRange(_lecturerService.GetAll());
            } while (result.Count < 10000);

            return result;
        }

        [HttpGet,Route("compressed")]
        [Compression(Enabled = true)]
        public IEnumerable<Lecturer> GetCompressedData()
        {
            //test ve Fiddleru: Accept-Encoding: gzip;
            //http://checkgzipcompression.com/?url=http%3A%2F%2Fseznam.cz
            var result = new List<Lecturer>();

            do
            {
                result.AddRange(_lecturerService.GetAll());
            } while (result.Count < 10000);

            return result;
        }
    }
}