using Newtonsoft.Json;
using System;

namespace UnleashedApp.Models
{
    public class Squad
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
