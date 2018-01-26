using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.SpaceRepositories
{
    public class SpaceRepository : Repository, ISpaceRepository
    {
        private List<Space> _spaces = new List<Space>();

        public List<Space> GetAllSpaces()
        {
            string address = "spaces/";
            try
            {
                HttpResponseMessage response = _client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    List<SerializableSpace> spaces = JsonConvert.DeserializeObject<List<SerializableSpace>>(resultString);
                    foreach (SerializableSpace s in spaces)
                    {
                        Space space = new Space(s.XCoord, s.YCoord, s.EmployeeId, s.Room.Id);
                        _spaces.Add(space);
                    }
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return _spaces;
        }
    }
}
