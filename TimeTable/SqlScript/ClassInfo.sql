SELECT ClassInfo.ClassId,Class.Number, Class.Name, ClassInfo.LessonsCount, ClassInfo.TeacherCount
FROM 
(
SELECT Lesson.ClassId,
count(case when SchoolSubject.Id = Lesson.SchoolSubjectId then SchoolSubject.Id  end) as LessonsCount, 
count(Distinct (case when SchoolSubject.Id = Lesson.SchoolSubjectId then SchoolSubject.TeacherId  end)) as TeacherCount
FROM [dbo].Lesson, [dbo].SchoolSubject
GROUP BY Lesson.ClassId

) AS ClassInfo,
dbo.Class
where Class.Id = ClassInfo.ClassId;

