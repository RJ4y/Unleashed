using System;
using UnleashedApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhoIsWhoView : ContentPage
    {

        public WhoIsWhoView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void InitializeList()
        {
            var viewModel = ViewModelLocator.Instance.WhoIsViewViewModel;

            //contentView added als template, dus telkens Children.Add(new xTemplate { BindingContext = y})
            foreach(String groupName in viewModel.Groups)
            {
                //___ vervangen door de juiste template en bindingcontext
                //Groups.Children.Add(new ___ { BindingContext = ___});
            }
        }
    }
}