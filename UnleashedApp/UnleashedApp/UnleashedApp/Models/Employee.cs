using Newtonsoft.Json;
using System;

namespace UnleashedApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public String First_Name { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public String Last_Name { get; set; }

        public String FullName { get; set; }

        public String Function { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "habitat")]
        public Habitat Habitat { get; set; }       
    }
}
