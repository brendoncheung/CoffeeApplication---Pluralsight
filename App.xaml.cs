using System;
using System.Windows;
using CoffeeApplication.Data;
using CoffeeApplication.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection service = new();
            ConfigureService(service);
            _serviceProvider = service.BuildServiceProvider();
        }

        private void ConfigureService(ServiceCollection service)
        {
            service.AddTransient<MainWindow>();

            service.AddTransient<MainViewModel>();
            service.AddTransient<CustomersViewModel>();
            service.AddTransient<ProductViewModel>();
            service.AddTransient<ICustomerDataProvider, CustomerDataProvider>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
