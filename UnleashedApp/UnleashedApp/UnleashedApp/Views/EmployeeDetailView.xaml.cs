﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeDetailView : ContentPage
    {
        public EmployeeDetailView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = ViewModelLocator.Instance.EmployeeDetailViewModel;
        }
    }
}