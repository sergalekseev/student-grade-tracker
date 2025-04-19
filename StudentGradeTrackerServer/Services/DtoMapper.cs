using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public static class DtoMapper
{

    public static StudentDto StudentToDto(Student student)
    {
        return new StudentDto()
        {
            IdCard = student.IdCard,
            Name = student.Name,
            Grade = student.Grade,
        };
    }

    public static StudentDto ToDto(this Student student)
    {
        return StudentToDto(student);
    }

    public static SubjectDto SubjectToDto(Subject subject)
    {
        return new SubjectDto()
        {
            Name = subject.Name,
            Description = subject.Description
        };
    }

    public static SubjectDto ToDto(this Subject subject)
    {
        return SubjectToDto(subject);
    }

    public static GradeDto GradeToDto(StudentSubjectGrade grade)
    {
        return new GradeDto()
        {
            Timestamp = grade.Timestamp,
            Grade = grade.Grade
        };
    }

    public static GradeDto ToDto(this StudentSubjectGrade grade)
    {
        return GradeToDto(grade);
    }
}
