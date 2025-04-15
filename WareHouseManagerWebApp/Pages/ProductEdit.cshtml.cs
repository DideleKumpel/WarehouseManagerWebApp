using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WareHouseManagerWebApp.Model;
using WareHouseManagerWebApp.Service;

namespace WareHouseManagerWebApp.Pages
{
    [Authorize(Roles = "manager")]
    public class ProductEditModel : PageModel
    {
        private readonly productService _productService;
        public productModel Product { get; set; }

        public productModel Input { get; set; }

        public async Task OnGetAsync(int ProductId)
        {

        }
    }
}
