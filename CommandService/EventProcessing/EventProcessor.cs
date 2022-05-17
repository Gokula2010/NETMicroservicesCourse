using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CommandService.Dtos;
using CommandService.Data;
using System;
using CommandService.Models;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(
            IServiceScopeFactory scopeFactory,
            IMapper mapper)
        {
            this._scopeFactory = scopeFactory;
            this._mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = this.DetermineEvent(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:

                    break;

                default:
                    break;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepository>();

                var platfromPublihsedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var platform = _mapper.Map<PlatformModel>(platfromPublihsedDto);

                    if(!repo.ExternalPlatformExists(platform.ExternlId))
                    {
                        repo.CreatePlatform(platform);
                        repo.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Platform already exists");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Platform_Published":
                    return EventType.PlatformPublished;

                default:
                    return EventType.Undetermined;
            }
        }
    }

    public enum EventType
    {
        PlatformPublished,
        Undetermined       
    }
}