using Newtonsoft.Json;

namespace UnleashedApp.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "given_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "family_name")]
        public string LastName { get; set; }

        public string Email { get; set; }

    }
}
