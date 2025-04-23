using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WareHouseManagerWebApp.Pages
{
    public class AccesDeniedModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index"); // lub np. "/Login" w zale�no�ci gdzie chcesz przekierowa� po wylogowaniu
        }
    }
}
