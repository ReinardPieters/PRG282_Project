using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using PRG282Project.DataHandler;


namespace PRG282Project.BusinessProcess
{
    internal class UserManager
    {

        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");


        public List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
            {
                return new List<User>(); 
            }

            try
            {
                string jsonString = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<User>>(jsonString) ?? new List<User>();
            }
            catch (Exception ex)
            {
            
                Console.WriteLine($"Error reading users file: {ex.Message}");
                return new List<User>(); 
            }
        }

        public void SaveUsers(List<User> users)
        {
            string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(FilePath, jsonString);
        }

        public bool ValidateUser(string username, string password)
        {
            List<User> users = LoadUsers();

            string passwordHash = HashPassword(password);

            return users.Exists(user => user.Username == username && user.PasswordHash == passwordHash);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        public void AddUser(string username, string password)
        {
            List<User> users = LoadUsers();

            string passwordHash = HashPassword(password);

           
            if (users.Exists(user => user.Username == username))
            {
                throw new Exception("Username already exists.");
            }

 
            users.Add(new User { Username = username, PasswordHash = passwordHash });

            SaveUsers(users);
        }
    }
}