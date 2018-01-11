using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.SquadRepositories
{
    public class SquadRepository : Repository, ISquadRepository
    {
        private List<Squad> _squads;
        private Squad _squad;
        private List<Employee> _employees;

        public List<Squad> GetAllSquads()
        {
            var address = "employees";

            try
            {
                HttpResponseMessage response = _client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _squads = JsonConvert.DeserializeObject<List<Squad>>(resultString);
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return _squads;
        }

        public Squad GetSquadById(int id)
        {
            var address = "squads/" + id;

            try
            {
                HttpResponseMessage response = _client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _squad = JsonConvert.DeserializeObject<Squad>(resultString);
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return _squad;
        }

        public List<Employee> GetEmployees(int id)
        {
            var address = "squads/" + id + "/employees";

            try
            {
                HttpResponseMessage response = _client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    List<Temp> rootObjects = JsonConvert.DeserializeObject<List<Temp>>(resultString);
                    _employees = new List<Employee>();

                    foreach(Temp root in rootObjects)
                    {
                        _employees.Add(root.Employee);
                    }

                    //_employees = JsonConvert.DeserializeObject<List<Employee>>(resultString);
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
