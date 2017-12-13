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

        public List<Habitat> GetAllHabitats()
        {
            var address = "employees";

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
    }
}
