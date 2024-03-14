using Newtonsoft.Json;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Helper : IHelper
    {
        

        public async Task<string> DeleteAPI(string url, string token)
        {
            var client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(url);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //request.Headers.Add("Bearer", token);
            HttpResponseMessage response = await client.DeleteAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> GetAPI(string url, string token)
        {
            var client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(url);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //request.Headers.Add("Bearer", token);
            HttpResponseMessage response = await client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> PostAPI(string url, string token, object obj)
        {
            var client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(url);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //request.Headers.Add("Bearer", token);
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> PutAPI(string url, string token, object obj)
        {
            var client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Put;
            request.RequestUri = new Uri(url);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //request.Headers.Add("Bearer", token);
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var a = await GetAPI(url, token);
            return responseString;
        }
    }
}
