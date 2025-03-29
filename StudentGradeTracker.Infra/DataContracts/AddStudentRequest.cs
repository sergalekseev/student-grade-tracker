using StudentGradeTracker.Infra.Models;

namespace StudentGradeTracker.Infra.DataContracts;

public class AddStudentRequest
{
    public string Name { get; set; }
    public Grade Grade { get; set; }
}
