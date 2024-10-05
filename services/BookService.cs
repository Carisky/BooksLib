using System.IO;
using System.Text.RegularExpressions;
using BooksLib.config;
using BooksLib.interfaces;
using BooksLib.models;

namespace BooksLib.services
{
    public partial class BookService : ISERVICE<Book>
    {
        private static string BasePath = Config.BooksStoragePath;
        
        private static string SanitizeFileName(string fileName)
        {
            return MyRegex().Replace(fileName, "_");
        }
        
        public void Write(Book book)
        {
            string sanitizedTitle = SanitizeFileName(book.Title);
            string filePath = Path.Combine(BasePath, $"{sanitizedTitle}.txt");

            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            using (StreamWriter sw = new StreamWriter(filePath, append: false))
            {
                sw.WriteLine(book.Id);
                sw.WriteLine(book.Author);
                sw.WriteLine(book.Title);
                sw.WriteLine(book.Description);
                sw.WriteLine(book.PublishedAt);
                sw.WriteLine(book.Plot);
            }
        }
        
        public Book Read(string title)
        {
            string sanitizedTitle = SanitizeFileName(title);
            string filePath = Path.Combine(BasePath, $"{sanitizedTitle}.txt");
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Book file not found");
            using (StreamReader sr = new StreamReader(filePath))
            {
                int id = int.Parse(sr.ReadLine());
                string author = sr.ReadLine();
                string bookTitle = sr.ReadLine();
                string description = sr.ReadLine();
                DateTime publishedAt = DateTime.Parse(sr.ReadLine());
                string plot = sr.ReadLine();
                return new Book(id, author, bookTitle, description, publishedAt, plot);
            }
        }
        
        public List<Book> ReadAll()
        {
            List<Book> books = new List<Book>();
            if (!Directory.Exists(BasePath))
                throw new DirectoryNotFoundException("Books directory not found");
            foreach (var file in Directory.GetFiles(BasePath, "*.txt"))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    int id = int.Parse(sr.ReadLine());
                    string author = sr.ReadLine();
                    string title = sr.ReadLine();
                    string description = sr.ReadLine();
                    DateTime publishedAt = DateTime.Parse(sr.ReadLine());
                    string plot = sr.ReadLine();
                    books.Add(new Book(id, author, title, description, publishedAt, plot));
                }
            }
            return books;
        }
        
        public Book ReadById(int id)
        {
            if (!Directory.Exists(BasePath))
                throw new DirectoryNotFoundException("Books directory not found");
            foreach (var file in Directory.GetFiles(BasePath, "*.txt"))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    int bookId = int.Parse(sr.ReadLine());
                    if (bookId == id)
                    {
                        string author = sr.ReadLine();
                        string title = sr.ReadLine();
                        string description = sr.ReadLine();
                        DateTime publishedAt = DateTime.Parse(sr.ReadLine());
                        string plot = sr.ReadLine();
                        return new Book(bookId, author, title, description, publishedAt, plot);
                    }
                }
            }
            throw new FileNotFoundException($"Book with Id {id} not found");
        }
        [GeneratedRegex(@"[<>:""/\\|?*]")]
        private static partial Regex MyRegex();
    }
}
