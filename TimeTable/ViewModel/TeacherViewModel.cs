using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;

namespace TimeTable.ViewModel
{
    public class TeacherViewModel
    {
 
        public Guid? Id { get; set; }
        public String Name { get; set; }
        public String SurName { get; set; }
        public String Patronymic { get; set; }
        public String SchoolSubjects { get; set; }    
    }
}