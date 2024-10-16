using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BooksLib.models;
using BooksLib.services;

namespace BooksLib.Pages
{
    public class BooksDetailsModel : PageModel
    {
        private readonly BookService _bookService;

        public Book SelectedBook { get; set; }

        public BooksDetailsModel(BookService bookService)
        {
            _bookService = bookService;
        }
        public void OnGet(int id)
        {
            SelectedBook = _bookService.ReadById(id);
        }
    }
}
