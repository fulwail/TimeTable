using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;

namespace TimeTable.ViewModel
{
    public class EditTeacherViewModel
    {
        public Teacher Teacher { get; set; }
        public List<SchoolSubject> SchoolSubject { get; set; }
    }
}