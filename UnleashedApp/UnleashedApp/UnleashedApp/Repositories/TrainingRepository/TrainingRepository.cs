using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.TrainingRepository
{
    public class TrainingRepository : Repository, ITrainingRepository
    {
        private List<Training> _trainings;
        private Training _training;

        public List<Training> GetAll()
        {
            string address = "trainings/";

            try
            {
                HttpResponseMessage response = Client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _trainings = JsonConvert.DeserializeObject<List<Training>>(resultString,
                        new IsoDateTimeConverter {DateTimeFormat = "dd/MM/yyyy"});
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return _trainings;
        }

        public Training GetTraining(int trainingId)
        {
            string address = "trainings/" + trainingId + "/";

            try
            {
                HttpResponseMessage response = Client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _training = JsonConvert.DeserializeObject<Training>(resultString);
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return _training;
        }

        public async Task PostTrainingAsync(Training training)
        {
            string address = "trainings/";

            try
            {
                string postString = JsonConvert.SerializeObject(training,
                    new IsoDateTimeConverter() {DateTimeFormat = "dd/MM/yyyy"});
                StringContent content = new StringContent(postString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await Client.PostAsync(address, content);
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public List<Training> GetAllMyTrainings(int employeeId)
        {
            string address = "employees/" + employeeId + "/trainings/";

            try
            {
                HttpResponseMessage response = Client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _trainings = JsonConvert.DeserializeObject<List<Training>>(resultString);
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return _trainings;
        }

        public Training GetMyTraining(int employeeId, int trainingId)
        {
            string address = "employees/" + employeeId + "/trainings/" + trainingId + "/";

            try
            {
                HttpResponseMessage response = Client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _training = JsonConvert.DeserializeObject<Training>(resultString);
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return _training;
        }
    }
}