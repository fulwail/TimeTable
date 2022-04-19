using System;
using System.Web.Mvc;
using TimeTable.Models;
using TimeTable.Services;

namespace TimeTable.Controllers
{
    public class ClassRoomController : Controller
    {
        private readonly ITimetableServices _sqlServices;


        public ClassRoomController(ITimetableServices sqlServices)
        {
            _sqlServices = sqlServices;
        }
        public ActionResult CreateOrUpdateClassroom(Classroom classroom)
        {
            _sqlServices.CreateOrUpdateClassroom(classroom);
            return RedirectToAction("../School/ClassRoomView");
        }

        public ActionResult DeleteClassroom(Guid id)
        {
            _sqlServices.DeleteClassroom(id);
            return Redirect(Request.UrlReferrer.ToString());

        }
        public ActionResult Edit(Guid id)
        {
          Classroom classroom=  _sqlServices.GetClassroomById(id);
            return View(classroom);

        }
    }
}