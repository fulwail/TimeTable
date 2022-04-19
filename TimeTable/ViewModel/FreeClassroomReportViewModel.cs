using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.ViewModel
{
    public class FreeClassroomReportViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int LessonsCount { get; set; }

    }
}