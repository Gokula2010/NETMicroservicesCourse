using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context)
        {
            this._context = context;
        }
        public void CreatePlatform(PlatformModel platform)
        {
            if( platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Add(platform);
        }

        public IEnumerable<PlatformModel> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public PlatformModel GetByPlatformId(int id)
        {
            return _context.Platforms.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}