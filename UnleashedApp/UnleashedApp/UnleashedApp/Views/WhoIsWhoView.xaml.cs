using DLToolkit.Forms.Controls;
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
            //uncommment after tests
            //InitializeList();
        }

        public void InitializeList()
        {
            var viewModel = ViewModelLocator.Instance.WhoIsViewViewModel;
            FlowListView flowListView;
            StackLayout stack;
            Label label;

            //contentView added als template, dus telkens Children.Add(new xTemplate { BindingContext = y})
            foreach(Group group in viewModel.Groups)
            {
                stack = new StackLayout();

                label = new Label();
                label.Text = group.Name;
                stack.Children.Add(label);

                viewModel.ProvideEmployeesPerHabitat(group.Id);

                //___ vervangen door de juiste template en bindingcontext
                flowListView = new FlowListView();
                flowListView.HasUnevenRows = false;
                flowListView.FlowItemsSource = viewModel.Groups;
                flowListView.SetBinding(BindingContextProperty, "HabitatEmployeeList");
                //uncommment if the binding doesn't work
                //flowListView.ItemsSource = viewModel.HabitatEmployeeList;

                stack.Children.Add(flowListView);

                GroupStack.Children.Add(stack);
                //Groups.Children.Add(new ___ { BindingContext = ___});
            }
        }
    }
}