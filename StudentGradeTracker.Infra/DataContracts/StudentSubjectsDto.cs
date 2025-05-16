namespace StudentGradeTracker.Infra.DataContracts;

public class StudentSubjectsDto
{
    public StudentDto Student { get; set; }
    public List<SubjectDto> Subjects { get; set; }
}
