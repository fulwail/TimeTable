using TimeTable.Models;
using TimeTable.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable.Services
{
    public interface ITimetableServices
    {
        LessonListViewModel GetLessonView();
        TeacherListViewModel GetTeachersView();
        EditTeacherViewModel GetEditTeachersView(Guid id);
        Teacher GetTeacherById(Guid id);
        FreeClassroomReportViewModel GetFreeClassroom(List<Enum.DayOfWeek> dayOfWeeks);
        void CreateOrUpdateTeacher(Teacher teacher);
        string CreateClass(Class @class);
        Result CreateOrUpdateLesson(Lesson timeTable);
        void CreateOrUpdateClassroom(Classroom classroom);
        void CreateOrUpdateSchoolSubject(SchoolSubject schoolSubject);
        Result DeleteLesson(Guid id);
        EditSchoolSubjectViewModel GetEditSchoolSubjectView(Guid id);
        Classroom GetClassroomById(Guid id);
        void DeleteTeacher(Guid id);
        void DeleteClass(Guid id);
        void DeleteClassroom(Guid id);
        void DeleteSchoolSubject(Guid id);
        void RemoveTeacherOnSchoolSubject(Guid schoolSubjectId);
        void AddTeacherOnSchoolSubject(Guid id, Guid schoolSubjectId);
       SchoolSubjectListViewModel GetSchoolSubjectView();
        List<Class> GetClassList();
        List<Classroom> GetClassrooms();
        List<Lesson> GetTimetable(Guid? classId = null, Enum.DayOfWeek? dayOfWeek = null);
        ReportViewModel GetReportViewModel(List<Enum.DayOfWeek> dayOfWeeks, int? classParallelNumber);
    }

}
