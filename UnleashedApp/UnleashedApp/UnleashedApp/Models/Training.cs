using Newtonsoft.Json;
using System;

namespace UnleashedApp.Models
{
    public class Training
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "days")]
        public int Days { get; set; }
        [JsonProperty(PropertyName = "firstname")]
        public string First_Name { get; set; }
        [JsonProperty(PropertyName = "lastname")]
        public string Last_Name { get; set; }
        [JsonProperty(PropertyName = "team")]
        public string Team { get; set; }
        [JsonProperty(PropertyName = "training")]
        public string TrainingName { get; set; }
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; set; }
        [JsonProperty(PropertyName = "invoice")]
        public string Invoice { get; set; }
        [JsonProperty(PropertyName = "info")]
        public string Info { get; set; }
    }
}
