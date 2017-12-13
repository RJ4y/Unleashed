using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.SquadRepositories
{
    public class SquadRepository : Repository, ISquadRepository
    {
        private List<Squad> _squads;

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
    }
}
