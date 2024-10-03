using BooksLib.interfaces;
using BooksLib.utils;

namespace BooksLib.models;

public class Book : CRUD
{
    public int Id { get; set; }
    public string Author { get; set; } = String.Empty;
    public string Title { get; set; } = String.Empty;
    public string Description { get; } = String.Empty;
    public DateTime PublishedAt { get; set; } = DateTime.MinValue;
    public string Plot { get; set; } = String.Empty;

    public Book(int Id, string Author, string Title, string Description, DateTime PublishedAt, string Plot){
        this.Id = Id;
        this.Author = Author;
        this.Title = Title;
        this.Description = Description;
        this.PublishedAt = PublishedAt;
        this.Plot = Plot;
    }

    public void Save()
    {
        Writer.WriteBook(this);
    }
    public void Read()
    {
        
    }
    public void Update()
    {

    }
    public void Delete()
    {

    }

}
