using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.ViewModel
{
    public class ParallelInfoReportViewModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Teachers { get; set; }
        public string SchoolSubjects { get; set; }
    }
}