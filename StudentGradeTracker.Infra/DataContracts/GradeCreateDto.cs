using StudentGradeTracker.Infra.Models;

namespace StudentGradeTracker.Infra.DataContracts;

public class GradeCreateDto
{
    public int SubjectId { get; set; }
    public DateTime Timestamp { get; set; }
    public Grade Grade { get; set; }
}
