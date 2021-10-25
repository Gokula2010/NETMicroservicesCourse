using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(
            IPlatformRepository repository,
            IMapper mapper,
            ICommandDataClient commandDataClient)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._commandDataClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platformItems = this._repository.GetAllPlatforms();

            return this.Ok(this._mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name="GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = this._repository.GetByPlatformId(id);

            if (platformItem == null)
            {
                return this.NotFound();
            }

            return this.Ok(this._mapper.Map<PlatformReadDto>(platformItem));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto createDto)
        {

            var platformModel = this._mapper.Map<Platform>(createDto);

            this._repository.CreatePlatform(platformModel);

            this._repository.SaveChanges();

            var platformReadDto = this._mapper.Map<PlatformReadDto>(platformModel);

            try
            {
                 await this._commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=> Could not send synchronsly : {ex.Message}");
            }


            return this.CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}