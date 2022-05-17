using System.Collections.Generic;
using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        // For Platforms
        IEnumerable<PlatformModel> GetAllPlatforms();
        void CreatePlatform(PlatformModel platform);
        bool PlatformExists(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);

        // For Commands

        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}