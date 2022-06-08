using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoffeeApplication.Commands;
using CoffeeApplication.Data;
using CoffeeApplication.Model;

namespace CoffeeApplication.ViewModel
{
    public class CustomersViewModel : ViewModelBase
    {
        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();
        public readonly ICustomerDataProvider CustomerDataProvider;

        public DelegateCommand AddCommand { get; }
        public DelegateCommand MoveNavigationCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public bool IsCustomerSelected => SelectedCustomer is not null;

        private CustomerItemViewModel? _selectedCustomer;
        private NavigationSide _navigationSide;


        public CustomersViewModel(ICustomerDataProvider customerDataProvider)
        {
            CustomerDataProvider = customerDataProvider;
            AddCommand = new DelegateCommand(Add);
            MoveNavigationCommand = new DelegateCommand(MoveNavigation);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);

        }



        public CustomerItemViewModel? SelectedCustomer { 
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsCustomerSelected));
                DeleteCommand.RaisedCanExecuteChanged();
            }
       }


        public NavigationSide NavigationSide { 
            get => _navigationSide; 
            set
            {
                _navigationSide = value;
                RaisePropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            if (Customers.Any())
            {
                return;
            }

            var customers = await CustomerDataProvider.GetAllAsync();

            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }

        }

        private void Delete(object? parameter)
        {
            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
        }

        private bool CanDelete(object? parameter)
        {
            return SelectedCustomer is not null;
        }

        private void MoveNavigation(object? parameter)
        {
            NavigationSide = NavigationSide == NavigationSide.LEFT ? 
                NavigationSide.RIGHT : 
                NavigationSide.LEFT;
        }

        private void Add(object? parameter)
        {
            var customer = new Customer { FirstName = "New" };
            var viewModel = new CustomerItemViewModel(customer);
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

    }

    public enum NavigationSide
    {
        LEFT,
        RIGHT
    }
}
