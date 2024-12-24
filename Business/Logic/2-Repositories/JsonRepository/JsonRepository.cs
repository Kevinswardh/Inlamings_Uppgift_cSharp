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
    /// <summary>
    /// Klass för att hantera CRUD-operationer för användare i en JSON-fil.
    /// Implementerar gränssnitt för skapande, läsning, uppdatering och borttagning.
    /// </summary>
    public class JsonRepository : IJsonRepository, ICreate<BaseUser>, IRead<BaseUser>, IUpdate<BaseUser>, IDelete<BaseUser>
    {
        private readonly string _filePath;
        private readonly IExampleUserCreate _exampleUserCreate;

        /// <summary>
        /// Konstruktor som tar emot filvägen och en instans av IExampleUserCreate via Dependency Injection.
        /// </summary>
        /// <param name="filePath">Sökvägen till JSON-filen.</param>
        /// <param name="exampleUserCreate">Fabrik för att skapa en exempelanvändare.</param>
        public JsonRepository(string filePath, IExampleUserCreate exampleUserCreate)
        {
            _filePath = filePath;
            _exampleUserCreate = exampleUserCreate;
        }

        /// <summary>
        /// Läser alla objekt från JSON-filen.
        /// </summary>
        /// <typeparam name="T">Typen av objekt som ska läsas.</typeparam>
        /// <returns>En lista med objekt av typen T.</returns>
        public List<T> ReadAll<T>() where T : class
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();  // Returnera en tom lista om filen inte finns.
            }

            var jsonData = File.ReadAllText(_filePath);  // Läs hela filen som en sträng.

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return new List<T>();  // Returnera en tom lista om filen är tom.
            }

            var jsonArray = JsonConvert.DeserializeObject<List<JObject>>(jsonData);

            if (jsonArray == null)
            {
                return new List<T>();  // Returnera en tom lista om deserialisering misslyckas.
            }

            List<T> users = new List<T>();

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

        /// <summary>
        /// Skriver alla objekt till JSON-filen.
        /// </summary>
        /// <typeparam name="T">Typen av objekt som ska skrivas.</typeparam>
        /// <param name="data">Listan med objekt som ska skrivas till filen.</param>
        public void WriteAll<T>(List<T> data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }

        /// <summary>
        /// Skapar en ny användare och sparar den i JSON-filen.
        /// </summary>
        /// <param name="user">Användaren som ska skapas.</param>
        public void Create(BaseUser user)
        {
            var users = ReadAll<BaseUser>();
            users.Add(user);
            WriteAll(users);
        }

        /// <summary>
        /// Hämtar en användare baserat på användarens ID.
        /// </summary>
        /// <param name="id">Användarens ID.</param>
        /// <returns>Användaren med det angivna ID:t.</returns>
        public BaseUser Get(string id)
        {
            var users = ReadAll<BaseUser>();
            return users.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Hämtar alla användare från JSON-filen.
        /// </summary>
        /// <returns>En lista med alla användare.</returns>
        public List<BaseUser> GetAll()
        {
            return ReadAll<BaseUser>();
        }

        /// <summary>
        /// Uppdaterar en befintlig användare.
        /// </summary>
        /// <param name="user">Användaren med uppdaterad information.</param>
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

        /// <summary>
        /// Tar bort en användare baserat på användarens ID.
        /// </summary>
        /// <param name="id">Användarens ID som ska tas bort.</param>
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

        /// <summary>
        /// Hämtar en användare baserat på e-postadress.
        /// </summary>
        /// <param name="email">Användarens e-postadress.</param>
        /// <returns>Användaren med den angivna e-postadressen.</returns>
        public BaseUser GetUserByEmail(string email)
        {
            var users = ReadAll<BaseUser>();
            return users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        //
        // KONTAKTHANTERING
        //

        /// <summary>
        /// Läser alla kontakter för en specifik användare.
        /// </summary>
        public List<Contact> ReadContactsForUser(string userId)
        {
            var users = ReadAll<BaseUser>();
            var user = users.FirstOrDefault(u => u.Id == userId);
            return user?.Contacts ?? new List<Contact>();
        }

        /// <summary>
        /// Skriver uppdaterade kontakter för en specifik användare.
        /// </summary>
        public void WriteContactsForUser(string userId, List<Contact> contacts)
        {
            var user = Get(userId);
            if (user != null)
            {
                user.Contacts = contacts;
                Update(user);
            }
        }

        /// <summary>
        /// Läser alla favoriter för en specifik användare.
        /// </summary>
        public List<FavoriteContact> ReadFavoritesForUser(string userId)
        {
            var user = Get(userId);
            return user?.Favorites ?? new List<FavoriteContact>();
        }

        /// <summary>
        /// Skriver uppdaterade favoriter för en specifik användare.
        /// </summary>
        public void WriteFavoritesForUser(string userId, List<FavoriteContact> favorites)
        {
            var user = Get(userId);
            if (user != null)
            {
                user.Favorites = favorites;
                Update(user);
            }
        }

        /// <summary>
        /// Säkerställer att en exempelanvändare finns i JSON-filen.
        /// </summary>
        public void EnsureExampleUserExists()
        {
            var users = ReadAll<BaseUser>();

            if (!users.Any(u => u.Email == "x@x.xx"))
            {
                var exampleUser = _exampleUserCreate.CreateExampleUser();
                users.Insert(0, exampleUser);
                WriteAll(users);
            }
        }
    }
}
