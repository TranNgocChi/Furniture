using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            if (Username == "admin" && Password == "Admin@123")
            {
                return RedirectToPage("Products/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
