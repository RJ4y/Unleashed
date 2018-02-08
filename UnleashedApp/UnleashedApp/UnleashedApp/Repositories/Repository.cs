using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UnleashedApp.Repositories
{
    public abstract class Repository
    {
        protected static readonly HttpClient Client = new HttpClient();

        //Set the base address to the ip of the device/server running the python backend!
        protected readonly Uri BaseAddress = new Uri("http://10.84.134.18:8000/");

        protected Repository()
        {
            Client.BaseAddress = BaseAddress;
            Client.Timeout = TimeSpan.FromSeconds(20);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}