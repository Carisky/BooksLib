using System.IO;
using System.Text.RegularExpressions;
using BooksLib.config;
using BooksLib.models;

namespace BooksLib.utils
{
    public class Writer
    {
        private static string BasePath = Config.BooksStoragePath; 
        
        private static string SanitizeFileName(string fileName)
        {
            return Regex.Replace(fileName, @"[<>:""/\|?*]", "_");
        }

        
        public static void WriteBook(Book book)
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
    }
}
