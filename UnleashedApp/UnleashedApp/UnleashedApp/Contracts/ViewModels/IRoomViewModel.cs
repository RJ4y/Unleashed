using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface IRoomViewModel
    {
        ICommand EmployeeDetailCommand { get; set; }
    }
}
