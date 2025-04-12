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
        return new StudentDto()
        {
            IdCard = student.IdCard,
            Name = student.Name,
            Grade = student.Grade,
        };
    }
}
