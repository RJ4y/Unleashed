using Newtonsoft.Json;

namespace UnleashedApp.Models
{
    public class Habitat
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Id + ":" + Name;
        }
    }
}
