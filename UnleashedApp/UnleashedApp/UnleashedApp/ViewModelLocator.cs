using UnleashedApp.Repositories.EmployeeRepositories;
using UnleashedApp.Repositories.HabitatRepositories;
using UnleashedApp.Repositories.SquadRepositories;
using UnleashedApp.Services;
using UnleashedApp.ViewModels;
using Xamarin.Forms;

namespace UnleashedApp
{
    public class ViewModelLocator
    {
        //singelton
        public static readonly ViewModelLocator Instance = new ViewModelLocator();

        public MenuViewModel MenuViewModel { get; }
        public WhoIsWhoViewModel WhoIsViewViewModel { get; }
        public EmployeeDetailViewModel EmployeeDetailViewModel { get; }

        public ViewModelLocator()
        {
            //services:
            IMessagingCenter messengerService = new MessengerService();
            INavigationService navigationService = new NavigationService();

            //repositories:
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            IHabitatRepository habitatRepository = new HabitatRepository();
            ISquadRepository squadRepository = new SquadRepository();

            //viewmodels:
            MenuViewModel = new MenuViewModel(messengerService, navigationService);
            WhoIsViewViewModel = new WhoIsWhoViewModel(navigationService);
            EmployeeDetailViewModel = new EmployeeDetailViewModel();
        }
    }
}
