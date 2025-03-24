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
                _context.Tasks.Add(task);
                _context.Locations.Update(location);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
