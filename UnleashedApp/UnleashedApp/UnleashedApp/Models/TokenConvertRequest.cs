using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Models;

namespace UnleashedApp.Authentication
{
    public class TokenConvertRequest: TokenRequest
    {
        [JsonProperty("backend")]
        public string Backend { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        public TokenConvertRequest(string googleToken): base()
        {
            GrantType = "convert_token";
            Backend = "google-oauth2";
            Token = googleToken;
            ClientSecret = Constants.CLIENT_SECRET_API;
        }
    }
}
