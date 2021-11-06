using System.Collections.Generic;
using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepository
    {
        IEnumerable<PlatformModel> GetAllPlatforms();
        PlatformModel GetByPlatformId(int id);
        void CreatePlatform(PlatformModel platform);
        bool SaveChanges();
    }
}