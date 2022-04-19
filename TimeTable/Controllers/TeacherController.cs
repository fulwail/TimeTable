using System;
using System.Web.Mvc;
using TimeTable.Models;
using TimeTable.Services;
using TimeTable.ViewModel;

namespace TimeTable.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITimetableServices _sqlServices;
        public TeacherController(ITimetableServices sqlServices)
        {
            _sqlServices = sqlServices;
        }
        public ActionResult Edit(Guid id)
        {
            var teacherViewModel = _sqlServices.GetEditTeachersView(id);         
            return View(teacherViewModel);
        }
        public ActionResult Create()
        {
            var teacherViewModel = new TeacherViewModel
            {
                Id = Guid.NewGuid()
            };
            return View(teacherViewModel);
        }

       [HttpPost]
        public ActionResult CreateOrUpdateTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _sqlServices.CreateOrUpdateTeacher(teacher);
                return RedirectToAction("../School/TeacherView");
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult DeleteTeacher(Guid id)
        {
            _sqlServices.DeleteTeacher(id);
            return RedirectToAction("../School/TeacherView");
        }
        public ActionResult RemoveTeacherOnSchoolSubject(Guid id, Guid schoolSubjectId)
        {
            _sqlServices.RemoveTeacherOnSchoolSubject(schoolSubjectId);
            return RedirectToAction($"../Teacher/Edit/{id}");
        }
        public ActionResult AddTeacherOnSchoolSubject(Guid TeacherId, Guid schoolSubjectId)
        {
            _sqlServices.AddTeacherOnSchoolSubject(TeacherId, schoolSubjectId);
            return RedirectToAction($"../Teacher/Edit/{TeacherId}");
        }
    }
}