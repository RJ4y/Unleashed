using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;

namespace UnleashedApp.ViewModels
{
    public class MenuViewModel : IMenuViewModel
    {
        //properties
        public ICommand WhoIsWhoCommand { get; set; }
    }
}
