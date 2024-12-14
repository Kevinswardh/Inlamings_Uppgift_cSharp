using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Business.CoreFiles.Crud;
using Business.CoreFiles.Models.Users;
using System.Xml;
using Business.CoreFiles.Models.Users.Roles;
using Newtonsoft.Json.Linq;
using Business.Interfaces.IUser;
using Business._2_Repositories.JsonRepository.Interface;

namespace Business.Logic._2_Repositories
{
    public class JsonRepository : IJsonRepository, ICreate<BaseUser>, IRead<BaseUser>, IUpdate<BaseUser>, IDelete<BaseUser>
    {
        private readonly string _filePath;

        // Konstruktor som tar emot filvägen via Dependency Injection
        public JsonRepository(string filePath)
        {
            _filePath = filePath;
        }

        // Läser alla objekt från JSON-filen
        public List<T> ReadAll<T>() where T : class  // Lägg till 'where T : class' för att säkerställa att T är en klass
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();  // Om filen inte finns, returnera en tom lista
            }

            var jsonData = File.ReadAllText(_filePath);  // Läs hela filen som en sträng
            var jsonArray = JsonConvert.DeserializeObject<List<JObject>>(jsonData);  // Läs data som en lista av JObject för att manipulera den

            List<T> users = new List<T>();

            // Gå igenom alla objekt och avgör om det ska vara en Admin eller DefaultUser
            foreach (var item in jsonArray)
            {
                string role = item["Role"]?.ToString();  // Hämta roll från JSON-objektet
                T user = default(T);  // Definiera ett standardvärde för T

                if (role == "Admin")
                {
                    user = item.ToObject<Admin>() as T;  // Deserialisera till Admin om rollen är Admin
                }
                else if (role == "Default")
                {
                    user = item.ToObject<DefaultUser>() as T;  // Annars deserialisera till DefaultUser
                }

                if (user != null)
                {
                    users.Add(user);  // Lägg till användaren i listan
                }
            }

            return users;
        }




     /*   public BaseUser ReadUserById(string userId)
        {
            if (!File.Exists(_filePath))
            {
                return null;  // Om filen inte finns, returnera null
            }

            var jsonData = File.ReadAllText(_filePath);  // Läs hela filen som en sträng
            var jsonArray = JsonConvert.DeserializeObject<List<JObject>>(jsonData);  // Läs data som en lista av JObject för att kunna manipulera den

            // Gå igenom alla objekt och avgör om det ska vara en Admin eller DefaultUser
            foreach (var item in jsonArray)
            {
                var id = item["Id"]?.ToString();  // Hämta Id från JSON-objektet

                if (id == userId)  // Jämför med det givna userId
                {
                    string role = item["Role"]?.ToString();  // Hämta roll från JSON-objektet
                    BaseUser user = null;

                    if (role == "Admin")
                    {
                        user = item.ToObject<Admin>();  // Deserialisera till Admin om rollen är Admin
                    }
                    else if (role == "Default")
                    {
                        user = item.ToObject<DefaultUser>();  // Annars deserialisera till DefaultUser
                    }

                    return user;  // Returnera användaren som matchar ID:t
                }
            }

            return null;  // Om ingen användare matchar, returnera null
        }
     */

        // Skriver alla objekt till JSON-filen
        public void WriteAll<T>(List<T> data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);  // Explicitly specify Newtonsoft.Json.Formatting
            File.WriteAllText(_filePath, jsonData);  // Skriv data till filen
        }


        // ICreate implementation
        public void Create(BaseUser user)
        {
            var users = ReadAll<BaseUser>();
            users.Add(user);  // Add the new user
            WriteAll(users);  // Write back all users to the file
        }
   

        public BaseUser Get(string id)
        {
            var users = ReadAll<BaseUser>();
            return users.FirstOrDefault(u => u.Id == id); 
        }

        public List<BaseUser> GetAll()
        {
            return ReadAll<BaseUser>();
        }

  
        public void Update(BaseUser user)
        {
            var users = ReadAll<BaseUser>();
            var existingUser = users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser != null)
            {
                
                users.Remove(existingUser);
                users.Add(user);
                WriteAll(users);  
            }
        }

    
        public void Delete(string id)
        {
            var users = ReadAll<BaseUser>();
            var userToRemove = users.FirstOrDefault(u => u.Id == id);

            if (userToRemove != null)
            {
                users.Remove(userToRemove); 
                WriteAll(users);  
            }
        }

        public BaseUser GetUserByEmail(string email)
        {
            var users = ReadAll<BaseUser>();  
            return users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower()); 
        }
    }
}
