using System.Collections.Generic;
using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepository
    {
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetByPlatformId(int id);
        void CreatePlatform(Platform platform);
        bool SaveChanges();
    }
}