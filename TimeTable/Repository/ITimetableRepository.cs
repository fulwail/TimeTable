using TimeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTable.ViewModel;

namespace TimeTable.Repository
{
    public interface ITimetableRepository
    {
        List<Lesson> GetLessons();
        List<Teacher> GetTeachers();
        Teacher GetTeacherById(Guid id);
        List<SchoolSubject> GetSchoolSubjects(bool needFreeSchoolSubjects = false);
        BusyTeacherViewModel GetBusyTeacher();
        FreeClassroomReportViewModel GetFreeClassroom(List<Enum.DayOfWeek> dayOfWeeks);
        void CreateOrUpdateTeacher(Teacher teacher);
        void CreateOrUpdateSchoolSubject(SchoolSubject schoolSubject);
        void DeleteTeacher(Guid id);
        void RemoveTeacherOnSchoolSubject(Guid schoolSubjectId);
        void AddTeacherOnSchoolSubject(Guid id, Guid schoolSubjectId);
        void DeleteSchoolSubject(Guid id);
        SchoolSubject GetSchoolSubjectById(Guid id);
        List<Class> GetClassList();
        void CreateClass(Class @class);
        Result FormClassName(int number);
        void DeleteClass(Guid id);
        List<Classroom> GetClassrooms();
        void CreateOrUpdateClassroom(Classroom classroom);
        void DeleteClassroom(Guid id);
        Result DeleteLesson(Guid id);
        List<LessonTime> GetLessonTimes();
        Result CreateOrUpdateLesson(Lesson timeTable);
        LessonTime GetLessonTimeById(Guid id);
        List<ClassInfoReportViewModel> GetClassInfoReport();
        List<ParallelInfoReportViewModel> GetParallelInfoReport(int classParallelNumber);
        Classroom GetClassroomById(Guid id);
    }
}
