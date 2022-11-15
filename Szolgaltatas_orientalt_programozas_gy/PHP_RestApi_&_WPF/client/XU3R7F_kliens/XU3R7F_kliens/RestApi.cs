using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;

namespace XU3R7F_kliens
{
    internal class RestApi
    {
        private const string BASE_ADDRESS = "http://127.0.0.1/wp3/";

        private CookieContainer cookieContainer;
        private HttpClientHandler clienthandler;
        private HttpClient client;


        public RestApi()
        {
            cookieContainer = new CookieContainer();
            clienthandler = new HttpClientHandler { AllowAutoRedirect = true, UseCookies = true, CookieContainer = cookieContainer };
            client = new HttpClient(clienthandler);
            client.BaseAddress = new Uri(BASE_ADDRESS);

        }

        public string Post(string path, Dictionary<string, string> payload = null)
        {
            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            string response = client.PostAsync(path, c).Result.Content.ReadAsStringAsync().Result;

            return response;
        }

        public string Get(string path)
        {
            string result = client.GetAsync(path).Result.Content.ReadAsStringAsync().Result;
            return result;
        }

        public string Put(string path, Dictionary<string, string> payload = null)
        {
            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            string response = client.PutAsync(path, c).Result.Content.ReadAsStringAsync().Result;

            return response;
        }

        public string Delete(string path)
        {
            string response = client.DeleteAsync(path).Result.Content.ReadAsStringAsync().Result;
            
            return response;
        }



    }
}
