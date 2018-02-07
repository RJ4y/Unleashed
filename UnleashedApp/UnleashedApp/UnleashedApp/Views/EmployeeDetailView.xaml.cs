﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeDetailView
    {
        public EmployeeDetailView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.EmployeeDetailViewModel;
        }
    }
}