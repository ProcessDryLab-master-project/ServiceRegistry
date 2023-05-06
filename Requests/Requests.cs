﻿using System.Text;
using System;
using Newtonsoft.Json;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
//using ServiceRegistry.App;

namespace ServiceRegistry.Requests
{
    public class Requests
    {
        static HttpClient client = new HttpClient();
        public static async Task<bool> GetPing(string path)
        {
            string requestPath = path + "/ping";
            try
            {
                HttpResponseMessage response = await client.GetAsync(requestPath);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            
        }

        public static async Task<string> GetConfigFromNode(string nodeUrl)
        {
            string requestPath = nodeUrl + "/configurations";
            try
            {
                Console.WriteLine("Requesting config from: " + requestPath);

                //var message = new HttpRequestMessage(HttpMethod.Get, requestPath) { Version = new Version(2, 0) };
                //var response = await client.SendAsync(message);

                HttpResponseMessage response = await client.GetAsync(requestPath);
                if (response.IsSuccessStatusCode)
                {
                    string configString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Getting config from " + requestPath + " with status code: " + response.IsSuccessStatusCode);
                    return configString;
                }

                Console.WriteLine($"Request to {requestPath} failed with {response.IsSuccessStatusCode}");
                return "";
            }
            catch (Exception err)
            {
                Console.WriteLine($"Request to {requestPath} failed with error: {err}");
                return "";
            }
        }
    }
}
