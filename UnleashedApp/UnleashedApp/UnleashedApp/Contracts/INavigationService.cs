using System.Threading.Tasks;
using Xamarin.Forms;

namespace UnleashedApp
{
    public interface INavigationService
    {
        INavigation NavigationContext { set; }
        Task<Page> PopAsync();
        Task PushAsync(string pageName);
        Task<Page> PopModalAsync();
        Task PushModalAsync(string pageName);
        Task PushModalAsync(string pageName, object objectToPass);
        void ClearPageStack();
    }
}
