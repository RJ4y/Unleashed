using UnleashedApp.Contracts;
using UnleashedApp.Repositories.EmployeeRepositories;
using UnleashedApp.Repositories.HabitatRepositories;
using UnleashedApp.Repositories.RoomRepositories;
using UnleashedApp.Repositories.SpaceRepositories;
using UnleashedApp.Repositories.SquadRepositories;
using UnleashedApp.Repositories.TrainingRepository;
using UnleashedApp.Services;
using UnleashedApp.ViewModels;

namespace UnleashedApp
{
    public class ViewModelLocator
    {
        private static ViewModelLocator _instance;

        public MenuViewModel MenuViewModel { get; }
        public WhoIsWhoViewModel WhoIsWhoViewModel { get; }
        public TrainingViewModel TrainingViewModel { get; }
        public EmployeeDetailViewModel EmployeeDetailViewModel { get; }
        public FloorplanViewModel FloorplanViewModel { get; }
        public RoomViewModel RoomViewModel { get; }
        public NameGameViewModel NameGameViewModel { get; }

        public static ViewModelLocator Instance => _instance ?? (_instance = new ViewModelLocator());

        private ViewModelLocator()
        {
            INavigationService navigationService = new NavigationService();

            ISpaceRepository spaceRepository = new SpaceRepository();
            IRoomRepository roomRepository = new RoomRepository();
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            IHabitatRepository habitatRepository = new HabitatRepository();
            ISquadRepository squadRepository = new SquadRepository();
            ITrainingRepository trainingRepository = new TrainingRepository();

            MenuViewModel = new MenuViewModel(navigationService);
            WhoIsWhoViewModel = new WhoIsWhoViewModel(navigationService, habitatRepository, squadRepository);
            FloorplanViewModel = new FloorplanViewModel(navigationService, spaceRepository, roomRepository);
            TrainingViewModel = new TrainingViewModel(trainingRepository);
            RoomViewModel = new RoomViewModel(navigationService, employeeRepository);
            EmployeeDetailViewModel = new EmployeeDetailViewModel();
            NameGameViewModel = new NameGameViewModel(employeeRepository);
        }
    }
}