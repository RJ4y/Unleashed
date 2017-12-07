using System.Windows.Input;

namespace UnleashedApp.Contracts.ViewModels
{
    public interface IWhoIsWhoViewModel
    {
        ICommand LoadCommand { get; set; }
    }
}
