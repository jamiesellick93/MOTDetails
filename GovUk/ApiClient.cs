using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace MOTDetails.GovUk
{
    public class ApiClient
    {
        private const string govUkApiKeyAppSetting = "govUkApiKey";
        private const string govUkMotUrlAppSetting = "govUkMotUrl";

        private static string govUkApiKey;
        private static string govUkMotUrl;
        private HttpClient client;

        public ApiClient()
        {
            SettingsUtil.SetSettingOrThrow(govUkApiKeyAppSetting, out govUkApiKey);
            SettingsUtil.SetSettingOrThrow(govUkMotUrlAppSetting, out govUkMotUrl);
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", govUkApiKey);
        }

        public VehicleRecord GetVehicleRecord(string registrationNumber)
        {
            var response = client.GetAsync($"{govUkMotUrl}?registration={registrationNumber}").Result;
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound: return null;
                case HttpStatusCode.OK:
                    var results = JsonConvert.DeserializeObject<List<VehicleRecord>>(response.Content.ReadAsStringAsync().Result);
                    return results.Count > 0 ? results[0] : null;
                default: throw new Exception($"Request failed with error response [{(int)response.StatusCode}] {response.ReasonPhrase}");
            }
        }
    }
}
