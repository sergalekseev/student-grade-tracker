using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentGradeTracker.Services;
using StudentGradeTracker.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace StudentGradeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            var builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices((hostContext, services) =>
            {
                // services
                services.AddSingleton<IServerApi, ServerApi>();

                // view models
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<StudentDetailsViewModel>();

                // views
                services.AddSingleton<MainWindow>();
            });


            AppHost = builder.Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                await AppHost!.StartAsync();

                var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            try
            {
                if (AppHost != null)
                {
                    await AppHost.StopAsync(TimeSpan.FromSeconds(5));
                    AppHost.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            base.OnExit(e);
        }
    }

}
