using TimeTable.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace TimeTable.ViewModel
{
    public class LessonViewModel
    {
        public Guid LessonId { get; set; }
        public string DayOfWeek { get; set; }
        public string ClassName { get; set; }
        public string ClassroomName { get; set; }
        public string SchoolSubjectName { get; set; }
        public string TeacherName { get; set; }
        public int LessonNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}