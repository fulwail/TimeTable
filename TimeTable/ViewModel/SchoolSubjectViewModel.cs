using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;

namespace TimeTable.ViewModel
{
    public class SchoolSubjectViewModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String TeacherName { get; set; }
    }
}