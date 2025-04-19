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
        public async Task DeleteTaskAsync(taskModel task)
        {
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<taskModel> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Location)
                .Include(t => t.Product)
                .Include(t => t.Ramp)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<taskModel> GetNumOfTasksWithProductAsync(string barcode)
        {
            return await _context.Tasks.FirstOrDefaultAsync( b => b.ProductBarcode == barcode);
        }

        public async Task <List<taskModel>> GetFreeToTakeTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Location)
                .Include(t => t.Product)
                .Include(t => t.Ramp)
                .Where(t => t.Status == "toDo")
                .ToListAsync();
        }

        public async Task AssignedUserToTask(int UserId, int TaskId)
        {
            var task = await _context.Tasks.FindAsync(TaskId);
            if (task != null)
            {
                task.EmployeeId = UserId;
                task.Status = "taken";
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<taskModel>> GetUserTasksAsync(int UserId)
        {
            return await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Location)
                .Include(t => t.Product)
                .Include(t => t.Ramp)
                .Where(t => t.EmployeeId == UserId  && t.Status != "done")
                .ToListAsync();
        }
        public async Task AbondedUserTask(int TaskId)
        {
            var task = await _context.Tasks.FindAsync(TaskId);
            if (task != null)
            {
                task.EmployeeId = null;
                task.Status = "todo";
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
