using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public static class DtoMapper
{

    public static StudentDto StudentToDto(Student student) => new()
    {
        IdCard = student.IdCard,
        Name = student.Name,
    };

    public static StudentDto ToDto(this Student student) => StudentToDto(student);

    public static async Task<StudentDto> ToDtoAsync(this Task<Student> studentTask)
    {
        var student = await studentTask;
        return student.ToDto();
    }

    public static SubjectDto SubjectToDto(Subject subject) => new()
    { 
        Name = subject.Name,
        Description = subject.Description
    };

    public static SubjectDto ToDto(this Subject subject) => SubjectToDto(subject);

    public static async Task<SubjectDto> ToDtoAsync(this Task<Subject> subjectTask)
    {
        var subject = await subjectTask;
        return subject.ToDto();
    }

    public static GradeDto GradeToDto(StudentSubjectGrade grade) => new()
    {
        Timestamp = grade.Timestamp,
        Grade = grade.Grade
    };

    public static GradeDto ToDto(this StudentSubjectGrade grade) => GradeToDto(grade);

    public static async Task<GradeDto> ToDtoAsync(this Task<StudentSubjectGrade> gradeTask)
    {
        var grade = await gradeTask;
        return grade.ToDto();
    }
}
