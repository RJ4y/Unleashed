using Newtonsoft.Json;
using System;

namespace UnleashedApp.Models
{
    public class Habitat
    {
        [JsonProperty(PropertyName="id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }
    }
}
