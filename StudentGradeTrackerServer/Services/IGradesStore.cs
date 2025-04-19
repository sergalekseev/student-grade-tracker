using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public interface IGradesStore
{
    public List<StudentSubjectGrade> Grades { get; }
    public StudentSubjectGrade AddGrade(StudentSubjectGrade studentSubjectGrade);
    public StudentSubjectGrade RemoveGrade(StudentSubjectGrade studentSubjectGrade);
}
