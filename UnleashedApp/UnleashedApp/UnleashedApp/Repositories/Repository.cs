using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UnleashedApp.Repositories
{
    public abstract class Repository
    {
        protected static readonly HttpClient Client = new HttpClient();
        protected readonly Uri BaseAddress = new Uri("http://10.84.1.15:8000/");

        protected Repository()
        {
            Client.BaseAddress = BaseAddress;
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.Timeout = TimeSpan.FromSeconds(10);
        }
    }
}