using Microsoft.Extensions.DependencyInjection;
using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTracker.ViewModels;
using System.Windows;

namespace StudentGradeTracker
{
    /// <summary>
    /// Interaction logic for StudentDetails.xaml
    /// </summary>
    public partial class StudentDetails : Window
    {
        private StudentDetailsViewModel _viewModel;

        public StudentDetails(StudentDto student)
        {
            InitializeComponent();
            DataContext = _viewModel = 
                App.AppHost.Services.GetService<StudentDetailsViewModel>();

            _viewModel.Student = student;
            _ = _viewModel.InitializeAsync();
        }
    }
}
