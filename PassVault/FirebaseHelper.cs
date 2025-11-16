using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassVault
{
    public static class FirebaseHelper
    {
        private static readonly string firebaseUrl = "https://passvault-eccee-default-rtdb.firebaseio.com/";
        private static readonly FirebaseClient client = new FirebaseClient(firebaseUrl);

        // Save a user (info.txt equivalent)
        public static async Task SaveUserAsync(string username, string encryptedData)
        {
            // Wrap the Base64 string in a JSON object
            var record = new { Data = encryptedData };

            await client.Child("Users")
                        .Child(username)
                        .PutAsync(record);
        }

        // Get all users
        public class FirebaseUserRecord
        {
            public string Data { get; set; }
        }

        public static async Task<Dictionary<string, string>> GetAllUsersAsync()
        {
            var users = await client.Child("Users").OnceAsync<FirebaseUserRecord>();
            return users.ToDictionary(u => u.Key, u => u.Object.Data); // read the Data property
        }

        // Save user credentials (username_info.txt equivalent)
        public static async Task SaveUserCredentialsAsync(string username, List<string> encryptedLines)
        {
            await client.Child("UserData")
                        .Child(username)
                        .PutAsync(encryptedLines);
        }

        // Load user credentials
        public static async Task<List<string>> LoadUserCredentialsAsync(string username)
        {
            var result = await client.Child("UserData")
                                     .Child(username)
                                     .OnceSingleAsync<List<string>>();

            return result ?? new List<string>();
        }
    }
}
