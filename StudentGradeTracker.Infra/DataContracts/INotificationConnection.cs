namespace StudentGradeTracker.Infra.DataContracts;

public interface INotificationConnection
{
    Task ReceiveNewStudentUpdate(StudentDto newStudent);

    Task ReceiveMessage(Message message);
}
