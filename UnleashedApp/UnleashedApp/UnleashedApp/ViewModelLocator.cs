using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Repositories.EmployeeRepositories;
using UnleashedApp.Repositories.HabitatRepositories;
using UnleashedApp.Repositories.RoomRepositories;
using UnleashedApp.Repositories.SpaceRepositories;
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
        private readonly ISpaceRepository _spaceRepository;
        private readonly IRoomRepository _roomRepository;

        public LoginViewModel LoginViewModel { get; }
        public MenuViewModel MenuViewModel { get; }
        public WhoIsWhoViewModel WhoIsWhoViewModel { get; }
        public EmployeeDetailViewModel EmployeeDetailViewModel { get; }
        public FloorplanViewModel FloorplanViewModel { get; }
        public RoomViewModel RoomViewModel { get; }
        public NameGameViewModel NameGameViewModel { get; }

        public static ViewModelLocator Instance => _instance ?? (_instance = new ViewModelLocator());

        private ViewModelLocator()
        {
            _navigationService = new NavigationService();
            _authenticationService = AuthenticationService.Instance;

            //repositories:
            _httpClientAdapter = new HttpClientAdapter();
            _employeeRepository = new EmployeeRepository(AuthenticationService.Instance, _httpClientAdapter);
            _habitatRepository = new HabitatRepository(AuthenticationService.Instance, _httpClientAdapter);
            _squadRepository = new SquadRepository(AuthenticationService.Instance, _httpClientAdapter);
            _spaceRepository = new SpaceRepository(AuthenticationService.Instance, _httpClientAdapter);
            _roomRepository = new RoomRepository(AuthenticationService.Instance, _httpClientAdapter);
            _authenticationRepository = new AuthenticationRepository(AuthenticationService.Instance, _httpClientAdapter, new AuthenticationHttpClientAdapter(AuthenticationService.Instance, _httpClientAdapter));

            //viewmodels:
            LoginViewModel = new LoginViewModel(_navigationService, _authenticationService, _authenticationRepository);
            MenuViewModel = new MenuViewModel(_navigationService, _authenticationService, _authenticationRepository);
            WhoIsWhoViewModel = new WhoIsWhoViewModel(_navigationService, _habitatRepository, _squadRepository);
            FloorplanViewModel = new FloorplanViewModel(_navigationService, _spaceRepository, _roomRepository);
            RoomViewModel = new RoomViewModel(_navigationService, _employeeRepository);
            EmployeeDetailViewModel = new EmployeeDetailViewModel();
            NameGameViewModel = new NameGameViewModel(_employeeRepository);
        }
    }
}
