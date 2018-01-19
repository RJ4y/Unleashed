using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface IMenuViewModel
    {
        ICommand WhoIsWhoCommand { get; set; }
        ICommand LogOutCommand { get; set; }
    }
}
