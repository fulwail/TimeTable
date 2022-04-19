SELECT Classroom.Id,Classroom.Name, LessonsCount
FROM 
(
SELECT top 1 Lesson.ClassroomId, count(*) as LessonsCount
FROM [dbo].Lesson
Where Lesson.DayOfWeek In({0})
GROUP BY Lesson.ClassroomId
order by LessonsCount 
) AS Class,dbo.Classroom
where Classroom.Id = Class.ClassroomId;