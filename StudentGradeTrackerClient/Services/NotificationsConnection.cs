using Microsoft.AspNetCore.SignalR.Client;
using StudentGradeTracker.Infra.DataContracts;

namespace StudentGradeTracker.Services;

public class NotificationsConnection : INotificationConnection
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
            nameof(ReceiveNewStudentUpdate),
            ReceiveNewStudentUpdate);

        _connection.On<IncomingMessage>(
            nameof(ReceiveMessage),
            ReceiveMessage);

        await _connection.StartAsync(cancellationToken);

        _isInitialized = true;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _connection.Remove(nameof(ReceiveNewStudentUpdate));
        _connection.Remove(nameof(ReceiveMessage));

        await _connection.StopAsync(cancellationToken);

        _isInitialized = false;
    }

    public Task SendNewStudentUpdateAsync(StudentCreateDto newStudent, 
        CancellationToken cancellationToken)
    {
        return _connection.SendAsync(
            NotificationHubMethods.NotifyNewStudent,
            newStudent,
            cancellationToken);
    }

    public Task ReceiveNewStudentUpdate(StudentDto newStudent)
    {
        NewStudentUpdateReceived?.Invoke(this, newStudent);
        return Task.CompletedTask;
    }

    public Task ReceiveMessage(Message message)
    {
        return message.Name switch
        {
            NotificationHubMessages.NewStudentUpdate =>
                ReceiveNewStudentUpdate(message.Payload as StudentDto),
            _ => Task.CompletedTask
        };
    }
}
