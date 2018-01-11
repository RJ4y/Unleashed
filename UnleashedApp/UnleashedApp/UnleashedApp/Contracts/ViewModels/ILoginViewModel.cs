using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface ILoginViewModel
    {
        ICommand LoginCommand { get; set; }
    }
}
