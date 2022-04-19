using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    [Table("Class")]
    public class Class
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int Number { get; set; }
    }
}