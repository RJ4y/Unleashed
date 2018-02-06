using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootView : MasterDetailPage
    {
        public RootView()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Default;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}