using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.TrainingRepository;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class TrainingViewModel : ITrainingViewModel
    {
        private ITrainingRepository _trainingRepository;
        private ObservableCollection<Training> _trainings;
        private int _trainingTotal;
        private Training _postTraining;
        public ICommand AddTrainingCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TrainingViewModel(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
            InitialiseCommands();
            Init();
            CalculateTotal();
        }

        public void Init()
        {
            _trainings = new ObservableCollection<Training>();

            var trainingList = _trainingRepository.GetAll();

            foreach(Training training in trainingList)
            {
                var tr = new Training
                {
                    City = training.City,
                    Company = training.Company,
                    Cost = training.Cost,
                    Date = training.Date,
                    Days = training.Days,
                    First_Name = training.First_Name,
                    Last_Name = training.Last_Name,
                    Info = training.Info,
                    Invoice = training.Invoice,
                    Team = training.Team,
                    TrainingName = training.TrainingName
                };

                _trainings.Add(tr);
            }
        }

        public void CalculateTotal()
        {
            var total = 0;

            foreach(Training training in _trainings)
            {
                total += training.Cost;
            }

            TrainingTotal = total;
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

        #region TrainingProperties
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

        private string _isOn;
        public bool IsOn
        {
            get
            {
                if(_isOn == "Ja")
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    _isOn = "Ja";
                }
                else
                {
                    _isOn = "Nee";
                }
                RaisePropertyChanged(nameof(IsOn));
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
                _postTraining = new Training();

                _postTraining.City = City;
                _postTraining.Company = Company;
                _postTraining.Cost = Convert.ToInt32(Cost);
                _postTraining.Date = Date;
                _postTraining.Days = Convert.ToInt32(Days);
                _postTraining.First_Name = "René";
                _postTraining.Last_Name = "Jacobs";
                _postTraining.Info = "info";
                _postTraining.Invoice = _isOn;
                _postTraining.Team = "team";
                _postTraining.TrainingName = Event;

                _trainingRepository.PostTrainingAsync(_postTraining);
            });
        }
    }
}
