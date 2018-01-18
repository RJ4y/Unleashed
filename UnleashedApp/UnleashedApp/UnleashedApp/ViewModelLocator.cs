using UnleashedApp.Repositories.RoomRepositories;
using UnleashedApp.Repositories.SpaceRepositories;
using UnleashedApp.ViewModels;

namespace UnleashedApp
{
    public class ViewModelLocator
    {
        private static ViewModelLocator _instance;
        private readonly INavigationService _navigationService;

        private readonly ISpaceRepository _spaceRepository;
        private readonly IRoomRepository _roomRepository;

        public MenuViewModel MenuViewModel { get; }
        public WhoIsWhoViewModel WhoIsViewViewModel { get; }
        public FloorplanViewModel FloorplanViewModel { get; }
        public RoomViewModel RoomViewModel { get; }

        public static ViewModelLocator Instance => _instance ?? (_instance = new ViewModelLocator());

        private ViewModelLocator()
        {
            _navigationService = new NavigationService();

            _spaceRepository = new SpaceRepository();
            _roomRepository = new RoomRepository();

            MenuViewModel = new MenuViewModel(_navigationService);
            WhoIsViewViewModel = new WhoIsWhoViewModel(_navigationService);
            FloorplanViewModel = new FloorplanViewModel(_navigationService, _spaceRepository, _roomRepository);
            RoomViewModel = new RoomViewModel(_navigationService);
        }
    }
}
