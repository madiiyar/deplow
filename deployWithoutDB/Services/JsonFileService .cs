using deployAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace deployAPI.Services
{
    public class JsonFileService
    {
        private readonly string _filePath;

        public JsonFileService(string filePath)
        {
            _filePath = filePath;
        }

        // Retrieve all users from the JSON file
        public List<User> GetUsers()
        {
            if (!File.Exists(_filePath)) return new List<User>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        // Save users to the JSON file
        public void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
