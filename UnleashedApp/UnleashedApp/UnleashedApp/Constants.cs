namespace UnleashedApp
{
    public static class Constants
    {
        public const string ClientIdApi = "PisqVHmz7R28aCN9cc552eqhiHgO4tddU6Ed8a9F";
        public const string ClientSecretApi = "gabqf7ooJTbhZPpaD0yeDN1Dl8RDIu8aKNBVsTe4UpbPk9hr5IU4oniJ5C926OECfWYij349ECbBhEaqiU6mBuQJ8MLk1OtsxWg5fjYjQqscdmPXcJ6q1GovkFNCWPHL";
        public const string AppName = "Unleashed";
        public const string BaseApiUrl = "http://192.168.0.206:8000/";
        public const string RefreshUrl = "auth/token";

        public const string AccountPropertyAccessToken = "API_token";
        public const string AccountPropertyRefreshToken = "API_refresh_token";
        public const string AccountPropertyExpiration = "API_token_expiration";
        public const string AccountPropertyFirstName = "user_first_name";
        public const string AccountPropertyLastName = "user_last-name";

        public static string ClientId
        {
            get
            {
                string clientId = "";
                #if __ANDROID__
                    clientId = "213697302597-efe8ekru9nue7ihl2qocfbe0j8dgmqcp.apps.googleusercontent.com";
                #else
                #if __IOS__
                    clientId = "213697302597-qa85d81nieh9b4vuodqka6rvgc393363.apps.googleusercontent.com"; 
                #endif
                #endif
                return clientId;
            }
        }

        public static string RedirectScheme
        {
            get
            {
                string redirectScheme = "";
                #if __ANDROID__
                    redirectScheme = "com.googleusercontent.apps.213697302597-efe8ekru9nue7ihl2qocfbe0j8dgmqcp";
                #else
                #if __IOS__
                    redirectScheme = "com.googleusercontent.apps.213697302597-qa85d81nieh9b4vuodqka6rvgc393363"; 
                #endif
                #endif
                return redirectScheme;
            }
        }

    }
}