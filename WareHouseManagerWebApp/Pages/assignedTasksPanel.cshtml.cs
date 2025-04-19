using Microsoft.AspNetCore.Mvc;
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


        [BindProperty]
        public List<taskModel> Tasks { get; set; }

        public assignedTasksPanelModel(AppDbContext appDbContext)
        {
            _taskService = new taskService(appDbContext);
            Tasks = new List<taskModel> { };
        }

        public async Task OnGet()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            Tasks = await _taskService.GetUserTasksAsync(userId);
        }
    }
}
