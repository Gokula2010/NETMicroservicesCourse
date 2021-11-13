using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;
using Serilog;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(ILogger logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = this._configuration["RabbitMQHost"],
                Port = Convert.ToInt32(this._configuration["RabbitMQPort"])
            };

            try
            {
                this._connection = factory.CreateConnection();
                this._channel = this._connection.CreateModel();
                this._channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                this._connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                logger.Information("Connected to the RobbitMQ Message Bus");

            }
            catch (Exception ex)
            {
                logger.Error("Could not connect to the RobbitMQ Message Bus");
                throw new ApplicationException($"Could not connect to the RobbitMQ Message Bus, {ex.Message}");
            }
        }

        public void PublishNewPlatform(PlatformPublishDto platformPublishDto)
        {
            var message = JsonConvert.SerializeObject(platformPublishDto);

            if (!this._connection.IsOpen)
            {
                this._logger.Warning("RabbitMQ connection is cloded, not sending message");
            }

            this._logger.Warning("RabbitMQ connection is Open, sending message...");

            SendMessage(message);
        }

        public void Dispose()
        {
            this._logger.Information($"RabbitMQ Service Bus Disposed");

            if (this._channel.IsOpen)
            {
                this._channel.Close();
                this._connection.Close();
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            this._channel.BasicPublish(
                exchange: "trigger",
                routingKey: "",
                basicProperties: null,
                body: body);

            this._logger.Information($"Message sent, {message}");
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            this._logger.Information("RabbitMQ Connection Shutdown");
        }
    }
}