using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface IFloorplanViewModel
    {
        ICommand FloorplanCommand { get; set; }
    }
}
