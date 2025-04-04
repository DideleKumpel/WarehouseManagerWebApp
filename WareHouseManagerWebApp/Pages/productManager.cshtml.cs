using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
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
        private readonly locationsServise _locationService;
        private readonly taskService _taskService;

        [BindProperty]
        public inputModel Input { get; set; }
        public class inputModel
        {
            [Required]
            [StringLength(50, ErrorMessage = "Name is to long (max 50)")]
            public string Name { get; set; }
            [Required]
            [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than 0.01")]
            public double Weight { get; set; }
            [Required]
            [StringLength(50, ErrorMessage = "Category is to long (max 50)")]
            public string Category { get; set; }
            [Required]
            [StringLength(50, ErrorMessage = "Barcode is to long (max 50)")]
            public string Barcode { get; set; }
            [Required]
            [StringLength(350, ErrorMessage = "Description is to long (max 350)")]
            public string Description { get; set; }
        }
        [BindProperty(SupportsGet = true)]
        public List<productModel> Products { get; set; }

        public productManagerModel(AppDbContext context)
        {
            _productService = new productService(context);
            _locationService = new locationsServise(context);
            _taskService = new taskService(context);
        }

        public async Task OnGet()
        {
            Products = await _productService.LoadProductsAsync();

        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid value in forms. Proudct was not added";
                Products = await _productService.LoadProductsAsync();
                return Page();
            }

            productModel product = new productModel
            {
                Name = Input.Name,
                Weight = Input.Weight,
                Category = Input.Category,
                Barcode = Input.Barcode,
                Description = Input.Description
            };

            try
            {
                await _productService.AddProductAsync(product);
                TempData["SuccessMessage"] = "Product added";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Error ocurred while adding task.";
            }

            Products = await _productService.LoadProductsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                TempData["ErrorMessage"] = "Product not selected";
                Products = await _productService.LoadProductsAsync();
                return Page();
            }
            if (await _locationService.GetSpaceIdWithProduct(barcode) != -1)
            {
                TempData["ErrorMessage"] = "Can't delete product that is in stock";
                Products = await _productService.LoadProductsAsync();
                return Page();
            }
            if (await _taskService.GetNumOfTasksWithProductAsync(barcode) != null)
            {
                TempData["ErrorMessage"] = "Can't delete product that is in stock";
                Products = await _productService.LoadProductsAsync();
                return Page();
            }
            try
            {
                await _productService.DeleteProductAsync(barcode);
                TempData["SuccessMessage"] = "Product deleted";
            }
            catch
            {
                TempData["SuccessMessage"] = "Error ocured while deleting";
            }

            Products = await _productService.LoadProductsAsync();
            return Page();
        }
    }
    
}
