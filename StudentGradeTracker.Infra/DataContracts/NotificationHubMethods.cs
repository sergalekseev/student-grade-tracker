namespace StudentGradeTracker.Infra.DataContracts;

public class NotificationHubMethods
{
    public class Hub
    {
        public const string NotifyNewStudent = "NotifyNewStudent";
    }

    public class Connection
    {
        public const string ReceiveNewStudentUpdate = "ReceiveNewStudentUpdate";
    }
}
