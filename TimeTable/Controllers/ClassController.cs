using System;
using System.Web.Mvc;
using TimeTable.Models;
using TimeTable.Services;

namespace TimeTable.Controllers
{
    public class ClassController : Controller
    {
        private readonly ITimetableServices _sqlServices;


        public ClassController(ITimetableServices sqlServices)
        {
            _sqlServices = sqlServices;
        }


        public ActionResult CreateClass(Class @class)
        {
                   var message = _sqlServices.CreateClass(@class);
                 TempData["Message"] = message;
            return RedirectToAction("../School/ClassView");
        }

        public ActionResult DeleteClass(Guid id)
        {
            _sqlServices.DeleteClass(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}