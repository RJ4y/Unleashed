using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnleashedApp.Authentication
{
    public class TokenConvertRequest
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("backend")]
        public string Backend { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        public TokenConvertRequest(string googleToken)
        {
            GrantType = "convert_token";
            ClientId = Configuration.CLIENT_ID_API;
            ClientSecret = Configuration.CLIENT_SECRET_API;
            Backend = "google-oauth2";
            Token = googleToken;
        }
    }
}
