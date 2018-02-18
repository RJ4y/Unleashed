using Newtonsoft.Json;

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
