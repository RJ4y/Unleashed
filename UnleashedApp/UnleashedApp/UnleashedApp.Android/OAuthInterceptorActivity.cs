using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using UnleashedApp.Authentication;

namespace UnleashedApp.Droid
{
    [Activity(Label = "OAuthInterceptorActivity", NoHistory =false)]
    [IntentFilter(
    actions: new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataScheme = "com.googleusercontent.apps.213697302597-efe8ekru9nue7ihl2qocfbe0j8dgmqcp",
    DataPath = GoogleAuthenticator.RedirectPath
)]
    public class OAuthInterceptorActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Uri uri = new Uri(Intent.Data.ToString());
            GoogleAuthenticator.Authenticator.OnPageLoading(uri);

            Finish();
        }
    }
}