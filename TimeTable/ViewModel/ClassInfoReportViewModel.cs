using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.ViewModel
{
    public class ClassInfoReportViewModel
    {
        public Guid ClassId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int LessonsCount { get; set; }
        public int TeacherCount { get; set; }
    }
}