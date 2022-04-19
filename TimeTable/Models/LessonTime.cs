using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    [Table("LessonTime")]
    public class LessonTime
    {
        public Guid Id { get; set; }
        public int LessonNumber { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}