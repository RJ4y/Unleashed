using UnleashedApp.Authentication;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Repositories.EmployeeRepositories;
using UnleashedApp.Repositories.HabitatRepositories;
using UnleashedApp.Repositories.SquadRepositories;
using UnleashedApp.ViewModels;

namespace UnleashedApp
{
    public class ViewModelLocator
    {
        private static ViewModelLocator _instance;
        private readonly INavigationService _navigationService;
        private readonly AuthenticationService _authenticationService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHabitatRepository _habitatRepository;
        private readonly ISquadRepository _squadRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public LoginViewModel LoginViewModel { get; }
        public MenuViewModel MenuViewModel { get; }
        public WhoIsWhoViewModel WhoIsViewViewModel { get; }
        public EmployeeDetailViewModel EmployeeDetailViewModel { get; }

        public static ViewModelLocator Instance => _instance ?? (_instance = new ViewModelLocator());

        public ViewModelLocator()
        {
            //services:
            _navigationService = new NavigationService();
            _authenticationService = AuthenticationService.Instance;

            //repositories:
            _employeeRepository = new EmployeeRepository();
            _habitatRepository = new HabitatRepository();
            _squadRepository = new SquadRepository();
            _authenticationRepository = new AuthenticationRepository(new AuthenticationHttpClientAdapter());

            //viewmodels:
            LoginViewModel = new LoginViewModel(_navigationService, _authenticationService, _authenticationRepository);
            MenuViewModel = new MenuViewModel(_navigationService, _authenticationService, _authenticationRepository);
            WhoIsViewViewModel = new WhoIsWhoViewModel(_navigationService, _habitatRepository, _squadRepository);
            //EmployeeDetailViewModel = new EmployeeDetailViewModel();
        }
    }
}
