using Microsoft.EntityFrameworkCore;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;

namespace WareHouseManagerWebApp.Service
{
    public class taskService
    {
        private readonly AppDbContext _context;

        public taskService(AppDbContext context)
        {
            _context = context;
        }

        // Fetch all tasks with their related data
        public async Task<List<taskModel>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Location)
                .Include(t => t.Product)
                .Include(t => t.Ramp)
                .ToListAsync();
        }

        //// Get a task by its ID
        //public async Task<taskModel> GetTaskByIdAsync(int taskId)
        //{
        //    return await _context.Tasks
        //        .Include(t => t.Ramp)
        //        .Include(t => t.Employee)
        //        .Include(t => t.Location)
        //        .Include(t => t.Product)
        //        .FirstOrDefaultAsync(t => t.Id == taskId);
        //}

        // Add a new task
        public async Task AddTaskAsync(taskModel task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        // Update an existing task
        public async Task UpdateTaskAsync(taskModel task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        // Delete a task
        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
