using StudentGradeTracker.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentGradeTracker.Components
{
    /// <summary>
    /// Interaction logic for StudentItemControl.xaml
    /// </summary>
    public partial class StudentItemControl : UserControl
    {
        public static readonly DependencyProperty StudentProperty =
            DependencyProperty.Register(
                nameof(Student), 
                typeof(Student), 
                typeof(StudentItemControl));

        public static readonly DependencyProperty RemoveStudentCommandProperty =
            DependencyProperty.Register(
                nameof(RemoveStudentCommand), 
                typeof(ICommand), 
                typeof(StudentItemControl));

        public StudentItemControl()
        {
            InitializeComponent();
            DataContextChanged += StudentItemControlDataContextChanged;
        }

        private void StudentItemControlDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var t = DataContext;
            if (DataContext is Student)
            {
                //DataContext = this;
            }
        }

        public Student Student
        {
            get => (Student)GetValue(StudentProperty);
            set => SetValue(StudentProperty, value);
        }

        public ICommand RemoveStudentCommand
        {
            get => (ICommand)GetValue(RemoveStudentCommandProperty);
            set => SetValue(RemoveStudentCommandProperty, value);
        }
    }
}
