using StudentGradeTracker.Infra.Models;
using StudentGradeTracker.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace StudentGradeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = _viewModel = viewModel;
        }

        protected override void OnActivated(EventArgs e)
        {
            Task.Delay(3000).ContinueWith(t =>
            {
                _ = _viewModel.OnAppearing();
            }); 

            base.OnActivated(e);
        }
    }
}