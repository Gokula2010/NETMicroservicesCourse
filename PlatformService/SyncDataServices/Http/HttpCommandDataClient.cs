using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HttpCommandDataClient(IConfiguration configuration, HttpClient httpClient)
        {
            this._configuration = configuration;
            this._httpClient = httpClient;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                content: JsonConvert.SerializeObject(platform),
                encoding: Encoding.UTF8,
                "application/json"
            );

            var response = await this._httpClient.PostAsync($"{this._configuration["CommandServiceEndPoint"]}", httpContent);
            
            var message = response.IsSuccessStatusCode ? "=> Sync POST to CommandService was OK!" : "=> Sync POST to CommandService was NOT OK!";
            
            Console.WriteLine(message);

        }
    }
}