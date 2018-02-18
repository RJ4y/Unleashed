using Newtonsoft.Json;

namespace UnleashedApp.Models
{
    public abstract class TokenRequest
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        protected TokenRequest()
        {
            ClientId = Constants.ClientIdApi;
        }
    }
}
