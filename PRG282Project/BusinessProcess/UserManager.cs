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

        //Gets the list of users from the user.json file
        public List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
            {
                return new List<User>(); 
            }

            try
            {
                string jsonString = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<User>>(jsonString) ?? new List<User>();// Deserialize the JSON string (jsonString) into a List of User objects.
                                                                                                 // If deserialization returns null (e.g., if jsonString is empty or invalid),
                                                                                                 // an empty List<User> is returned instead.
            }
            catch (Exception ex)
            {
            
                Console.WriteLine($"Error reading users file: {ex.Message}");
                return new List<User>(); 
            }
        }
        //Saves a list of users to users.json
        public void SaveUsers(List<User> users)
        {
            string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(FilePath, jsonString);
        }
        //Checks input from user to compare with accounts in user.json
        public bool ValidateUser(string username, string password)
        {
            List<User> users = LoadUsers();

            string passwordHash = HashPassword(password); //hashes the input password to compare to the hashed password in user.json

            return users.Exists(user => user.Username == username && user.PasswordHash == passwordHash);//returns a noolean vallue if the user is found or not
        }
        //hashes the password before it is stored to increase security
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        //Adds new users
        public void AddUser(string username, string password)
        {
            List<User> users = LoadUsers();

            string passwordHash = HashPassword(password);

           
            if (users.Exists(user => user.Username == username))//Checks weather or not a username already exists
            {
                throw new Exception("Username already exists.");
            }

 
            users.Add(new User { Username = username, PasswordHash = passwordHash });//ads new user object to the list

            SaveUsers(users);//invokes the SaveUsers method with the users list
        }
    }
}