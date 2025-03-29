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
                typeof(StudentItemControl),
                new PropertyMetadata(null, HandlePropertyChanged));

        public static readonly DependencyProperty RemoveStudentCommandProperty =
            DependencyProperty.Register(
                nameof(RemoveStudentCommand), 
                typeof(ICommand), 
                typeof(StudentItemControl),
                new PropertyMetadata(null, HandlePropertyChanged));

        public StudentItemControl()
        {
            InitializeComponent();
            // DataContext = this; <-- It's bad practice
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

        protected static void HandlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine($"{nameof(StudentItemControl)}.{nameof(HandlePropertyChanged)} - " +
                $"property {e.Property.Name} was changed, old value: {e.OldValue}, new value {e.NewValue}");
        }
    }
}
