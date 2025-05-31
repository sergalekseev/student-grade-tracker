using Microsoft.AspNetCore.SignalR.Client;
using StudentGradeTracker.Infra.DataContracts;

namespace StudentGradeTracker.Services;

public class NotificationsConnection
{
    public event EventHandler<StudentDto> NewStudentUpdateReceived;

    HubConnection _connection;
    private bool _isInitialized;

    public NotificationsConnection()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("http://127.0.0.1:6570/notifications")
            .WithAutomaticReconnect()
            .Build();
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_isInitialized) return;

        _connection.On<StudentDto>(
            NotificationHubMethods.Connection.ReceiveNewStudentUpdate,
            OnNewStudentUpdateReceived);

        await _connection.StartAsync(cancellationToken);

        _isInitialized = true;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _connection.Remove(NotificationHubMethods.Connection.ReceiveNewStudentUpdate);

        await _connection.StopAsync(cancellationToken);

        _isInitialized = false;
    }

    public Task SendNewStudentUpdateAsync(StudentCreateDto newStudent, 
        CancellationToken cancellationToken)
    {
        return _connection.SendAsync(
            NotificationHubMethods.Hub.NotifyNewStudent,
            newStudent,
            cancellationToken);
    }

    private void OnNewStudentUpdateReceived(StudentDto newStudent)
    {
        NewStudentUpdateReceived?.Invoke(this, newStudent);
    }
}
