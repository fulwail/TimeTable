using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTable.Models;
using TimeTable.ViewModel;

namespace TimeTable.Domain
{
    public interface ISqlContext : IDisposable
    {
        DbSet<Lesson> TimeTables { get; set; }
        DbSet<SchoolSubject> SchoolSubjects { get; set; }
        DbSet<Class> Classess { get; set; }
        DbSet<Classroom> Classrooms { get; set; }
        DbSet<LessonTime> LessonTimes { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        T GetItem<T>(string path);
        List<T> GetQuery<T>(string path);
    }

}
