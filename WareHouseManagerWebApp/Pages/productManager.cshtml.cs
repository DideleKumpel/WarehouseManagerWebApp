using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;
using WareHouseManagerWebApp.Service;

namespace WareHouseManagerWebApp.Pages
{
    [Authorize(Roles = "manager")]
    public class productManagerModel : PageModel
    {
        private readonly productService _productService;

        [BindProperty]
        public productModel Input { get; set; }

        public List<productModel> Products { get; set; }

        public productManagerModel( AppDbContext context)
        {
            _productService = new productService(context);
        }

        public async Task OnGet()
        {
            Products = await _productService.LoadProductsAsync();

        }

        public async Task OnPostAsync()
        {

        }
    }
}
