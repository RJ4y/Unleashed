﻿using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories;
using UnleashedApp.Repositories.AuthenticationRepositories;
﻿using UnleashedApp.Contracts;
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

        public LoginViewModel LoginViewModel { get; }
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
            IAuthenticationService authenticationService = AuthenticationService.Instance;
            IHttpClientAdapter httpClientAdapter = new HttpClientAdapter();
            ISpaceRepository spaceRepository = new SpaceRepository(authenticationService, httpClientAdapter);
            IRoomRepository roomRepository = new RoomRepository(authenticationService, httpClientAdapter);
            IEmployeeRepository employeeRepository = new EmployeeRepository(authenticationService, httpClientAdapter);
            IHabitatRepository habitatRepository = new HabitatRepository(authenticationService, httpClientAdapter);
            ISquadRepository squadRepository = new SquadRepository(authenticationService, httpClientAdapter);
            ITrainingRepository trainingRepository = new TrainingRepository(authenticationService, httpClientAdapter);
            IAuthenticationRepository authenticationRepository = new AuthenticationRepository(authenticationService, httpClientAdapter, new AuthenticationHttpClientAdapter(authenticationService, httpClientAdapter));

            MenuViewModel = new MenuViewModel(navigationService, authenticationService, authenticationRepository);
            WhoIsWhoViewModel = new WhoIsWhoViewModel(navigationService, habitatRepository, squadRepository);
            FloorplanViewModel = new FloorplanViewModel(navigationService, spaceRepository, roomRepository);
            TrainingViewModel = new TrainingViewModel(trainingRepository);
            RoomViewModel = new RoomViewModel(navigationService, employeeRepository);
            EmployeeDetailViewModel = new EmployeeDetailViewModel(spaceRepository, roomRepository);
            NameGameViewModel = new NameGameViewModel(employeeRepository);
            LoginViewModel = new LoginViewModel(navigationService, authenticationService, authenticationRepository);
        }
    }
}