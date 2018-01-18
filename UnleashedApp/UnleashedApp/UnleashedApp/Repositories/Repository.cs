using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UnleashedApp.Repositories
{
    public abstract class Repository
    {
        protected static readonly HttpClient _client = new HttpClient();
        //NOTE: phones will turn to their own (device's) localhost. so set the ip to the ip of the device/server running the python backend!
        //protected readonly Uri _baseAddress = new Uri("http://localhost:8000/");
        protected readonly Uri _baseAddress = new Uri("http://192.168.0.207:8000/");

        public Repository()
        {
            _client.BaseAddress = _baseAddress;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("UWP_APP"));
        }


    }
}
