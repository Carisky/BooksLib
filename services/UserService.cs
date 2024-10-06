using System.IO;
using System.Text.RegularExpressions;
using BooksLib.config;
using BooksLib.interfaces;
using BooksLib.models;

namespace BooksLib.services
{
    public partial class UserService : ISERVICE<User>
    {
        private static string BasePath = Config.UsersStoragePath;

        private static string SanitizeFileName(string fileName)
        {
            return MyRegex().Replace(fileName, "_");
        }

        public void Write(User user)
        {
            string sanitizedUserName = SanitizeFileName(user.UserName);
            string filePath = Path.Combine(BasePath, $"{sanitizedUserName}.txt");
            string? directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory) && directory != null)
            {
                Directory.CreateDirectory(directory);
            }
            using (StreamWriter sw = new StreamWriter(filePath, append: false))
            {
                sw.WriteLine(user.Id);
                sw.WriteLine(user.UserName);
                sw.WriteLine(user.Password);
            }
        }

        public User Read(string userName)
        {
            string sanitizedUserName = SanitizeFileName(userName);
            string filePath = Path.Combine(BasePath, $"{sanitizedUserName}.txt");
            if (!File.Exists(filePath))
                throw new FileNotFoundException("User file not found");
            using (StreamReader sr = new StreamReader(filePath))
            {

                if (!int.TryParse(sr.ReadLine(), out int id))
                    throw new InvalidDataException("Invalid user ID");
                string? username = sr.ReadLine();
                string? password = sr.ReadLine();
                if (string.IsNullOrEmpty(username))
                    throw new InvalidDataException("Username is missing or invalid");
                if (string.IsNullOrEmpty(password))
                    throw new InvalidDataException("Password is missing or invalid");
                return new User(id, username, password);
            }
        }

        public List<User> ReadAll()
        {
            List<User> users = new List<User>();
            if (!Directory.Exists(BasePath))
                throw new DirectoryNotFoundException("Users directory not found");
            foreach (var file in Directory.GetFiles(BasePath, "*.txt"))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string? id_string = sr.ReadLine();
                    if (!string.IsNullOrEmpty(id_string))
                    {
                        int id = int.Parse(id_string);
                        string? username = sr.ReadLine();
                        string? password = sr.ReadLine();
                        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                        {
                            users.Add(new User(id, username, password));
                        }
                        else
                        {
                            throw new InvalidDataException("Invalid user data");
                        }
                    }
                }
            }
            return users;
        }

        public User ReadById(int id)
        {
            if (!Directory.Exists(BasePath))
                throw new DirectoryNotFoundException("Users directory not found");
            foreach (var file in Directory.GetFiles(BasePath, "*.txt"))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    if (!int.TryParse(sr.ReadLine(), out int userId))
                        throw new InvalidDataException("Invalid user ID");
                    if (userId == id)
                    {
                        string ?username = sr.ReadLine();
                        string ?password = sr.ReadLine();
                        if (string.IsNullOrEmpty(username))
                            throw new InvalidDataException("Username is missing or invalid");
                        if (string.IsNullOrEmpty(password))
                            throw new InvalidDataException("Password is missing or invalid");
                        return new User(userId, username, password);
                    }
                }
            }
            throw new FileNotFoundException($"User with Id {id} not found");
        }
        [GeneratedRegex(@"[<>:""/\\|?*]")]
        private static partial Regex MyRegex();
    }
}
