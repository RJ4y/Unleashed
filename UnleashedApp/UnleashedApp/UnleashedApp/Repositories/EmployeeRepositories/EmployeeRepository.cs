using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.EmployeeRepositories
{
    public class EmployeeRepository : Repository, IEmployeeRepository
    {
        private List<Employee> _employees;
        private Employee _employee;

        public List<Employee> GetAllEmployees()
        {
            string address = "employees/";

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

        public Employee GetEmployeeById(int id)
        {
            string address = "employees/" + id + "/";

            try
            {
                HttpResponseMessage response = _client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    _employee = JsonConvert.DeserializeObject<Employee>(resultString);
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return _employee;
        }
    }
}
