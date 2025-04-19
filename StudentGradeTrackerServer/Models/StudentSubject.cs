namespace StudentGradeTrackerServer.Models;

public class StudentSubject
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }

    //refs 
    public Student Student { get; set; }
    public Subject Subject { get; set; }
}
