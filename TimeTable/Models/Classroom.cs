using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    [Table("Classroom")]
    public class Classroom
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
    }
}