using StudentGradeTracker.Infra.DataContracts;

namespace StudentGradeTracker.Models;

public class SubjectGrades
{
    public string SubjectName { get; set; }
    public List<GradeDto> Grades { get; set; } = new();
}
