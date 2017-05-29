﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PCO.Net
{
    public class HttpTransport : ITransport
    {
        public static string BaseUri = "https://api.planningcenteronline.com/services/v2";
        private readonly string token = "";
        private readonly string secret = "";

        public async Task<string> GetJsonResult(string url)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(
                        string.Format("{0}:{1}", token, secret))));

            return await client.GetStringAsync(url);
        }
    }
}
