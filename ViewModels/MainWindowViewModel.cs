using StudentGradeTracker.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudentGradeTracker.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private Student _newStudent;
        private ObservableCollection<Student> _students;

        public MainWindowViewModel()
        {
            _newStudent = new();
            _students = new()
            {
                new Student { Name = "Student 1", Grade = Grade.Excellent },
                new Student { Name = "Student 2", Grade = Grade.Excellent },
                new Student { Name = "Student 3", Grade = Grade.Poor },
                new Student { Name = "Student 4", Grade = Grade.Fail },
                new Student { Name = "Student 5", Grade = Grade.Good },
                new Student { Name = "Student 6", Grade = Grade.Good },
                new Student { Name = "Student 7", Grade = Grade.Poor }
            };
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
            Students.Add(NewStudent);
            NewStudent = new Student();
        }

        private void OnRemoveStudent(Student studentToRemove)
        {
            Students.Remove(studentToRemove);
        }

    }
}
