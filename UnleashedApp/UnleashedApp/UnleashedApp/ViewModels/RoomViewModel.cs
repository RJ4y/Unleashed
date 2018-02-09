using System.Windows.Input;
using UnleashedApp.Contracts;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.EmployeeRepositories;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class RoomViewModel : ViewModelBase, IRoomViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand EmployeeDetailCommand { get; set; }
        public IEmployeeRepository EmployeeRepository { get; }
        public Employee SelectedEmployee { get; set; }

        public RoomViewModel(INavigationService navigationService, IEmployeeRepository employeeRepository)
        {
            _navigationService = navigationService;
            EmployeeRepository = employeeRepository;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            EmployeeDetailCommand = new Command(async () =>
            {
                EmployeeDetailView page = new EmployeeDetailView
                {
                    BindingContext = SelectedEmployee
                };
                await _navigationService.PushAsync(page);
            });
        }
    }
}