using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;

namespace WareHouseManagerWebApp.Service
{
    public class taskLocationCoordinationService
    {
        private readonly AppDbContext _context;

        public taskLocationCoordinationService(AppDbContext context)
        {
            _context = context;
        }

        // Get task location with coordinates
        //public async Task<List<locationModel>> GetAllLocationsAsync()
        //{
        //    //return await _context.Locations
        //    //    .OrderBy(l => l.Row)
        //    //    .ThenBy(l => l.Level)
        //    //    .ThenBy(l => l.Shelf)
        //    //    .ThenBy(l => l.Space)
        //    //    .ToListAsync();
        //}

        // Get a specific location by ID
        //public async Task<locationModel> GetLocationByIdAsync(int locationId)
        //{
        //    //return await _context.Locations
        //    //    .FirstOrDefaultAsync(l => l.Id == locationId);
        //}

        // Add a new location
        public async Task AddLocationAsync(locationModel location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
        }

        // Update an existing location
        public async Task UpdateLocationAsync(locationModel location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
        }

        // Delete a location
        public async Task DeleteLocationAsync(int locationId)
        {
            var location = await _context.Locations.FindAsync(locationId);
            if (location != null)
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
        }
    }
}
