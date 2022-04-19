Select Parallel.Id, 
Parallel.Number as Number,
Parallel.ClassName as Name,
STRING_AGG ( Parallel.TeacherSurName+' '+Parallel.TeacherName+' '+Parallel.TeacherPatronymic, ', ')  As  Teachers, 
STRING_AGG (TeacherSchoolSubject, ', ' )  As  SchoolSubjects
from(
SELECT  Class.Id, Class.Number, Class.Name as ClassName,Teacher.Name as TeacherName, Teacher.Patronymic as TeacherPatronymic,Teacher.SurName  as TeacherSurname,
STRING_AGG (SchoolSubject.Name, ', ' )  As  TeacherSchoolSubject
FROM Class, (select  DISTINCT Lesson.SchoolSubjectId, Lesson.ClassId  FROM Lesson) AS Timetable, SchoolSubject, Teacher
Where Class.Id=Timetable.ClassId AND SchoolSubject.Id = Timetable.SchoolSubjectId  And Teacher.Id = SchoolSubject.TeacherId 
GROUP BY Class.Id, Class.Number,  Class.Name,Teacher.Name, Teacher.Patronymic,Teacher.SurName
) as Parallel
Where Number={0}
GROUP BY Parallel.Id,Parallel.Number, Parallel.ClassName

