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
        private static ViewModelLocator _instance;
        private readonly INavigationService _navigationService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHabitatRepository _habitatRepository;
        private readonly ISquadRepository _squadRepository;

        public MenuViewModel MenuViewModel { get; }
        public WhoIsWhoViewModel WhoIsViewViewModel { get; }
        public EmployeeDetailViewModel EmployeeDetailViewModel { get; }

        public static ViewModelLocator Instance => _instance ?? (_instance = new ViewModelLocator());

        private ViewModelLocator()
        {
            //services:
            _navigationService = new NavigationService();

            //repositories:
            _employeeRepository = new EmployeeRepository();
            _habitatRepository = new HabitatRepository();
            _squadRepository = new SquadRepository();

            //viewmodels:
            MenuViewModel = new MenuViewModel(_navigationService);
            WhoIsViewViewModel = new WhoIsWhoViewModel(_navigationService, _habitatRepository, _squadRepository);
            EmployeeDetailViewModel = new EmployeeDetailViewModel();
        }
    }
}
