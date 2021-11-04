using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PlatformService.Dtos;
using Serilog;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HttpCommandDataClient(
            ILogger logger,
            IConfiguration configuration,
            HttpClient httpClient)
        {
            this._logger = logger;
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

            if (response.IsSuccessStatusCode)
            {
                this._logger.Information("=> Sync POST to CommandService was OK!");
            }
            else
            {
                this._logger.Warning("=> Sync POST to CommandService was NOT OK!");
            }
        }
    }
}