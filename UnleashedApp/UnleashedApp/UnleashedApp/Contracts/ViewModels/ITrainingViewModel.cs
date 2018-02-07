using System.ComponentModel;
using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface ITrainingViewModel : INotifyPropertyChanged
    {
        ICommand AddTrainingCommand { get; set; }
    }
}
