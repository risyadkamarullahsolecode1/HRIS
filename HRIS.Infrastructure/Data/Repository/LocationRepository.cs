using Microsoft.EntityFrameworkCore;
using HRIS.Domain.Entities;
using HRIS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Data.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly HRISContext _context;

        public LocationRepository(HRISContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return await _context.Locations.ToListAsync();
        }
        public async Task<Location> AddLocation(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }
        public async Task<Location> UpdateLocation(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
            return location;
        }
        public async Task DeleteLocation(Location location)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
