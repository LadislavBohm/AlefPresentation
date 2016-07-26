using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AlefPresentation.Api.ActionFilters;
using AlefPresentation.DataAccess;
using AlefPresentation.DataAccess.Interfaces;
using AlefPresentation.Model;

namespace AlefPresentation.Api.Controllers
{
    [RoutePrefix("api/presentation")]
    public class PresentationController : ApiController
    {
        private readonly IPresentationService _presentationService = new PresentationService();

        //konstruktor s parametrem

        //public PresentationController(IPresentationService presentationService)
        //{
        //    _presentationService = presentationService;
        //}

        [HttpGet,Route("empty")]
        public IEnumerable<Presentation> GetEmpty()
        {
            return _presentationService.GetEmpty(); 
        }

        [HttpPatch, AlefAuthentication,Route("")]
        public IHttpActionResult UpdateDescription(int id, string description)
        {
            var toUpdate = _presentationService.GetById(id);
            if (toUpdate == null)
            {
                //Cisco vraci HttpStatusCode.Gone
                
                return NotFound();
            }

            toUpdate.Description = description;

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete, AlefAuthentication, Authorize(Roles = "admin"), Route("")]
        public IHttpActionResult Delete(int id)
        {
            var toDelete = _presentationService.GetById(id);

            if (toDelete == null)
            {
                //Cisco opet vraci HttpStatusCode.Gone
                return NotFound();
            }

            _presentationService.Delete(toDelete);

            return Ok();
        }
    }
}