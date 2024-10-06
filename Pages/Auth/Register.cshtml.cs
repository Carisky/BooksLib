using BooksLib.models;
using BooksLib.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace BooksLib.Pages.Auth
{
    public class RegisterModel : PageModel
    {

        private readonly UserService _userService;

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public class RegisterInputModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public IActionResult OnPost()
        {
            if (_userService.IsUserAlreadyExist(Input.UserName))
            {
                ModelState.AddModelError(string.Empty, "User Already Exist");
                return Page();
            }
            else
            {
                _userService.WriteNew(Input.UserName, Input.Password);
                return RedirectToPage("/Auth/Login");
            }
        }
    }

}
