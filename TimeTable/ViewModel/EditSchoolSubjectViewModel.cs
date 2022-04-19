using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;

namespace TimeTable.ViewModel
{
    public class EditSchoolSubjectViewModel
    {
        public SchoolSubject SchoolSubject { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}