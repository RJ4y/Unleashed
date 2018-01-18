using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnleashedApp.Authentication
{
    public class TokenConvertRequest
    {
        public string Grant_type { get; set; }
        public string Client_id { get; set; }
        public string Client_secret { get; set; }
        public string Backend { get; set; }
        public string Token { get; set; }

        public TokenConvertRequest(string googleToken)
        {
            Grant_type = "convert_token";
            Client_id = Configuration.CLIENT_ID_API;
            Client_secret = Configuration.CLIENT_ID_API;
            Backend = "google-oauth2";
            Token = googleToken;
        }
    }
}
