using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp
{
    public class NavigationService : INavigationService
    {
        private INavigation _navigation;
        public INavigation NavigationContext
        {
            set => _navigation = value;
        }

        private void PreventNullReferenceMethod()
        {
            if (_navigation == null)
                _navigation = Application.Current.MainPage.Navigation;
        }

        public Task<Page> PopAsync()
        {
            PreventNullReferenceMethod();
            return _navigation.PopAsync();
        }

        public Task<Page> PopModalAsync()
        {
            PreventNullReferenceMethod();
            return _navigation.PopModalAsync();
        }

        public Task PushAsync(string pageName)
        {
            PreventNullReferenceMethod();
            return _navigation.PushAsync(GetPage(pageName));
        }

        public Task PushAsync(Page page) {
            return _navigation.PushAsync(page);
        }

        public Task PushModalAsync(string pageName, object objectToPass)
        {
            PreventNullReferenceMethod();
            ContentPage page = GetPage(pageName, objectToPass);
            return _navigation.PushModalAsync(page);
        }

        public Task PushModalAsync(string pageName)
        {
            PreventNullReferenceMethod();
            var contentPage = GetPage(pageName);
            //avoid showing the same modal twice
            var contentPageType = contentPage.GetType();
            if (_navigation.ModalStack.Any(p => p.GetType() == contentPageType))
            {
                return Task.Run(() => { });
            }

            return _navigation.PushModalAsync(contentPage);
        }

        private ContentPage GetPage(string pageName, object objectToPass = null)
        {
            switch (pageName)
            {
                case nameof(MenuView): return new MenuView();
                case nameof(WhoIsWhoView): return new WhoIsWhoView();
                case nameof(FloorplanView): return new FloorplanView();
                case nameof(TrainingView): return new TrainingView();
                case nameof(RoomView): return new RoomView();
                case nameof(EmployeeDetailView): return new EmployeeDetailView();
            }
            return null;
        }

        public void ClearPageStack()
        {
            PreventNullReferenceMethod();
            List<Page> existingPages = _navigation.NavigationStack.ToList();

            for (int i = 0; i < existingPages.Count - 1; i++)//-1 to keep the last page (current page)
            {
                _navigation.RemovePage(existingPages[i]);
            }
        }
    }
}
