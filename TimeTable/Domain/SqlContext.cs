using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TimeTable.Models;
using TimeTable.ViewModel;

namespace TimeTable.Domain
{
    public class SqlContext : DbContext, ISqlContext
    {
        public SqlContext() : base("SqlContext")
        {
            Database.SetInitializer<SqlContext>(null);
        }

        public DbSet<Lesson> TimeTables { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SchoolSubject> SchoolSubjects { get; set; }
        public DbSet<Class> Classess { get; set; }
        public DbSet<Classroom> Classrooms{ get; set; }
        public DbSet<LessonTime> LessonTimes { get; set; }

        public T GetItem<T>(string fileName)
        {
            string sqlScript = System.IO.File.ReadAllText(GetPath(fileName));
            var item = Database.SqlQuery<T>(sqlScript).FirstOrDefault();
            return item;
        }
        public List<T> GetQuery<T>(string fileName)
        {
            string sqlScript = System.IO.File.ReadAllText(GetPath(fileName));
            var query = Database.SqlQuery<T>(sqlScript).ToList();
            return query;
        }

        public FreeClassroomReportViewModel GetFreeClasroom(List<Enum.DayOfWeek> dayOfWeeks)
        {
            var classroom = new FreeClassroomReportViewModel();
            string sqlScript = System.IO.File.ReadAllText(GetPath("FreeClassroom"));
            sqlScript = string.Format(sqlScript, String.Join(", ", dayOfWeeks.Select(day => ((int)day))));
            classroom = Database.SqlQuery<FreeClassroomReportViewModel>(sqlScript).DefaultIfEmpty(new FreeClassroomReportViewModel()).FirstOrDefault(); 
            return classroom;

        }

        public List<ParallelInfoReportViewModel> GetParallelInfoReport(int classParallelNumber)
        {
            string sqlScript = System.IO.File.ReadAllText(GetPath("ParallelInfo"));
            sqlScript = string.Format(sqlScript,classParallelNumber);
            var parallelInfoReport = Database.SqlQuery<ParallelInfoReportViewModel>(sqlScript).ToList();
            return parallelInfoReport;

        }
        public List<ClassInfoReportViewModel> GetClassInfoReport()
        {
            string sqlScript = System.IO.File.ReadAllText(GetPath("ClassInfo"));
            var parallelInfoReport = Database.SqlQuery<ClassInfoReportViewModel>(sqlScript).ToList();
            return parallelInfoReport;

        }

        private string GetPath(string filename)
        {
            return System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"SqlScript\", $"{filename}.sql"); ; ;
        }
      
    }
}
