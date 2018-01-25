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

        public event PropertyChangedEventHandler PropertyChanged;

        public TrainingViewModel(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
            Init();
        }

        public void Init()
        {
            _trainings = new ObservableCollection<Training>
            {
                new Training() { Date = "2018-01-24", Name = "TabbedView", Cost = 9001 },
                new Training() { Date = "2018-01-25", Name = "Code Review", Cost = 500 },
                new Training() { Date = "2018-01-25", Name = "Wenen", Cost = 9999999 }
            };
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

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
