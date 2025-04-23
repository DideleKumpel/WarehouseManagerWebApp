using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;
using WareHouseManagerWebApp.Service;

namespace WareHouseManagerWebApp.Pages
{
    public class assignedTasksPanelModel : PageModel
    {
        private readonly taskService _taskService;
        private readonly taskLocationCoordinationService _taskLocationCoordinationService;


        [BindProperty]
        public List<taskModel> Tasks { get; set; }

        public assignedTasksPanelModel(AppDbContext appDbContext)
        {
            _taskService = new taskService(appDbContext);
            _taskLocationCoordinationService = new taskLocationCoordinationService(appDbContext);
            Tasks = new List<taskModel> { };
        }

        public async Task OnGet()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            Tasks = await _taskService.GetUserTasksAsync(userId);
        }

        public async Task<IActionResult> OnPostFinishAsync(int taskId)
        {
            try
            {
                taskModel task = await _taskService.GetTaskByIdAsync(taskId);
                if (task != null)
                {
                    switch (task.Type)
                    {
                        case "load":
                            await _taskLocationCoordinationService.FinishLoadTask(task);
                            break;
                        case "unload":
                            await _taskLocationCoordinationService.FinishUnloadTask(task);
                            break;
                        default:
                            TempData["ErrorMessage"] = "Invalid task type.";
                            break;
                    }
                    TempData["SuccessMessage"] = "Task finished successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Task not found.";
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while finishing the task.";
            }
            await OnGet();
            return Page();
        }
        public async Task<IActionResult> OnPostAbandonedAsync(int taskId)
        {
            // Assign the task to the employee
            int userId = 0;
            try
            {
                userId = int.Parse(User.FindFirst("UserId").Value); // Assuming you have a way to get the employee ID from the user
                await _taskService.AbondedUserTask(taskId);
                TempData["SuccessMessage"] = "Task abonded successfully.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Task not found.";
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index"); // lub np. "/Login" w zale¿noœci gdzie chcesz przekierowaæ po wylogowaniu
        }
    }
}
