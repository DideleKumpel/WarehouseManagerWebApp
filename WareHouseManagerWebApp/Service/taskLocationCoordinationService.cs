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

        public async Task AddUnLoadTask(taskModel task)
        {
            locationModel location = await _context.Locations.FindAsync(task.LocationId);
            if (location != null)
            {
                location.ItemBarcode = task.ProductBarcode;
                location.IsOnLocation = false;
                _context.Tasks.Add(task);
                _context.Locations.Update(location);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddLoadTask(taskModel task)
        {
            locationModel location = await _context.Locations.FindAsync(task.LocationId);
            if (location != null)
            {
                location.ItemBarcode = task.ProductBarcode;
                location.IsOnLocation = false;
                _context.Tasks.Add(task);
                _context.Locations.Update(location);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUnloadTask(taskModel task)
        {
            if (task != null)
            {
                locationModel location = await _context.Locations.FindAsync(task.LocationId);
                if (location != null)
                {
                    location.ItemBarcode = null;
                    _context.Tasks.Remove(task);
                    _context.Locations.Update(location);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteLoadTask(taskModel task)
        {
            if (task != null)
            {
                locationModel location = await _context.Locations.FindAsync(task.LocationId);
                if (location != null)
                {
                    location.IsOnLocation = true;
                    _context.Tasks.Remove(task);
                    _context.Locations.Update(location);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
