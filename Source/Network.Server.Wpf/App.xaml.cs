using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Network.Server.Wpf.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Network.Server.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: Application
    {
        private readonly IHost _host;

        public App() 
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<MainWindow>()
                        .AddSingleton<MainViewModel>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow window = _host.Services.GetRequiredService<MainWindow>(); 
            window.Show();
            
            base.OnStartup(e);

        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync().Wait();
            base.OnExit(e);
        }
    }

}
