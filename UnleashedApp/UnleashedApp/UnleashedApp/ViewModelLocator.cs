using UnleashedApp.ViewModels;

namespace UnleashedApp
{
    public class ViewModelLocator
    {
        //singelton
        public static readonly ViewModelLocator Instance = new ViewModelLocator();

        public MenuViewModel MenuViewModel { get; }

        public ViewModelLocator()
        {
            //services:
            INavigationService navigationService = new NavigationService();
            //repositories:

            //viewmodels:
            MenuViewModel = new MenuViewModel(navigationService);
        }
    }
}
