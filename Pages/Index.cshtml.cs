using Microsoft.AspNetCore.Mvc.RazorPages;
using BooksLib.services;
using BooksLib.models;

namespace BooksLib.Pages;

public class IndexModel : PageModel
{
    private readonly BookService _bookService;
    private readonly ILogger<IndexModel> _logger;

    
    public Book CurrentBook { get; set; }

    public IndexModel(ILogger<IndexModel> logger, BookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    public void OnGet()
    {
        CurrentBook = _bookService.ReadById(1);
        _logger.LogInformation("Loaded book with ID: {BookId}", CurrentBook.Id);
    }
}
