using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
