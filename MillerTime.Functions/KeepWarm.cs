using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace MillerTime.Functions
{
    public static class KeepWarm
    {
        private static readonly HttpClient _httpClient = new();
        private static readonly string healthEndpoint = "https://miller-time-api.azurewebsites.net/api/Health/CheckHealth";

        [FunctionName("KeepWarm")]
        public async static Task RunAsync([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(healthEndpoint);
            }
            catch (Exception)
            {

            }
        }
    }
}
