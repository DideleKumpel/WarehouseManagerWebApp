using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WareHouseManagerWebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page(); // Wy�wietlamy stron� logowania
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Username and password are required";
                return Page();
            }

            // Prosta walidacja na sztywno (potem pod��czysz baz�!)
            if (Username == "admin" && Password == "admin123")
            {
                // TODO: tu dodasz autoryzacj� i cookies / claims!
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Incorrect login or password.";
            return Page();
        }
    }
}
