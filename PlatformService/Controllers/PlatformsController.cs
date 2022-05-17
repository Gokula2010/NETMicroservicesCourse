using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using Serilog;
namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(
            ILogger logger,
            IPlatformRepository repository,
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            this._logger = logger;
            this._repository = repository;
            this._mapper = mapper;
            this._commandDataClient = commandDataClient;
            this._messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platformItems = this._repository.GetAllPlatforms();

            return this.Ok(this._mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = this._repository.GetByPlatformId(id);

            if (platformItem == null)
            {
                this._logger.Error($"{id} not found in Platform");

                return this.NotFound();
            }

            return this.Ok(this._mapper.Map<PlatformReadDto>(platformItem));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto createDto)
        {

            var platformModel = this._mapper.Map<PlatformModel>(createDto);

            this._repository.CreatePlatform(platformModel);

            this._repository.SaveChanges();

            var platformReadDto = this._mapper.Map<PlatformReadDto>(platformModel);

            // try
            // {
            //     await this._commandDataClient.SendPlatformToCommand(platformReadDto);
            // }
            // catch (Exception ex)
            // {
            //     this._logger.Error($"Could not send synchronously : {ex.Message}");
            // }

            await Task.CompletedTask;
            try
            {
                var platformPublishedDto = this._mapper.Map<PlatformPublishDto>(platformReadDto);
                platformPublishedDto.Event = "Platform_Published";
                this._messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                this._logger.Error($"Could not send asynchronously : {ex.Message}");
            }

            return this.CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}