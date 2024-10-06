namespace BooksLib.models;

public class User(int Id, string UserName, string Password)
{
    public int Id { get; set; } = Id;
    public string UserName { get; set; } = UserName;
    public string Password { get; set; } = Password;
}
