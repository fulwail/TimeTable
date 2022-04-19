using TimeTable.Data;
using TimeTable.Domain;
using TimeTable.Models;
using TimeTable.Repository;
using TimeTable.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EnumsNET;
using TimeTable.Helper;

namespace TimeTable.Services
{
    public class TimetableServices : ITimetableServices
    {

        private readonly ITimetableRepository _timetableRepository;


        public TimetableServices(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }



        public FreeClassroomReportViewModel GetFreeClassroom(List<Enum.DayOfWeek> dayOfWeeks)
        {
            return _timetableRepository.GetFreeClassroom(dayOfWeeks);
        }

        public TeacherListViewModel GetTeachersView()
        {
            var teachers = _timetableRepository.GetTeachers();
            var teacherViews = teachers.Select(x => new TeacherViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                SurName = x.SurName,
                Patronymic = x.Patronymic,
                SchoolSubjects = String.Join(", ", x.SchoolSubject.Select(s => s.Name)),
            }).ToList();
            var view = new TeacherListViewModel
            {
                TeachersList = teacherViews,
            };
            return view;
        }

        public LessonListViewModel GetLessonView()
        {

            var lessons = _timetableRepository.GetLessons();
            var lessonView = lessons.Select(x => new LessonViewModel()
            {
                DayOfWeek = x.DayOfWeek.GetEnumDescription(),
                ClassName = x.Class.Number + x.Class.Name,
                ClassroomName = x.Classroom.Name,
                SchoolSubjectName = x.SchoolSubject.Name,
                TeacherName = $"{x.SchoolSubject.Teacher?.SurName} {x.SchoolSubject.Teacher?.Name} {x.SchoolSubject.Teacher?.Patronymic}",
                LessonNumber = x.LessonTime.LessonNumber,
                StartTime = x.LessonTime.StartTime,
                EndTime = x.LessonTime.EndTime,
                LessonId=x.LessonId
            })
                .OrderBy(x=>x.ClassName)
                .ThenBy(x=>x.LessonNumber)
                .ToList();

            var classess= _timetableRepository.GetClassList();
            var schoolSubject = _timetableRepository.GetSchoolSubjects().Where(x=>x.TeacherId!=null).ToList();
            var classrooms = _timetableRepository.GetClassrooms();
            var lessonTime = _timetableRepository.GetLessonTimes();
            var dayOfWeekView = Enum.DayOfWeek.GetValues(typeof(Enum.DayOfWeek)).Cast<Enum.DayOfWeek>()
                .Select(x=> new DayOfWeekView { Enum=x, Description=x.GetEnumDescription()}).ToList();
            var lessonListViewModel = new LessonListViewModel
            {
                Lessons = lessonView,
                Class = classess,
                DayOfWeek = dayOfWeekView,
                Classrooms = classrooms,
                LessonTimes =lessonTime,
                SchoolSubjects= schoolSubject
            };
            return lessonListViewModel;

        }

        public Teacher GetTeacherById(Guid Id)
        {
            return _timetableRepository.GetTeacherById(Id);
        }

        public void CreateOrUpdateTeacher(Teacher teacher)
        {
            _timetableRepository.CreateOrUpdateTeacher(teacher);
        }
        public void DeleteTeacher(Guid id)
        {
            _timetableRepository.DeleteTeacher(id);
        }
        public void RemoveTeacherOnSchoolSubject(Guid schoolSubjectId)
        {
            _timetableRepository.RemoveTeacherOnSchoolSubject(schoolSubjectId);
        }

        public void AddTeacherOnSchoolSubject(Guid id, Guid schoolSubjectId)
        {
            _timetableRepository.AddTeacherOnSchoolSubject(id,schoolSubjectId);
        }

        public EditTeacherViewModel GetEditTeachersView(Guid id)
        {
            var teacher = _timetableRepository.GetTeacherById(id);
            var schoolSubject = _timetableRepository.GetSchoolSubjects(true);
            var teacherViewModel = new EditTeacherViewModel()
            {
                Teacher = teacher,
                SchoolSubject= schoolSubject
            };
            return teacherViewModel;
        }

        public SchoolSubjectListViewModel GetSchoolSubjectView()
        {
            var schoolSubject = _timetableRepository.GetSchoolSubjects();
            var schoolSubjectView = schoolSubject.Select(x => new SchoolSubjectViewModel { Id=x.Id, 
                Name = x.Name, 
                TeacherName= $"{x.Teacher?.SurName} {x.Teacher?.Name} {x.Teacher?.Patronymic}"
            }).ToList();
            var teacher = _timetableRepository.GetTeachers();
            var schoolSubjectListViewModel = new SchoolSubjectListViewModel
            {
                SchoolSubjects = schoolSubjectView,
                Teachers = teacher
            };
            return schoolSubjectListViewModel;
        }

