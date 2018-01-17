using UnleashedApp.Repositories.HabitatRepositories;
using UnleashedApp.Repositories.SquadRepositories;
using UnleashedApp.ViewModels;

namespace UnleashedApp
{
    public class ViewModelLocator
    {
        private static ViewModelLocator _instance;
        private readonly INavigationService _navigationService;

        private readonly IHabitatRepository _habitatRepository;
        private readonly ISquadRepository _squadRepository;

        public MenuViewModel MenuViewModel { get; }
        public WhoIsWhoViewModel WhoIsViewViewModel { get; }
        public FloorplanViewModel FloorplanViewModel { get; }
        public RoomViewModel RoomViewModel { get; }

        public static ViewModelLocator Instance => _instance ?? (_instance = new ViewModelLocator());

        private ViewModelLocator()
        {
            _navigationService = new NavigationService();
            _habitatRepository = new HabitatRepository();
            _squadRepository = new SquadRepository();

            MenuViewModel = new MenuViewModel(_navigationService);
            WhoIsViewViewModel = new WhoIsWhoViewModel(_navigationService, _habitatRepository, _squadRepository);
            FloorplanViewModel = new FloorplanViewModel(_navigationService);
            RoomViewModel = new RoomViewModel(_navigationService);
        }
    }
}
