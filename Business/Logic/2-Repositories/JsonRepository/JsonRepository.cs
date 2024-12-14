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
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Factory.Interfaces;

namespace Business.Logic._2_Repositories
{
    public class JsonRepository : IJsonRepository, ICreate<BaseUser>, IRead<BaseUser>, IUpdate<BaseUser>, IDelete<BaseUser>
    {
        private readonly string _filePath;
        private readonly IExampleUserCreate _exampleUserCreate;
        // Konstruktor som tar emot filvägen via Dependency Injection
        public JsonRepository(string filePath, IExampleUserCreate exampleUserCreate)
        {
            _filePath = filePath;
            _exampleUserCreate = exampleUserCreate;
        }

        // Läser alla objekt från JSON-filen
        public List<T> ReadAll<T>() where T : class
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();  // Return an empty list if the file doesn't exist
            }

            var jsonData = File.ReadAllText(_filePath);  // Read the entire file as a string

            // Check if the file is empty or contains only whitespace
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return new List<T>();
            }

            var jsonArray = JsonConvert.DeserializeObject<List<JObject>>(jsonData);

            // Check if deserialization resulted in null
            if (jsonArray == null)
            {
                return new List<T>();
            }

            List<T> users = new List<T>();

            // Iterate through each item and determine the role
            foreach (var item in jsonArray)
            {
                string role = item["Role"]?.ToString();
                T user = default(T);

                if (role == "Admin")
                {
                    user = item.ToObject<Admin>() as T;
                }
                else if (role == "Default")
                {
                    user = item.ToObject<DefaultUser>() as T;
                }

                if (user != null)
                {
                    users.Add(user);
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


        //
        //CONTACT
        //

        // Läser alla kontakter för en specifik användare
        public List<Contact> ReadContactsForUser(string userId)
        {
            var users = ReadAll<BaseUser>();
            var user = users.FirstOrDefault(u => u.Id == userId);
            return user?.Contacts ?? new List<Contact>();
        }

        // Skriver uppdaterade kontakter för en specifik användare
        public void WriteContactsForUser(string userId, List<Contact> contacts)
        {
            var users = ReadAll<BaseUser>();
            var user = users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Contacts = contacts;
                WriteAll(users);
            }
        }
        // Läser alla favoriter för en specifik användare
        public List<FavoriteContact> ReadFavoritesForUser(string userId)
        {
            var users = ReadAll<BaseUser>();
            var user = users.FirstOrDefault(u => u.Id == userId);
            return user?.Favorites ?? new List<FavoriteContact>();
        }

        // Skriver uppdaterade favoriter för en specifik användare
        public void WriteFavoritesForUser(string userId, List<FavoriteContact> favorites)
        {
            var users = ReadAll<BaseUser>();
            var user = users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Favorites = favorites;
                WriteAll(users);
            }
        }

        //
        //ExampleUser
        //
        public void EnsureExampleUserExists()
        {
            var users = ReadAll<BaseUser>();

            // Kontrollera om ExampleUser redan finns
            if (!users.Any(u => u.Email == "x@x.xx"))
            {
                var exampleUser = _exampleUserCreate.CreateExampleUser();
                users.Insert(0, exampleUser); // Lägg till ExampleUser högst upp i listan
                WriteAll(users);
            }
        }


    }
}
