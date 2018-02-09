using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface IWhoIsWhoViewModel
    {
        ICommand HabitatEmployeeDetailCommand { get; set; }
        ICommand SquadEmployeeDetailCommand { get; set; }
    }
}
