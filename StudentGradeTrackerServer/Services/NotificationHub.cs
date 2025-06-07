using Microsoft.AspNetCore.SignalR;
using StudentGradeTracker.Infra.DataContracts;

namespace StudentGradeTrackerServer.Services
{
    public class NotificationHub : Hub<INotificationConnection>
    {
        class PlayerState
        {
            public string Id { get; set; }
            public bool IsOnline { get; set; }
        }

        private Dictionary<string, PlayerState> Users { get; set; } = new();

        public NotificationHub() { }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            // identify user
            Users.Add(Context.ConnectionId, new PlayerState()
            {
                Id = Context.ConnectionId,
            });

            // add to some group
            //await Groups.AddToGroupAsync(Context.ConnectionId, "GroupId1");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // remove user
            Users.Remove(Context.ConnectionId);
            //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "GroupId1");

            await base.OnDisconnectedAsync(exception);
        }

        public Task SendMessage(Message message)
        {
            return Clients.All.ReceiveMessage(message);
        }

        public Task NotifyNewStudent(StudentCreateDto newStudent)
        {
            var student = new StudentDto()
            {
                IdCard = newStudent.IdCard,
                Name = newStudent.Name,
            };

            return Clients.Others.ReceiveNewStudentUpdate(student);       
        }

        public async Task ImOnline()
        {
            Users[Context.ConnectionId].IsOnline = true;

            // find another player

            var anotherUser = Users.FirstOrDefault(x =>
                x.Key != Context.ConnectionId && x.Value.IsOnline);

            if (anotherUser.Value is not null)
            {
                // remove from intiial/general group

                //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "GroupId1");
                //await Groups.RemoveFromGroupAsync(anotherUser.Key, "GroupId1");

                // add to new group - only 2 users
                //await Groups.AddToGroupAsync(Context.ConnectionId, "GroupId2");
                //await Groups.AddToGroupAsync(anotherUser.Key, "GroupId2");
            }
        }

        public Task SomeUserAction()
        {
            // check and update game state

            // option 1 - using connection id
            //Clients.User("connectionId").ReceiveMessage(null);

            // option 2 - using group
            //Clients.OthersInGroup("Game1").ReceiveMessage(null);

            return Task.CompletedTask;
        }
    }
}
