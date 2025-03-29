using StudentGradeTracker.Infra.Models;
using StudentGradeTracker.Infra.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudentGradeTracker.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Student _newStudent;
        private ObservableCollection<Student> _students;
        private IStudentsStore _studentsStore;

        public MainWindowViewModel(IStudentsStore studentsStore)
        {
            _studentsStore = studentsStore;
            _newStudent = new();
            _students = new(studentsStore.Students);
           
            GradeValues = Enum.GetValues(typeof(Grade));
            AddStudent = new RelayCommand<Student>(OnAddStudent);
            RemoveStudent = new RelayCommand<Student>(OnRemoveStudent);
        }

        public Array GradeValues { get; }

        public Student NewStudent
        {
            get => _newStudent;
            set => SetProperty(ref _newStudent, value);
        }

        public ObservableCollection<Student> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        public ICommand AddStudent { get; }

        public ICommand RemoveStudent { get; set; }

        private void OnAddStudent(Student _)
        {
            _studentsStore.AddStudent(NewStudent);
            Students.Add(NewStudent);
            NewStudent = new Student();
        }

        private void OnRemoveStudent(Student studentToRemove)
        {
            _studentsStore.RemoveStudent(studentToRemove);
            Students.Remove(studentToRemove);
        }

    }
}
