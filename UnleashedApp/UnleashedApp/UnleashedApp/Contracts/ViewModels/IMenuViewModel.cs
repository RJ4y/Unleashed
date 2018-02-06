using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface IMenuViewModel
    {
        ICommand HomeCommand { get; set; }
        ICommand WhoIsWhoCommand { get; set; }
        ICommand FloorplanCommand { get; set; }
    }
}
