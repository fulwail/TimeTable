using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.ViewModel
{
    public class BusyTeacherViewModel
    {
        
        public Guid? Id { get; set; }
        public String Name { get; set; }
        public String SurName { get; set; }
        public String Patronymic { get; set; }
        public int CountSchoolSubject { get; set; }
    }
}