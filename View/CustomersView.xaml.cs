using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoffeeApplication.Data;
using CoffeeApplication.ViewModel;

namespace CoffeeApplication.View
{
    /// <summary>
    /// Interaction logic for CustomersView.xaml
    /// </summary>
    public partial class CustomersView : UserControl
    {
        private CustomersViewModel _viewModel;

        public CustomersView()
        {
            InitializeComponent();

            _viewModel = new CustomersViewModel(new CustomerDataProvider());
            DataContext = _viewModel;
            Loaded += CustomersView_Loaded;
        }

        public async void CustomersView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
