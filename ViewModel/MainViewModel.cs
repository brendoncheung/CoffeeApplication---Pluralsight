using CoffeeApplication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeApplication.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public CustomersViewModel CustomerViewModel { get; }
        public ProductViewModel ProductViewModel {get ;}
        private ViewModelBase? _selectViewModel;
        public MainViewModel(CustomersViewModel customersViewModel, ProductViewModel productViewModel)
        {
            CustomerViewModel = customersViewModel;
            ProductViewModel = productViewModel;
            SelectedViewModel = CustomerViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }

        public ViewModelBase? SelectedViewModel
        {
            get { return _selectViewModel; }
            set { _selectViewModel = value; RaisePropertyChanged(); }
        }

        public DelegateCommand SelectViewModelCommand { get; }

        public async override Task LoadAsync()
        {
            if(SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
    }
}
