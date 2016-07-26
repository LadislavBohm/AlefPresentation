using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AlefPresentation.Api.ActionFilters;
using AlefPresentation.Api.Model;
using AlefPresentation.DataAccess;
using AlefPresentation.DataAccess.Interfaces;
using AlefPresentation.Model;

namespace AlefPresentation.Api.Controllers
{
    [RoutePrefix("api/attendee")]
    public class AttendeeController : ApiController
    {
        private readonly ICrudService<PresentationAttendee,int> _attendeeService = new AttendeeService();

        [AllowAnonymous,HttpGet]
        public IEnumerable<PresentationAttendee> Get()
        {
            return _attendeeService.GetAll();
        }

        [AlefAuthentication, HttpPost]
        public Task<HttpResponseMessage> Post([FromBody] NewAttendeeViewModel attendee)
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            var added = _attendeeService.Add(new PresentationAttendee()
            {
                FullName = attendee.FullName,
                JobPosition = attendee.JobPosition
            });

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created, added));
        }

        
        [HttpGet,CheckForNull]
        public IEnumerable<PresentationAttendee> GetByName(string name)
        {
            return _attendeeService.GetAll().Where(a => a.FullName.Contains(name));
        }

        
        [HttpGet]
        public IEnumerable<PresentationAttendee> GetByJob(string job)
        {
            return _attendeeService.GetAll().Where(a => a.JobPosition.Contains(job));
        }
    }
}