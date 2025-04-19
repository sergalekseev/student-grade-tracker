using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTracker.Infra.Models;
using StudentGradeTracker.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudentGradeTracker.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        //private Student _newStudent;
        private ObservableCollection<StudentDto> _students;
        //private IStudentsStore _studentsStore;
        private readonly IServerApi _serverApi;

        public MainWindowViewModel(IServerApi serverApi)
        {
            _serverApi = serverApi;

            //_studentsStore = studentsStore;
            //_newStudent = new();
            //_students = new(studentsStore.Students);

            //GradeValues = Enum.GetValues(typeof(Grade));
            //AddStudent = new RelayCommand<Student>(OnAddStudent);
            //RemoveStudent = new RelayCommand<Student>(OnRemoveStudent);
        }

        public Array GradeValues { get; }

        //public Student NewStudent
        //{
        //    get => _newStudent;
        //    set => SetProperty(ref _newStudent, value);
        //}

        public ObservableCollection<StudentDto> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        public ICommand AddStudent { get; }

        public ICommand RemoveStudent { get; set; }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            var students = await _serverApi.GetStudentsAsync();
            Students = [.. students];
        }

        //private void OnAddStudent(Student _)
        //{
        //    _studentsStore.AddStudent(NewStudent);
        //    Students.Add(NewStudent);
        //    NewStudent = new Student();
        //}

        //private void OnRemoveStudent(Student studentToRemove)
        //{
        //    _studentsStore.RemoveStudent(studentToRemove);
        //    Students.Remove(studentToRemove);
        //}

    }
}
