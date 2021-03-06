﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using UnleashedApp.Contracts;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.SpaceRepositories
{
    public class SpaceRepository : Repository, ISpaceRepository
    {
        private List<Space> _spaces;

        public SpaceRepository(IAuthenticationService authenticationService, IHttpClientAdapter httpClientAdapter) : base(authenticationService, httpClientAdapter)
        {
        }

        public List<Space> GetAllSpaces()
        {
            _spaces = new List<Space>();
            string address = "spaces/";
            try
            {
                bool addToken = AddAuthenticationHeaderAsync().Result;
                HttpResponseMessage response = Client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    
                    List<SerializableSpace> spaces = JsonConvert.DeserializeObject<List<SerializableSpace>>(resultString);
                    foreach (SerializableSpace s in spaces)
                    {
                        Space space = new Space(s.XCoord, s.YCoord, s.EmployeeId, s.Room);
                        _spaces.Add(space);
                    }
                    
                    _spaces = JsonConvert.DeserializeObject<List<Space>>(resultString);
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