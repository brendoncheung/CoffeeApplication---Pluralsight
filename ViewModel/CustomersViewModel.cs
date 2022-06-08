using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoffeeApplication.Data;
using CoffeeApplication.Model;

namespace CoffeeApplication.ViewModel
{
    public class CustomersViewModel
    {
        public ObservableCollection<Customer> Customers { get; } = new();
        public readonly ICustomerDataProvider CustomerDataProvider;

        public CustomersViewModel(ICustomerDataProvider customerDataProvider)
        {
            CustomerDataProvider = customerDataProvider;
        }

        public async Task LoadAsync()
        {
            if(Customers.Any())
            {
                return;
            }

            var customers = await CustomerDataProvider.GetAllAsync();

            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(customer);
                }
            }

        }
    }
}
