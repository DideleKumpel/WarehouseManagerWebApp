using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WareHouseManagerWebApp.Model;
using WareHouseManagerWebApp.Service;
using WareHouseManagerWebApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WareHouseManagerWebApp.Pages
{
    [Authorize(Roles = "manager")]
    public class ProductEditModel : PageModel
    {
        private readonly productService _productService;

        [BindProperty]
        public productModel OrginalProduct { get; set; }

        [BindProperty]
        public productModel Input { get; set; }


        public ProductEditModel(AppDbContext appContext)
        {
            _productService = new productService(appContext);
            Input = new productModel();
        }

        public async Task OnGetAsync(string id)
        {
                OrginalProduct = await _productService.GetProductByBarcodeAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            try { 
                OrginalProduct = await _productService.GetProductByBarcodeAsync(OrginalProduct.Barcode);
            }
            catch (Exception ex)
            {
                return LocalRedirect("/Error");
            }

            if (!NewDataEnetered())
            {
                TempData["ErrorMessage"] = "Enter new data min in one filed";
                return Page();
            }

            if (!string.IsNullOrEmpty(Input.Name)) {
                OrginalProduct.Name = Input.Name;
            }
            if (!string.IsNullOrEmpty(Input.Description))
            {
                OrginalProduct.Description = Input.Description;
            }
            if (!string.IsNullOrEmpty(Input.Category))
            {
                OrginalProduct.Category = Input.Category;
            }
            if (Input.Weight != 0)
            {
                OrginalProduct.Weight = Input.Weight;
            }

            try
            {
                await _productService.UpdateProductAsync(OrginalProduct);
                TempData["SuccessMessage"] = "Product updated";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                OrginalProduct = await _productService.GetProductByBarcodeAsync(OrginalProduct.Barcode);
                return Page();
            }

                
            OrginalProduct = await _productService.GetProductByBarcodeAsync(OrginalProduct.Barcode);
            return Page();
        }

        private  bool NewDataEnetered()
        {
            if (string.IsNullOrEmpty(Input.Name) && string.IsNullOrEmpty(Input.Description) && string.IsNullOrEmpty(Input.Category) && Input.Weight == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index"); // lub np. "/Login" w zale¿noœci gdzie chcesz przekierowaæ po wylogowaniu
        }
    }
}
