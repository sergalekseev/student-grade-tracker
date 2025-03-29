using StudentGradeTracker.Infra.Models;

namespace StudentGradeTracker.Infra.DataContracts;

public class BaseChangeStudentRequest
{
    public Grade Grade { get; set; }
}

public class ChangeStudentRequest : BaseChangeStudentRequest
{
    public int Id { get; set; }
}
