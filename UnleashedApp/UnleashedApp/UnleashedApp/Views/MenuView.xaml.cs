using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView
    {
        public MenuView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            MessagingCenter.Subscribe<MenuViewModel, string>(this, "logout_failed", (sender, arg) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Logout failed!", arg, "Try again");
                });
            });
        }


    }
}