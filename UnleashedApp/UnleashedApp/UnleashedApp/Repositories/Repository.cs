using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UnleashedApp.Repositories
{
    public abstract class Repository
    {
        protected static readonly HttpClient _client = new HttpClient();
        //Set the base address to the ip of the device/server running the python backend!
        protected readonly Uri _baseAddress = new Uri("http://10.84.134.29:8000/");

        public Repository()
        {
            _client.BaseAddress = _baseAddress;
            _client.Timeout = TimeSpan.FromSeconds(5);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
