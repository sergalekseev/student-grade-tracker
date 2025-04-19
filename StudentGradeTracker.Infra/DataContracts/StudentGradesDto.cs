namespace StudentGradeTracker.Infra.DataContracts;

public class StudentGradesDto
{
    public StudentDto Student { get; set; }
    public List<SubjectGradeDto> Grades { get; set; }
}
