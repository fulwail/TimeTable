using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;

namespace TimeTable.ViewModel
{
    public class ClassViewModel
    {
        public List<Class> ClassList { get; set; }
        public string Message { get; set; }
    }

}