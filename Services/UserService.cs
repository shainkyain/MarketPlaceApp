using MarketPlaceApp.Models;
using Newtonsoft.Json;

namespace MarketPlaceApp.Services
{
    public class UserService
    {

        private readonly string _filepath;
        public UserService()
        {
            _filepath = FindJsonFilePath();
        }
        private string FindJsonFilePath() 
        { string currentDirectory = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(currentDirectory, "Users.json", SearchOption.AllDirectories);
            if (files.Length > 0) 
            { return files[0];
            } else 
            {
                throw new FileNotFoundException("Users.json file not found."); }
        }

        // private readonly string _filepath = "C:\\Users\\Trainee\\Desktop\\c#\\MarketPlaceApp\\wwwroot\\js\\Users.json";

        public List<User> GetAllUsers()
        {
            if (!File.Exists(_filepath))
            {
                return new List<User>();
            }
            var jsonData = File.ReadAllText(_filepath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>() ;
        }

        public void AddUser(User newUser)
        {
            var allUsers = GetAllUsers();
            newUser.Id = allUsers.Any() ? allUsers.Max(users => users.Id)+1 : 1;
            newUser.Role = "User";
            
            allUsers.Add(newUser);
            SaveUsers(allUsers);
        }

        public void ChangeRole(User user)
        {
            var allUsers = GetAllUsers();
            User userToUpdate = allUsers.FirstOrDefault(u => u.Id == user.Id);
            var role = userToUpdate.Role;
           
            if(role == "User") { 
            userToUpdate.Role = "Admin";
            }else userToUpdate.Role = "User";
            SaveUsers(allUsers);
        }

        private void SaveUsers(List<User> allUsers)
        {
            var jsonData = JsonConvert.SerializeObject(allUsers, Formatting.Indented);
            File.WriteAllText(_filepath, jsonData);
        }
    }
}
