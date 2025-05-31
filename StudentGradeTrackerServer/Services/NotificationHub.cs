using Microsoft.AspNetCore.SignalR;
using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services
{
    public class NotificationHub : Hub
    {
        public NotificationHub() { }

        public Task NotifyNewStudent(StudentCreateDto newStudent, CancellationToken cancellationToken)
        {
            var student = new StudentDto()
            {
                IdCard = newStudent.IdCard,
                Name = newStudent.Name,
            };

            return Clients.Others.SendAsync(
                NotificationHubMethods.Connection.ReceiveNewStudentUpdate,
                student,
                cancellationToken);
        }
    }
}
