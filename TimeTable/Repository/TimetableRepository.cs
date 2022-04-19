using TimeTable.Domain;
using TimeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeTable.ViewModel;

namespace TimeTable.Repository
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly ISqlContext _sqlContext;
     
        public TimetableRepository(ISqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public BusyTeacherViewModel GetBusyTeacher()
        {
            var ctx = (SqlContext)_sqlContext;

            var teacher = ctx.GetItem<BusyTeacherViewModel>("BusyTeacher");
            return teacher;
        }

        public FreeClassroomReportViewModel GetFreeClassroom(List<Enum.DayOfWeek> dayOfWeeks)
        {
            var ctx = (SqlContext)_sqlContext;
        
            var classroom = ctx.GetFreeClasroom(dayOfWeeks);
            return classroom;
        }

        public List<Teacher> GetTeachers()
        {
            var ctx = (SqlContext)_sqlContext;
            var teachers = ctx.Teachers
                 .Include("SchoolSubject")
                .ToList();
            return teachers;
        }

        public List<SchoolSubject> GetSchoolSubjects(bool needFreeSchoolSubjects =false)
        {
            var ctx = (SqlContext)_sqlContext;
            
            var schoolSubjects = ctx.SchoolSubjects
                 .Include("Teacher")
                .ToList();
            if (needFreeSchoolSubjects)
                schoolSubjects= schoolSubjects.Where(x => x.TeacherId ==null).ToList();
            
            return schoolSubjects;
        }
        public List<Lesson> GetLessons()
        {
            var ctx = (SqlContext)_sqlContext;
            var timetables = ctx.TimeTables
                .Include("Classroom")
                .Include("Class")
                .Include("SchoolSubject")
                .Include("SchoolSubject.Teacher")
                 .Include("LessonTime")
                 .OrderBy(x => x.ClassId)
                 .ThenBy(x=>x.DayOfWeek)
                 .ThenBy(x => x.LessonTimeId)
                .ToList();                        
             
            return timetables;
        }

        public Teacher GetTeacherById(Guid id)
        {
            var ctx = (SqlContext)_sqlContext;
            var teacher = ctx.Teachers
                .Include("SchoolSubject")
                .FirstOrDefault(x => x.Id == id);
            return teacher;
        }

        public void CreateOrUpdateTeacher(Teacher teacher)
        {
            var ctx = (SqlContext)_sqlContext;
            var teacherModel = ctx.Teachers.Where(x => x.Id == teacher.Id).FirstOrDefault();
            if (teacherModel != null)
            {
                teacherModel.Name = teacher.Name;
                teacherModel.Patronymic = teacher.Patronymic;
                teacherModel.SurName = teacher.SurName;
            }
            else
            {
                ctx.Teachers.Add(teacher);
            }
            ctx.SaveChanges();
        }

        public void RemoveTeacherOnSchoolSubject(Guid schoolSubjectId)
        {
            var ctx = (SqlContext)_sqlContext;
            var schoolSubject=ctx.SchoolSubjects.Where(x => x.Id == schoolSubjectId).FirstOrDefault();
            schoolSubject.TeacherId = null;
            ctx.SaveChanges();
        }

        public void AddTeacherOnSchoolSubject(Guid id, Guid schoolSubjectId)
        {
            var ctx = (SqlContext)_sqlContext;
            var schoolSubject = ctx.SchoolSubjects.Where(x => x.Id == schoolSubjectId).FirstOrDefault();
            schoolSubject.TeacherId = id;
            ctx.SaveChanges();
        }

        public void DeleteTeacher(Guid id)
        {
            var ctx = (SqlContext)_sqlContext;
            var teacher = ctx.Teachers.Find(id);
            if (teacher != null)
                ctx.Teachers.Remove(teacher);
            ctx.SaveChanges();

        }

        public void CreateOrUpdateSchoolSubject(SchoolSubject schoolSubject)
        {
            var ctx = (SqlContext)_sqlContext;
            var schoolSubjectModel = ctx.SchoolSubjects.Where(x => x.Id == schoolSubject.Id).FirstOrDefault();
            if (schoolSubjectModel != null)
            {
                schoolSubjectModel.Name = schoolSubject.Name;
                schoolSubjectModel.TeacherId = schoolSubject.TeacherId;
            }
            else
            {
                schoolSubject.Id = Guid.NewGuid();
                ctx.SchoolSubjects.Add(schoolSubject);
            }
            ctx.SaveChanges();
        }

        public void DeleteSchoolSubject(Guid id)
        {
            var ctx = (SqlContext)_sqlContext;
            var schoolSubject = ctx.SchoolSubjects.Find(id);
            if (schoolSubject != null)
                ctx.SchoolSubjects.Remove(schoolSubject);
            ctx.SaveChanges();
        }

        public SchoolSubject GetSchoolSubjectById(Guid id)
        {
            var ctx = (SqlContext)_sqlContext;
            var schoolSubjects = ctx.SchoolSubjects
                .Include("Teacher")
                .FirstOrDefault(x => x.Id == id);
            return schoolSubjects;
        }

        public List<Class> GetClassList()
        {
            var ctx = (SqlContext)_sqlContext;
            return ctx.Classess.OrderBy(x=>x.Number).ThenBy(x=>x.Name).ToList();
        }

        public void CreateClass(Class @class)
        {
            var ctx = (SqlContext)_sqlContext;
            ctx.Classess.Add(@class);
            ctx.SaveChanges();
        }

        public Result FormClassName(int number)
        {
            var result = new Result();
            var ctx = (SqlContext)_sqlContext;
            var name =Convert.ToChar( ctx.Classess.Where(x => x.Number == number).OrderByDescending(x => x.Name).Select(x => x.Name).FirstOrDefault() ?? " ") ;
            if (name == ' ')
            {
                result.IsSuccsess = true;
                result.Message = "А";
            }
            else if (name == 'Я')
            {
                result.IsSuccsess = false;
            }
            else
            {
                result.IsSuccsess = true;
                result.Message = Convert.ToChar(name + 1).ToString();
            }
           return result;
          
        }

        public void DeleteClass(Guid id)
        {
            var ctx = (SqlContext)_sqlContext;
            var classEntity = ctx.Classess.Find(id);
            if (classEntity != null)
                ctx.Classess.Remove(classEntity);
            ctx.SaveChanges();
        }
        public Result DeleteLesson(Guid id)
        {
            var result = new Result();
            var ctx = (SqlContext)_sqlContext;
            var timeTable = ctx.TimeTables.Find(id);
            if (timeTable != null)
            {
                var timetablesOnCurrentDay = GetLessons().Where(x => x.DayOfWeek == timeTable.DayOfWeek && x.ClassId == timeTable.ClassId && x.LessonId!=id).ToList();
                if (timetablesOnCurrentDay.Count != 0)
                {
                    timetablesOnCurrentDay = timetablesOnCurrentDay
                         .Where(x => x.LessonTime.LessonNumber == timeTable.LessonTime.LessonNumber + 1
                        || x.LessonTime.LessonNumber == timeTable.LessonTime.LessonNumber - 1).ToList();
                    if (timetablesOnCurrentDay.Count==2)
                    {
                        result.IsSuccsess = false;
                        result.Message = "Нельзя удалить урок.Это приведет к наличию окон в расписании";
                        return result;
                    }
                }
                ctx.TimeTables.Remove(timeTable);
            }
            result.IsSuccsess = true;
            ctx.SaveChanges();
            return result;
        }

        public List<Classroom> GetClassrooms()
        {
            var ctx = (SqlContext)_sqlContext;
            return ctx.Classrooms.OrderBy(x => x.Name).ToList();
        }

        public void CreateOrUpdateClassroom(Classroom classroom)
        {
            var ctx = (SqlContext)_sqlContext;
            var classroomModel = ctx.Classrooms.Find(classroom.Id);
            if (classroomModel == null)
            {
                classroom.Id = Guid.NewGuid();
                ctx.Classrooms.Add(classroom);
            }
            else
            {
                classroomModel.Name = classroom.Name;
            }
            ctx.SaveChanges();
        }

        public void DeleteClassroom(Guid id)
        {
            var ctx = (SqlContext)_sqlContext;
            var classEntity = ctx.Classrooms.Find(id);
            if (classEntity != null)
                ctx.Classrooms.Remove(classEntity);
            ctx.SaveChanges();
        }

        public List<LessonTime> GetLessonTimes()
        {
            var ctx = (SqlContext)_sqlContext;
            return ctx.LessonTimes.OrderBy(x=>x.LessonNumber).ToList();
        }

        public Result CreateOrUpdateLesson(Lesson timeTable)
        {
            var result = new Result();
            var ctx = (SqlContext)_sqlContext;
            var timeTabletModel = ctx.TimeTables.Where(x => x.LessonTimeId == timeTable.LessonTimeId && x.DayOfWeek== timeTable.DayOfWeek && x.ClassId== timeTable.ClassId).FirstOrDefault();
            if (timeTabletModel != null)
            {
                timeTabletModel.SchoolSubjectId = timeTable.SchoolSubjectId;
                timeTabletModel.ClassroomId = timeTable.ClassroomId;
            }
            else
            {
                var timetablesOnCurrentDay =GetLessons().Where(x => x.DayOfWeek == timeTable.DayOfWeek && x.ClassId == timeTable.ClassId).ToList();
                if (timetablesOnCurrentDay.Count!=0)
                {
                    timetablesOnCurrentDay = timetablesOnCurrentDay
                         .Where(x => x.LessonTime.LessonNumber == timeTable.LessonTime.LessonNumber + 1
                        || x.LessonTime.LessonNumber == timeTable.LessonTime.LessonNumber - 1).ToList();
                    if (!timetablesOnCurrentDay.Any())
                    {
                        result.IsSuccsess = false;
                        result.Message = "В расписании недопустимо наличие окон";
                        return result;
                    }    
                }
                timeTable.LessonId = Guid.NewGuid();
                ctx.TimeTables.Add(timeTable);
            }
            result.IsSuccsess = true;
            ctx.SaveChanges();
            return result;
        }

        public LessonTime GetLessonTimeById(Guid id)
        {
            var ctx = (SqlContext)_sqlContext;
            return ctx.LessonTimes.Find(id);
        }

        public List<ParallelInfoReportViewModel> GetParallelInfoReport(int classParallelNumber)
        {
            var ctx = (SqlContext)_sqlContext;
            return ctx.GetParallelInfoReport(classParallelNumber);
        }
        public List<ClassInfoReportViewModel> GetClassInfoReport()
        {
            var ctx = (SqlContext)_sqlContext;
            return ctx.GetClassInfoReport();
        }

        public Classroom GetClassroomById(Guid id)
        {
            var ctx = (SqlContext)_sqlContext;
            var classroom = ctx.Classrooms
                .FirstOrDefault(x => x.Id == id);
            return classroom;
        }
    }

}
