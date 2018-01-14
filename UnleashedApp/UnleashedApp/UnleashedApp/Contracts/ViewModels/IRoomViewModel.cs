using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface IRoomViewModel
    {
        ICommand RoomCommand { get; set; }
    }
}
