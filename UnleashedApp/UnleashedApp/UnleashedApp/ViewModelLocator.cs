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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHabitatRepository _habitatRepository;
        private readonly ISquadRepository _squadRepository;
        private readonly ISpaceRepository _spaceRepository;
        private readonly IRoomRepository _roomRepository;

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

            _spaceRepository = new SpaceRepository();
            _roomRepository = new RoomRepository();
            _employeeRepository = new EmployeeRepository();
            _habitatRepository = new HabitatRepository();
            _squadRepository = new SquadRepository();

            MenuViewModel = new MenuViewModel(_navigationService);
            WhoIsWhoViewModel = new WhoIsWhoViewModel(_navigationService, _habitatRepository, _squadRepository);
            FloorplanViewModel = new FloorplanViewModel(_navigationService, _spaceRepository, _roomRepository);
            RoomViewModel = new RoomViewModel(_navigationService, _employeeRepository);
            EmployeeDetailViewModel = new EmployeeDetailViewModel();
            NameGameViewModel = new NameGameViewModel(_employeeRepository);
        }
    }
}
