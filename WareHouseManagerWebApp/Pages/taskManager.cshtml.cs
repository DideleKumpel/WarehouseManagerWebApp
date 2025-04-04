using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Security.Cryptography;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;
using WareHouseManagerWebApp.Service;

namespace WareHouseManagerWebApp.Pages.Tasks
{
    [Authorize(Roles = "manager")]
    public class taskManagerModel : PageModel
    {

        private readonly taskService _taskService;
        private readonly rampService _rampService;
        private readonly productService _productService;
        private readonly taskLocationCoordinationService _taskLocationCoordinationService;
        private readonly locationsServise _locationService;

        public taskManagerModel(AppDbContext context)
        {
            _taskService = new taskService(context);
            _rampService = new rampService(context);
            _productService = new productService(context);
            _taskLocationCoordinationService = new taskLocationCoordinationService(context);
            _locationService = new locationsServise(context);
        }

        // Task list
        public List<taskModel> Tasks { get; set; }

        // Task input for adding and editing tasks

        [BindProperty]
        public string TaskTypeValue { get; set; }
        [BindProperty]
        public RampselectViewModel RampSelect { get; set; }
        [BindProperty]
        public ProductrSelectViewModel ProductrSelect { get; set; }


        public async Task OnGetAsync()
        {
            //Load tasks
            Tasks = await _taskService.GetAllTasksAsync();

            //Load load ramps name for select box
            await FillRampsSelectBox();

            //load products for select box
            await FillPorductSelectBox();

        }
        //Method for adding new task
        public async Task<IActionResult> OnPostAddAsync()
        {
            
            taskModel taskToAdd = new taskModel
            {
                Type = TaskTypeValue,
                Status = "toDo",
                UploadDate = DateTime.Now,
                RampName = RampSelect.SelectedRampId,
                EmployeeId = null,
                LocationId = -1,
                ProductBarcode = ProductrSelect.SelectedItemBarcode
            };
            if (TaskTypeValue == "load")
            {
                taskToAdd.LocationId = await _locationService.GetSpaceIdWithProduct(taskToAdd.ProductBarcode);
                if (taskToAdd.LocationId == -1)
                {
                    TempData["ErrorMessage"] = "Product is not in stock";
                }
                else
                {
                    await _taskService.AddTaskAsync(taskToAdd);
                    TempData["SuccesMessage"] = "Task added";
                }
            }
            else if (TaskTypeValue == "unload")
            {
                taskToAdd.LocationId = await _locationService.GetEmptySpaceId();
                if (taskToAdd.LocationId == -1)
                {
                    TempData["ErrorMessage"] = "No empty space available";
                }
                else
                {
                    await _taskLocationCoordinationService.AddUnLoadTask(taskToAdd);
                    TempData["SuccesMessage"] = "Task added";
                }
            }

            Tasks = await _taskService.GetAllTasksAsync();
            OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int taskId)
        {

            taskModel task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                ModelState.AddModelError("All", "Error ocured while deleting");
                return RedirectToPage();
            }

            if (task.Status != "toDo")
            {
                ModelState.AddModelError("All", "Task is in progress");
                return RedirectToPage();
            }
            if (task.Type == "load")
            {
                await _taskLocationCoordinationService.DeleteLoadTask(task);
            }
            else if (task.Type == "unload")
            {
                await _taskLocationCoordinationService.DeleteUnloadTask(task);
            }
            return RedirectToPage();
        }

        public async Task FillRampsSelectBox()
        {
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
        public async Task FillPorductSelectBox()
        {
            var products = await _productService.LoadProductsAsync();
            ProductrSelect = new ProductrSelectViewModel
            {
                Products = products.Select(p => new SelectListItem
                {
                    Value = p.Barcode,
                    Text = p.Barcode + " " + p.Name
                }).ToList()
            };
        }

    }
    //classes for holidng data in select boxes 
    public class RampselectViewModel
    {
        public string SelectedRampId { get; set; }
        public List<SelectListItem> Ramps { get; set; }
    }
    public class ProductrSelectViewModel
    {
        public string SelectedItemBarcode { get; set; }
        public List<SelectListItem> Products { get; set; }
    }

}

