SELECT Top 1 Teacher.Id,Teacher.SurName,Teacher.Name, Teacher.Patronymic, CountSchoolSubject
FROM 
(
SELECT   [SchoolSubject].TeacherId, count(*) as CountSchoolSubject
FROM [dbo].[SchoolSubject]
GROUP BY [SchoolSubject].TeacherId

) AS PopularTeacher,dbo.Teacher
where Teacher.Id = PopularTeacher.TeacherId
order by CountSchoolSubject desc