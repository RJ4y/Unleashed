using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.TrainingRepository;

namespace UnleashedApp.ViewModels
{
    public class TrainingViewModel : ITrainingViewModel
    {
        private ITrainingRepository _trainingRepository;
        private ObservableCollection<Training> _trainings;
        private int _trainingTotal;

        public event PropertyChangedEventHandler PropertyChanged;

        public TrainingViewModel(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
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

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
