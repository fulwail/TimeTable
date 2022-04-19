using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    [Table("SchoolSubject")]
    public class SchoolSubject
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Наименование предмета")]
        public String Name { get; set; }
        public Guid? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

    }
}