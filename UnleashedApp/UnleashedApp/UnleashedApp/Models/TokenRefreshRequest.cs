using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnleashedApp.Models
{
    public class TokenRefreshRequest: TokenRequest
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        public TokenRefreshRequest(string refreshToken): base()
        {
            GrantType = "refresh_token";
            RefreshToken = refreshToken;
            ClientSecret = Constants.ClientSecretApi;
        }
    }
}
