﻿using System;
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
            _date = DateTime.Now;
            _sendInvoice = "Nee";
            InitialiseCommands();
            Init();
            CalculateTotal();
        }

        private void Init()
        {
            _trainings = new ObservableCollection<Training>();

            var trainingList = _trainingRepository.GetAll();

            foreach(Training training in trainingList)
            {
                _trainings.Add(training);
            }
        }

        private void CalculateTotal()
        {
            foreach(Training training in _trainings)
            {
                TrainingTotal += training.Cost;
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

        private string _sendInvoice;
        public bool SendInvoice
        {
            get
            {
                return _sendInvoice == "Ja";
            }
            set
            {
                _sendInvoice = value ? "Ja" : "Nee";
                RaisePropertyChanged(nameof(SendInvoice));
            }
        }
        public string getSendInvoice()
        {
            return _sendInvoice;
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
                    First_Name = "René",
                    Last_Name = "Jacobs",
                    Info = "info",
                    Invoice = _sendInvoice,
                    Team = "team",
                    TrainingName = Event
                };

                _trainingRepository.PostTrainingAsync(_postTraining);
            });
        }
    }
}
