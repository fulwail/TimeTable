using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;

namespace TimeTable.ViewModel
{
    public class ReportViewModel
    {
        public List<DayOfWeekView> DayOfWeek { get; set; }


        public List<int> ClassParallels { get; set; }
        public List<ClassInfoReportViewModel> ClassInfoReport { get; set; }
        public  List<ParallelInfoReportViewModel> ParallelInfoReport { get; set; }
        public BusyTeacherViewModel BusyTeacher { get; set; }
        public FreeClassroomReportViewModel FreeClassRoom { get; set; }
        public List<Enum.DayOfWeek> ActiveDayOfWeeks { get; set; }

    }
}