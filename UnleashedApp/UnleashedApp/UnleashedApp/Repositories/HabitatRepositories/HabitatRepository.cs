using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.HabitatRepositories
{
    public class HabitatRepository : Repository, IHabitatRepository
    {
        private List<Habitat> _habitats;
        private List<Employee> _employees;
        private Habitat _habitat;

        public List<Habitat> GetAllHabitats()
        {
            string address = "habitats/";
            try
            {
                HttpResponseMessage response = _client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _habitats = JsonConvert.DeserializeObject<List<Habitat>>(resultString);
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return _habitats;
        }

        public Habitat GetHabitatById(int id)
        {
            string address = "habitats/" + id + "/";            
            try
            {
                HttpResponseMessage response = _client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _habitat = JsonConvert.DeserializeObject<Habitat>(resultString);
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return _habitat;
        }

        public List<Employee> GetEmployees(int id)
        {
            string address = "habitats/" + id + "/employees/";            
            try
            {
                HttpResponseMessage response = _client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _employees = JsonConvert.DeserializeObject<List<Employee>>(resultString);
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return _employees;
        }
    }
}
