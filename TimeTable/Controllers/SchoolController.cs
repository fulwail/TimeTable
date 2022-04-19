using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.Domain;
using TimeTable.Helper;
using TimeTable.Services;
using TimeTable.ViewModel;

namespace TimeTable.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ITimetableServices _sqlServices;


        public SchoolController(ITimetableServices sqlServices)
        {
            _sqlServices = sqlServices;
        }

        public ActionResult Index()
        {
            var errorMessage = TempData["ErrorMessage"] as string;
            var timetable = _sqlServices.GetLessonView();
            if (errorMessage != null)
                timetable.ErrorMessage = errorMessage;
            return View("Index",timetable);
        }

        public ActionResult TeacherView()
        {
            var teacher = _sqlServices.GetTeachersView();
            return View("TeacherView",teacher);
        }

        public ActionResult SchoolSubjectView()
        {
            var schoolSubject = _sqlServices.GetSchoolSubjectView();
            return View(schoolSubject);
        }

        public ActionResult ClassView()
        {
            var classes = _sqlServices.GetClassList();
            var message = TempData["Message"] as string;
            var classView = new ClassViewModel
            {
                ClassList = classes,    
                Message = message
            };
            return View(classView);
        }
        public ActionResult ClassRoomView()
        {        
            var classrooms = _sqlServices.GetClassrooms();          
            return View(classrooms);
        }
        public ActionResult ReportView(List<Enum.DayOfWeek> dayOfWeeks, int? classParallelNumber)
        {

            var reportViewModel = _sqlServices.GetReportViewModel(dayOfWeeks, classParallelNumber);
            return View(reportViewModel);
        }
    }
}