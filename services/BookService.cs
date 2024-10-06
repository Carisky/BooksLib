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
            string? directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory) && directory != null)
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
                string? idLine = sr.ReadLine();
                string? author = sr.ReadLine();
                string? bookTitle = sr.ReadLine();
                string? description = sr.ReadLine();
                string? publishedAtLine = sr.ReadLine();
                string? plot = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(idLine))
                    throw new InvalidDataException("ID line is empty or null.");
                if (string.IsNullOrWhiteSpace(author))
                    throw new InvalidDataException("Author line is empty or null.");
                if (string.IsNullOrWhiteSpace(bookTitle))
                    throw new InvalidDataException("Title line is empty or null.");
                if (string.IsNullOrWhiteSpace(description))
                    throw new InvalidDataException("Description line is empty or null.");
                if (string.IsNullOrWhiteSpace(publishedAtLine))
                    throw new InvalidDataException("PublishedAt line is empty or null.");
                if (string.IsNullOrWhiteSpace(plot))
                    throw new InvalidDataException("Plot line is empty or null.");
                if (!int.TryParse(idLine, out int id))
                    throw new FormatException("ID is not a valid integer.");
                if (!DateTime.TryParse(publishedAtLine, out DateTime publishedAt))
                    throw new FormatException("PublishedAt is not a valid DateTime.");
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
                    string? idLine = sr.ReadLine();
                    string? author = sr.ReadLine();
                    string? title = sr.ReadLine();
                    string? description = sr.ReadLine();
                    string? publishedAtLine = sr.ReadLine();
                    string? plot = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(idLine))
                        throw new InvalidDataException($"ID line is empty or null in file: {file}");
                    if (string.IsNullOrWhiteSpace(author))
                        throw new InvalidDataException($"Author line is empty or null in file: {file}");
                    if (string.IsNullOrWhiteSpace(title))
                        throw new InvalidDataException($"Title line is empty or null in file: {file}");
                    if (string.IsNullOrWhiteSpace(description))
                        throw new InvalidDataException($"Description line is empty or null in file: {file}");
                    if (string.IsNullOrWhiteSpace(publishedAtLine))
                        throw new InvalidDataException($"PublishedAt line is empty or null in file: {file}");
                    if (string.IsNullOrWhiteSpace(plot))
                        throw new InvalidDataException($"Plot line is empty or null in file: {file}");
                    if (!int.TryParse(idLine, out int id))
                        throw new FormatException($"ID is not a valid integer in file: {file}");
                    if (!DateTime.TryParse(publishedAtLine, out DateTime publishedAt))
                        throw new FormatException($"PublishedAt is not a valid DateTime in file: {file}");
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
                    string ?idLine = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(idLine))
                        throw new InvalidDataException($"ID line is empty or null in file: {file}");
                    if (!int.TryParse(idLine, out int bookId))
                        throw new FormatException($"ID is not a valid integer in file: {file}");
                    if (bookId == id)
                    {
                        string ?author = sr.ReadLine();
                        string ?title = sr.ReadLine();
                        string ?description = sr.ReadLine();
                        string ?publishedAtLine = sr.ReadLine();
                        string ?plot = sr.ReadLine();
                        if (string.IsNullOrWhiteSpace(author))
                            throw new InvalidDataException($"Author line is empty or null in file: {file}");
                        if (string.IsNullOrWhiteSpace(title))
                            throw new InvalidDataException($"Title line is empty or null in file: {file}");
                        if (string.IsNullOrWhiteSpace(description))
                            throw new InvalidDataException($"Description line is empty or null in file: {file}");
                        if (string.IsNullOrWhiteSpace(publishedAtLine))
                            throw new InvalidDataException($"PublishedAt line is empty or null in file: {file}");
                        if (string.IsNullOrWhiteSpace(plot))
                            throw new InvalidDataException($"Plot line is empty or null in file: {file}");
                        if (!DateTime.TryParse(publishedAtLine, out DateTime publishedAt))
                            throw new FormatException($"PublishedAt is not a valid DateTime in file: {file}");
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