        public void CreateOrUpdateSchoolSubject(SchoolSubject schoolSubject)
        {
            _timetableRepository.CreateOrUpdateSchoolSubject(schoolSubject);

        }

        public void DeleteSchoolSubject(Guid id)
        {
           _timetableRepository.DeleteSchoolSubject(id);
        }

        public EditSchoolSubjectViewModel GetEditSchoolSubjectView(Guid id)
        {
            var schoolSubject = _timetableRepository.GetSchoolSubjectById(id);
            var teacher = _timetableRepository.GetTeachers();
            var schoolSubjectEditViewModel = new EditSchoolSubjectViewModel
            {
                SchoolSubject = schoolSubject,
                Teachers = teacher
            };
            return schoolSubjectEditViewModel;
        }

        public List<Lesson> GetTimetable(Guid? classId = null, Enum.DayOfWeek? dayOfWeek = null)
        {
            var timetables = _timetableRepository.GetLessons();
            if (classId == null)
                timetables = timetables.Where(x => x.ClassId == classId).ToList();
            if (dayOfWeek == null)
                timetables = timetables.Where(x => x.DayOfWeek == dayOfWeek).ToList();
            return timetables;
        }

        public List<Class> GetClassList()
        {
            var classList = _timetableRepository.GetClassList();
            return classList;
        }

        public string CreateClass(Class @class)
        {
            var result = _timetableRepository.FormClassName(@class.Number);
            if(!result.IsSuccsess)
            {
                return $"Превышено количество допустимых классов в {@class.Number} параллели"; ;
            }
            @class.Id = Guid.NewGuid();
            @class.Name = result.Message;
            _timetableRepository.CreateClass(@class);
            return $"{@class.Number}{@class.Name} класс успешно сформирован";
        }

      

        public void DeleteClass(Guid id)
        {
           _timetableRepository.DeleteClass(id);
        }

        public List<Classroom> GetClassrooms()
        {
         return _timetableRepository.GetClassrooms();
        }

        public void CreateOrUpdateClassroom(Classroom classroom)
        {
            _timetableRepository.CreateOrUpdateClassroom(classroom);
        }

        public void DeleteClassroom(Guid id)
        {
            _timetableRepository.DeleteClassroom(id);
        }

        public Result CreateOrUpdateLesson(Lesson timeTable)
        {
            timeTable.LessonTime= _timetableRepository.GetLessonTimeById(timeTable.LessonTimeId);
            return _timetableRepository.CreateOrUpdateLesson(timeTable);
        }

        public Result DeleteLesson(Guid id)
        {
            return _timetableRepository.DeleteLesson(id);
        }

        public ReportViewModel GetReportViewModel(List<Enum.DayOfWeek> dayOfWeeks, int? classParallelNumber)
         {
            FreeClassroomReportViewModel freeclassroom =null;
            List<ParallelInfoReportViewModel> parallellInfoReport = null; 
            if (dayOfWeeks != null)
                 freeclassroom =_timetableRepository.GetFreeClassroom(dayOfWeeks);

            var dayOfWeekView = Enum.DayOfWeek.GetValues(typeof(Enum.DayOfWeek)).Cast<Enum.DayOfWeek>()
              .Select(x => new DayOfWeekView { Enum = x, Description = x.GetEnumDescription() }).ToList();
            var activeClassParallel = _timetableRepository.GetClassList().GroupBy(x => x.Number).Select(x => x.Key).ToList();

            var busyTeacher = _timetableRepository.GetBusyTeacher();
            if(classParallelNumber!=null)
            {
                parallellInfoReport = _timetableRepository.GetParallelInfoReport(classParallelNumber.Value);
            }
            var classInfoReport = _timetableRepository.GetClassInfoReport();
            var reportViewModel = new ReportViewModel
            {
                DayOfWeek = dayOfWeekView,
                FreeClassRoom = freeclassroom,
                BusyTeacher = busyTeacher,
                ClassParallels = activeClassParallel,
                ClassInfoReport = classInfoReport,
                ParallelInfoReport=parallellInfoReport,
                ActiveDayOfWeeks=dayOfWeeks 
            };
            return reportViewModel;
        }

        public Classroom GetClassroomById(Guid id)
        {
            return _timetableRepository.GetClassroomById(id);
        }
    }
}