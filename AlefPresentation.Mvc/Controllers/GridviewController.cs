using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlefPresentation.DataAccess;

namespace AlefPresentation.Mvc.Controllers
{
    public class GridviewController : Controller
    {
        private readonly PresentationService _presentationService = new PresentationService();

        // GET: Gridview
        public ActionResult Index()
        {
            var data = _presentationService.GetAll().ToList();
            return View(data);
        }

        public ActionResult Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                //pri prazdnem search vratit vse
                return RedirectToAction("Index");
            }

            ViewBag.SearchTerm = term;

            return View("Index", _presentationService.GetAll()
                .Where(p => 
                    p.Title.ToLower().Contains(term.ToLower()) || 
                    p.Lecturer.FirstName.ToLower().Contains(term.ToLower()) ||
                    p.Lecturer.LastName.ToLower().Contains(term.ToLower()))
                .ToList());
        }
    }
}