using System;
using System.Web.Mvc;
using TimeTable.Models;
using TimeTable.Services;

namespace TimeTable.Controllers
{
    public class SchoolSubjectController : Controller
    {
        private readonly ITimetableServices _sqlServices;
        public SchoolSubjectController(ITimetableServices sqlServices)
        {
            _sqlServices = sqlServices;
        }
        public ActionResult CreateOrUpdate(SchoolSubject schoolSubject)
        {
            if (ModelState.IsValid)
            {
                _sqlServices.CreateOrUpdateSchoolSubject(schoolSubject);
                return RedirectToAction("../School/SchoolSubjectView");
            }  

            return Redirect(Request.UrlReferrer.ToString());

        }
        public ActionResult Edit(Guid id)
        {
            var schoolSubjectViewModel = _sqlServices.GetEditSchoolSubjectView(id);
            return View(schoolSubjectViewModel);

        }

        public ActionResult Delete (Guid id)
        {
            _sqlServices.DeleteSchoolSubject(id);
          
            return RedirectToAction("../School/SchoolSubjectView");

        }
    }
}