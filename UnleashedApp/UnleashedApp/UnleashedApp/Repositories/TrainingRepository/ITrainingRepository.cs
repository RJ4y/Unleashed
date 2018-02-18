using System.Collections.Generic;
using System.Threading.Tasks;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.TrainingRepository
{
    public interface ITrainingRepository
    {
        List<Training> GetAll();
        Training GetTraining(int trainingId);
        Task PostTrainingAsync(Training training);
        List<Training> GetAllMyTrainings(int employeeId);
        Training GetMyTraining(int employeeId, int trainingId);
    }
}
