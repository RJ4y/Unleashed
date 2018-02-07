using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories;
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
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHabitatRepository _habitatRepository;
        private readonly ISquadRepository _squadRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IHttpClientAdapter _httpClientAdapter;

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
            _httpClientAdapter = new HttpClientAdapter();
            _employeeRepository = new EmployeeRepository(AuthenticationService.Instance, _httpClientAdapter);
            _habitatRepository = new HabitatRepository(AuthenticationService.Instance, _httpClientAdapter);
            _squadRepository = new SquadRepository(AuthenticationService.Instance, _httpClientAdapter);
            _authenticationRepository = new AuthenticationRepository(AuthenticationService.Instance, _httpClientAdapter, new AuthenticationHttpClientAdapter(AuthenticationService.Instance, _httpClientAdapter));

            //viewmodels:
            LoginViewModel = new LoginViewModel(_navigationService, _authenticationService, _authenticationRepository);
            MenuViewModel = new MenuViewModel(_navigationService, _authenticationService, _authenticationRepository);
            WhoIsViewViewModel = new WhoIsWhoViewModel(_navigationService, _habitatRepository, _squadRepository);
            //EmployeeDetailViewModel = new EmployeeDetailViewModel();
        }
    }
}
