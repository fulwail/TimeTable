using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    [Table("Teacher")]
    public class Teacher
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String SurName { get; set; }
        [Required]
        public String Patronymic { get; set; }
        public List<SchoolSubject> SchoolSubject { get; set; }
    }
}