using StudentGradeTracker.Infra.Models;

namespace StudentGradeTracker.Infra.DataContracts;

public class GradeDto
{
    public DateTime Timestamp { get; set; }
    public Grade Grade { get; set; }
}
