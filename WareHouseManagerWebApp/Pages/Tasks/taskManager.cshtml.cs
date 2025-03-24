using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;
using WareHouseManagerWebApp.Service;

namespace WareHouseManagerWebApp.Pages.Tasks
{
    public class taskManagerModel : PageModel
    {

       private readonly taskService _taskService;
       private readonly rampService _rampService;

        public taskManagerModel(AppDbContext context)
        {
            _taskService = new taskService(context);
            _rampService = new rampService(context);
        }

        // Task list
        public List<taskModel> Tasks { get; set; }

        // Task input for adding and editing tasks
        [BindProperty]
        public taskModel TaskInput { get; set; }

        [BindProperty]
        public string TaskTypeValue { get; set; }
        [BindProperty]
        public RampselectViewModel RampSelect { get; set; }


        public async Task OnGetAsync()
        {
            //Load tasks
            Tasks = await _taskService.GetAllTasksAsync();

            //Load load ramps name for select box

            var ramps = await _rampService.LoadRampsAsync();
            RampSelect = new RampselectViewModel
            {
                Ramps = ramps.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList()
            };

        }



    }
    
    public class RampselectViewModel
    {
        public int SelectedRampId { get; set; }
        public List<SelectListItem> Ramps { get; set; }
    }
    
}

