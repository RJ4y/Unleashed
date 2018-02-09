using System;
using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainingView : TabbedPage
    {
        public TrainingView ()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.TrainingViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext != null)
            {
                ((TrainingViewModel)BindingContext).LoadTrainings();
            }
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            ((TrainingViewModel)BindingContext).Refresh();
            ((ListView)sender).EndRefresh();
        }
    }
}