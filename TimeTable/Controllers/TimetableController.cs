using System;
using System.Web.Mvc;
using TimeTable.Models;
using TimeTable.Services;

namespace TimeTable.Controllers
{
    public class TimetableController : Controller
    {
        private readonly ITimetableServices _sqlServices;
        private Class _class;
        public TimetableController(ITimetableServices sqlServices)
        {
            _sqlServices = sqlServices;
        }
        public ActionResult CreateOrUpdate(Lesson timeTable)
        {        
              var result = _sqlServices.CreateOrUpdateLesson(timeTable);
                if (!result.IsSuccsess)
                    TempData["ErrorMessage"] = result.Message;
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult DeleteLesson(Guid id)
        {
            var result = _sqlServices.DeleteLesson(id);
            if (!result.IsSuccsess)
                TempData["ErrorMessage"] = result.Message;
            return Redirect(Request.UrlReferrer.ToString());
        }


    }
}