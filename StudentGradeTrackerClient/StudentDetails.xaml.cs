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

            // 1. pass student object to view model
            // 2. run the initialization (call API)
        }
    }
}
