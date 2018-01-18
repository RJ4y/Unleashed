using System.ComponentModel;
namespace UnleashedApp.ViewModels
{
    public class WhoIsWhoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public WhoIsWhoViewModel(INavigationService navigationService)
        {

        }
    }
}
