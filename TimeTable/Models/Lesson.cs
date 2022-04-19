using TimeTable.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    [Table("Lesson")]
    public class Lesson
    {
        [Key]
        public  Guid LessonId { get; set; }

        public Enum.DayOfWeek DayOfWeek { get; set; }

        public Guid ClassId { get; set; }

        public Guid LessonTimeId { get; set; }

        public Guid SchoolSubjectId { get; set; }

        public Guid ClassroomId { get; set; }
        public Class Class { get; set; }
        public Classroom Classroom { get; set; }
        public SchoolSubject SchoolSubject { get; set; }
        public LessonTime LessonTime { get; set; }
    }
}