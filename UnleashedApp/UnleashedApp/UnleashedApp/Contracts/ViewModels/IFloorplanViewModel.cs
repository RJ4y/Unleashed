using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface IFloorplanViewModel
    {
        ICommand RoomCommand { get; set; }
    }
}
