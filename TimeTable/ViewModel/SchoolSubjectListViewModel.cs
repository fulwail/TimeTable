using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;

namespace TimeTable.ViewModel
{
    public class SchoolSubjectListViewModel
    {
       public List<SchoolSubjectViewModel> SchoolSubjects { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}