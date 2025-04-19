using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public interface ISubjectsStore
{
    public List<Subject> Subjects { get; }
    public List<StudentSubject> StudentSubjects { get; }
    public Subject AddSubject(Subject newSubject);
    public Subject RemoveSubject(Subject subjectToRemove);
    public StudentSubject AddStudentSubject(StudentSubject newStudentSubject);
    public StudentSubject RemoveStudentSubject(StudentSubject studentSubjectToRemove);
}
