using System;
using System.Collections.Generic;
using System.Linq;
using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;
        
        public CommandRepository(AppDbContext context)
        {
            this._context = context;

        }
        public void CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;

            this._context.Commands.Add(command);
        }

        public void CreatePlatform(PlatformModel platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            this._context.Platforms.Add(platform);
        }

        public IEnumerable<PlatformModel> GetAllPlatforms()
        {
            return this._context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return this._context.Commands.FirstOrDefault(x => x.PlatformId == platformId && x.Id == commandId);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return this._context.Commands.Where(x => x.PlatformId == platformId);
        }

        public bool PlatformExists(int platformId)
        {
            return this._context.Platforms.Any(x => x.Id == platformId);
        }
        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return this._context.Platforms.Any(x => x.ExternlId == externalPlatformId);
        }
        public bool SaveChanges()
        {
            return (this._context.SaveChanges() > 0);
        }
    }
}