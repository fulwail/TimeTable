using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;

namespace TimeTable.ViewModel
{
    public class LessonListViewModel
    {
        public List<LessonViewModel> Lessons { get; set; }
        public List<Class> Class { get; set; }
        public List<DayOfWeekView> DayOfWeek { get; set; }
        public List<LessonTime> LessonTimes { get; set; }

        public List<SchoolSubject> SchoolSubjects { get; set; }
        public List<Classroom> Classrooms { get; set; }
        public String ErrorMessage { get; set; }

    }
}