using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTracker.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudentGradeTracker.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private StudentCreateDto _newStudent;
        private ObservableCollection<StudentDto> _students;
        private readonly IServerApi _serverApi;

        public MainWindowViewModel(IServerApi serverApi)
        {
            _serverApi = serverApi;
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

            var students = await _serverApi.GetStudentsAsync();
            Students = [.. students];
        }

        private async void OnAddStudent(StudentDto _)
        {
            try
            {
                var newStudent = await _serverApi.CreateStudentAsync(NewStudent);

                if (newStudent is not null)
                {
                    Students.Add(newStudent);
                }
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
