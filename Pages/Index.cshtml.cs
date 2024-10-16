using Microsoft.AspNetCore.Mvc.RazorPages;
using BooksLib.services;
using BooksLib.models;
using Microsoft.AspNetCore.Mvc;

namespace BooksLib.Pages;

public class IndexModel : PageModel
{
    private readonly BookService _bookService;


    public List<Book> BooksList { get; set; }

    public IndexModel(BookService bookService)
    {
        _bookService = bookService;
    }
    public IActionResult OnPostOnButtonClick(int BookId)
    {
        Console.WriteLine($"Book ID: {BookId}");
        return RedirectToPage();
    }

    public void OnGet()
    {
        BooksList = _bookService.ReadAll();
    }
}
