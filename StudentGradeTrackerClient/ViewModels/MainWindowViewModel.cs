using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTracker.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace StudentGradeTracker.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private StudentCreateDto _newStudent;
        private ObservableCollection<StudentDto> _students;
        private readonly IServerApi _serverApi;
        private readonly NotificationsConnection _notificationsConnection;

        public MainWindowViewModel(IServerApi serverApi, 
            NotificationsConnection notificationsConnection)
        {
            _serverApi = serverApi;
            _notificationsConnection = notificationsConnection;
            _newStudent = new();

            AddStudent = new RelayCommand<StudentDto>(OnAddStudent);
            RemoveStudent = new RelayCommand<StudentDto>(OnRemoveStudent);
            OpenStudentDetails = new RelayCommand<StudentDto>(OnOpenStudentDetails);
        }

        public Array GradeValues { get; }

        public StudentCreateDto NewStudent
        {
            get => _newStudent;
            set => SetProperty(ref _newStudent, value);
        }

        public ObservableCollection<StudentDto> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        public ICommand AddStudent { get; }

        public ICommand RemoveStudent { get; set; }

        public ICommand OpenStudentDetails { get; set; }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            _notificationsConnection.NewStudentUpdateReceived -= OnNewStudentUpdateReceived;
            _notificationsConnection.NewStudentUpdateReceived += OnNewStudentUpdateReceived;

            var students = await _serverApi.GetStudentsAsync();
            Students = [.. students];
        }

        public override Task OnDisappearing()
        {
            _notificationsConnection.NewStudentUpdateReceived -= OnNewStudentUpdateReceived;

            return base.OnDisappearing();
        }

        private void OnNewStudentUpdateReceived(object? sender, StudentDto newStudent)
        {
            App.Current.Dispatcher.Invoke(() => Students.Add(newStudent));
        }

        private async void OnAddStudent(StudentDto _)
        {
            try
            {
                var newStudent = await _serverApi.CreateStudentAsync(NewStudent);

                //if (newStudent is not null)
                //{
                //    await _notificationsConnection.SendNewStudentUpdateAsync(
                //        NewStudent, CancellationToken.None);
                //    //Students.Add(newStudent);
                //}
            }
            catch { }
            finally
            {
                NewStudent = new();
            }
        }

        private async void OnRemoveStudent(StudentDto studentToRemove)
        {
            try
            {
                var newStudent = await _serverApi.RemoveStudentAsync(studentToRemove.IdCard);

                if (newStudent is not null)
                {
                    Students.Remove(studentToRemove);
                }
                
            }
            catch { }
        }

        private void OnOpenStudentDetails(StudentDto student)
        {
            StudentDetails detailsWindow = new StudentDetails(student);
            detailsWindow.Show();
        }

    }
}
