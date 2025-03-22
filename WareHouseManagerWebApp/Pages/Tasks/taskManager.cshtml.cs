using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;

namespace WareHouseManagerWebApp.Pages.Tasks
{
    public class taskManagerModel : PageModel
    {

       private readonly AppDbContext _context;

        public taskManagerModel(AppDbContext context)
        {
            _context = context;
        }

        // Task list
        public List<taskModel> Tasks { get; set; }

        // Task input for adding and editing tasks
        [BindProperty]
        public taskModel TaskInput { get; set; }

        // Obs³uga wyœwietlania listy
        public async Task OnGetAsync()
        {
            //Tasks = await _context.Tasks.ToListAsync();
        }



    }
    
}

