using System;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;

namespace UnleashedApp.ViewModels
{
    public class EmployeeDetailViewModel : ViewModelBase, IEmployeeDetailViewModel
    {
        private Employee _employee;
        public Employee Employee { get; set; }

        public EmployeeDetailViewModel()
        {
            //Initialise();
        }

        private void Initialise()
        {
            MessagingCenter.Subscribe<WhoIsWhoViewModel, Employee>(this, "", (sender, data) =>
            {
                _employee = data;
            });
        }
    }
}
