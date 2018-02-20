using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.TrainingRepository;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class TrainingViewModel : ITrainingViewModel
    {
        private readonly ITrainingRepository _trainingRepository;
        private ObservableCollection<Training> _trainings;
        private int _trainingTotal;
        private Training _postTraining;
        public ICommand AddTrainingCommand { get; set; }
        private const int TrainingBudget = 2000;

        public event PropertyChangedEventHandler PropertyChanged;

        public TrainingViewModel(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
            _date = DateTime.Now;
            _sendInvoice = "No";
            _isValid = false;
            InitialiseCommands();
        }

        public void LoadTrainings()
        {
            //Switch commented line if (not) testing
            //TrainingList = new ObservableCollection<Training>();
            BudgetRemaining = TrainingBudget;
            var trainings = new ObservableCollection<Training>(_trainingRepository.GetAll());

            var tempList = trainings.Where(fn => fn.First_Name.Equals(AuthenticationService.Instance.GetUserFirstName())).Where(ln => ln.Last_Name.Equals(AuthenticationService.Instance.GetUserLastName()));
            TrainingList = new ObservableCollection<Training>(tempList);

            CalculateTotal();
        }

        public void Refresh()
        {
            LoadTrainings();
        }

        private void CalculateTotal()
        {
            TrainingTotal = 0;
            BudgetRemaining = TrainingBudget;

            if (_trainings != null)
            {
                foreach (Training training in _trainings)
                {
                    TrainingTotal += training.Cost;
                    BudgetRemaining -= training.Cost;
                }
            }
        }

        public void VerifyForm()
        {
            IsValid = IsCityValid && IsCompanyValid && IsCostValid && IsDaysValid && IsEventValid;
        }

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                RaisePropertyChanged(nameof(IsValid));
            }
        }

        public ObservableCollection<Training> TrainingList
        {
            get => _trainings;
            set
            {
                _trainings = value;
                RaisePropertyChanged(nameof(TrainingList));
            }
        }

        public int TrainingTotal
        {
            get => _trainingTotal;
            set
            {
                _trainingTotal = value;
                RaisePropertyChanged(nameof(TrainingTotal));
            }
        }

        private int _budgetRemaining;
        public int BudgetRemaining
        {
            get => _budgetRemaining;
            set
            {
                _budgetRemaining = value;
                RaisePropertyChanged(nameof(BudgetRemaining));
            }
        }

        #region TrainingProperties

        public DateTime MinimumDate => DateTime.Now.Subtract(TimeSpan.FromDays(365));

        public DateTime MaximumDate => DateTime.Now.AddYears(1);

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                RaisePropertyChanged(nameof(Date));
            }
        }

        private string _days;
        public string Days
        {
            get => _days;
            set
            {
                _days = value;
                RaisePropertyChanged(nameof(Days));
            }
        }

        private string _event;
        public string Event
        {
            get => _event;
            set
            {
                _event = value;
                RaisePropertyChanged(nameof(Event));
            }
        }

        private string _company;
        public string Company
        {
            get => _company;
            set
            {
                _company = value;
                RaisePropertyChanged(nameof(Company));
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                RaisePropertyChanged(nameof(City));
            }
        }

        private string _cost;
        public string Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                RaisePropertyChanged(nameof(Cost));
            }
        }

        private string _sendInvoice;
        public bool SendInvoice
        {
            get => _sendInvoice == "Yes";
            set
            {
                _sendInvoice = value ? "Yes" : "No";
                RaisePropertyChanged(nameof(SendInvoice));
            }
        }

        #region SendInvoiceTesting
        //Need for testing
        public string GetSendInvoice()
        {
            return _sendInvoice;
        }

        public void SetSendInvoice(string yesOrNo)
        {
            _sendInvoice = yesOrNo;
        }
        #endregion

        #endregion

        #region ValidationProperties
        private bool _isDaysValid;
        public bool IsDaysValid
        {
            get => _isDaysValid;
            set
            {
                _isDaysValid = value;
                VerifyForm();
                RaisePropertyChanged(nameof(IsDaysValid));
            }
        }

        private bool _isEventValid;
        public bool IsEventValid
        {
            get => _isEventValid;
            set
            {
                _isEventValid = value;
                VerifyForm();
                RaisePropertyChanged(nameof(IsEventValid));
            }
        }

        private bool _isCompanyValid;
        public bool IsCompanyValid
        {
            get => _isCompanyValid;
            set
            {
                _isCompanyValid = value;
                VerifyForm();
                RaisePropertyChanged(nameof(IsCompanyValid));
            }
        }

        private bool _isCityValid;
        public bool IsCityValid
        {
            get => _isCityValid;
            set
            {
                _isCityValid = value;
                VerifyForm();
                RaisePropertyChanged(nameof(IsCityValid));
            }
        }

        private bool _isCostValid;
        public bool IsCostValid
        {
            get => _isCostValid;
            set
            {
                _isCostValid = value;
                VerifyForm();
                RaisePropertyChanged(nameof(IsCostValid));
            }
        }
        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitialiseCommands()
        {
            AddTrainingCommand = new Command(() =>
            {
                _postTraining = new Training
                {
                    City = _city,
                    Company = _company,
                    Cost = Convert.ToInt32(_cost),
                    Date = _date,
                    Days = Convert.ToInt32(_days),
                    First_Name = AuthenticationService.Instance.GetUserFirstName(),
                    Last_Name = AuthenticationService.Instance.GetUserLastName(),
                    Info = "info",
                    Invoice = _sendInvoice,
                    Team = "team",
                    TrainingName = Event
                };
                TrainingList.Add(_postTraining);
                CalculateTotal();
                _trainingRepository.PostTrainingAsync(_postTraining);
            });
        }
    }
}
