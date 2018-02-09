using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using UnleashedApp.Contracts;
using UnleashedApp.Models;
using UnleashedApp.Models.Serializers;

namespace UnleashedApp.Repositories.SquadRepositories
{
    public class SquadRepository : Repository, ISquadRepository
    {
        private List<Squad> _squads;
        private Squad _squad;
        private List<Employee> _employees;

        public SquadRepository(IAuthenticationService authenticationService, IHttpClientAdapter httpClientAdapter) : base(authenticationService, httpClientAdapter)
        {
        }

        public List<Squad> GetAllSquads()
        {
            string address = "squads/";
            try
            {
                bool addToken = AddAuthenticationHeaderAsync().Result;
                HttpResponseMessage response = Client.GetAsync(address).Result;

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
            string address = "squads/" + id + "/";
            try
            {
                bool addToken = AddAuthenticationHeaderAsync().Result;
                HttpResponseMessage response = Client.GetAsync(address).Result;

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
            string address = "squads/" + id + "/employees/";
            try
            {
                bool addToken = AddAuthenticationHeaderAsync().Result;
                HttpResponseMessage response = Client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    List<SerializableEmployee> rootObjects =
                        JsonConvert.DeserializeObject<List<SerializableEmployee>>(resultString);
                    _employees = new List<Employee>();

                    foreach (SerializableEmployee root in rootObjects)
                    {
                        _employees.Add(root.Employee);
                    }
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