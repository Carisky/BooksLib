using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace BooksLib.Pages.Auth{
public class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterInputModel Input { get; set; }

    public class RegisterInputModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public IActionResult OnPost()
    {
        // Логика регистрации пользователя
        // Добавьте проверку и сохранение нового пользователя
        return RedirectToPage("/Auth/Login");
    }
}

}
