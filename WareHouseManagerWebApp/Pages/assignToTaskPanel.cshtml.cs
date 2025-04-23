using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;
using WareHouseManagerWebApp.Service;

namespace WareHouseManagerWebApp.Pages
{
    [Authorize]
    public class assignedToTaskPanelModel : PageModel
    {
        private readonly taskService _taskService;


        [BindProperty]
        public List<taskModel> Tasks { get; set; }

        public assignedToTaskPanelModel(AppDbContext appDbContext)
        {
            _taskService = new taskService(appDbContext);
        }

        public async Task OnGetAsync()
        {
            Tasks = await _taskService.GetFreeToTakeTasksAsync();
        }

        public async Task<IActionResult> OnPostAssignAsync(int taskId)
        {
            // Assign the task to the employee
            int userId = 0;
            try
            {
                userId = int.Parse(User.FindFirst("UserId").Value); // Assuming you have a way to get the employee ID from the user
                await _taskService.AssignedUserToTask(userId, taskId);
                TempData["SuccessMessage"] = "Task assigned successfully.";
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
