using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlefPresentation.Model;
using AlefPresentation.Mvc.ViewModels;

namespace AlefPresentation.Mvc.Controllers
{
    [Authorize]
    public class FormsController : Controller
    {
        // GET: Forms
        public ActionResult Index()
        {
            return View(new LecturerViewModel());
        }

        [Route("AddLecturer")]
        public ActionResult Add(LecturerViewModel lecturer)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", lecturer);
            }


            //TODO:prevod na z ViewModel na Model

            //TODO: Model zvalidovat a ulozit napr. do databaze

            lecturer.OperationSuccessful = true;

            return View("Index", lecturer);
        }
    }
}