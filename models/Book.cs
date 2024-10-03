using BooksLib.interfaces;

namespace BooksLib.models;

public class Book : CRUD
{
    private int Id { get; set; }
    private string Author { get; set; } = String.Empty;
    private string Title { get; set; } = String.Empty;
    private string Description { get; } = String.Empty;
    private DateTime PublishedAt { get; set; } = DateTime.MinValue;
    private string Plot { get; set; } = String.Empty;

    public void Save()
    {
        
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
