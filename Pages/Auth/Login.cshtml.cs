using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace BooksLib.Pages.Auth
{
    public class LoginModel : PageModel
{
    [BindProperty]
    public LoginInputModel Input { get; set; }

    public class LoginInputModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public IActionResult OnPost()
    {
        // Логика аутентификации
        if (Input.UserName == "admin" && Input.Password == "password")
        {
            // Успешный вход, можно перенаправить
            return RedirectToPage("/Index");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}

}
