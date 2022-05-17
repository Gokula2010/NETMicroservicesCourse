using System.Collections.Generic;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("api/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository commandRepository, IMapper mapper)
        {
            this._commandRepository = commandRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            if (!this._commandRepository.PlatformExists(platformId))
            {
                return this.NotFound();
            }

            var commands = this._commandRepository.GetCommandsForPlatform(platformId);

            return this.Ok(this._mapper.Map<IEnumerable<CommandCreateDto>>(commands));
        }

        [HttpGet("{commandId}")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            if (!this._commandRepository.PlatformExists(platformId))
            {
                return this.NotFound();
            }

            var command = this._commandRepository.GetCommand(platformId, commandId);
            if (command == null)
            {
                return this.NotFound();
            }

            return this.Ok(this._mapper.Map<CommandReadDto>(command));
        }
        
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandsForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            if (!this._commandRepository.PlatformExists(platformId))
            {
                return this.NotFound();
            }

            var command = this._mapper.Map<Command>(commandCreateDto);

            this._commandRepository.CreateCommand(platformId, command);
            this._commandRepository.SaveChanges();

            return this.CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = command.Id }, command);
        }

    }
}