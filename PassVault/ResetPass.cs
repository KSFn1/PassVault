using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json; 
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http;

namespace PassVault
{
    public partial class ResetPass : Form
    {
        //Set the path for the image 
        private readonly string ImagePath = Path.Combine(Application.StartupPath, "images");
        //Set the path for the database
        private const string FirebaseUrl = "https://passvault-eccee-default-rtdb.firebaseio.com";
        private string userEmail;
        public ResetPass(string email)
        {
            InitializeComponent();
            //Set the email
            userEmail = email.Trim();
        }

        //Obtain the user data
        private class UserRecord
        {
            public string username { get; set; }
            public string Email { get; set; }
            public string Salt { get; set; }
            public string Hash { get; set; }
            public int Iterations { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        //Find the user on the cloud
        private async Task<UserRecord> GetUserFromCloudAsync(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Connect to the database
                    HttpResponseMessage response = await client.GetAsync($"{FirebaseUrl}/Users.json");
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Failed to connect to Firebase.");
                        return null;
                    }

                    string jsonString = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(jsonString) || jsonString == "null")
                    {
                        MessageBox.Show("No users found in Firebase.");
                        return null;
                    }

                    using (JsonDocument doc = JsonDocument.Parse(jsonString))
                    {
                        foreach (JsonProperty userProperty in doc.RootElement.EnumerateObject())
                        {
                            try
                            {
                                JsonElement val = userProperty.Value;

                                // Value is a Base64 string (encrypted JSON)
                                if (val.ValueKind == JsonValueKind.String)
                                {
                                    string encryptedBase64 = val.GetString();
                                    if (string.IsNullOrEmpty(encryptedBase64)) continue;

                                    byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
                                    string decryptedJson = DecryptData(encryptedBytes);
                                    var user = JsonSerializer.Deserialize<UserRecord>(decryptedJson);

                                    if (user != null && !string.IsNullOrEmpty(user.Email) &&
                                        user.Email.Trim().Equals(email.Trim(), StringComparison.OrdinalIgnoreCase))
                                        return user;
                                }

                                // Value is an object with Data property containing Base64 encrypted JSON
                                else if (val.ValueKind == JsonValueKind.Object && val.TryGetProperty("Data", out JsonElement dataElem) && dataElem.ValueKind == JsonValueKind.String)
                                {
                                    string encryptedBase64 = dataElem.GetString();
                                    if (string.IsNullOrEmpty(encryptedBase64)) continue;

                                    byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
                                    string decryptedJson = DecryptData(encryptedBytes);
                                    var user = JsonSerializer.Deserialize<UserRecord>(decryptedJson);

                                    if (user != null && !string.IsNullOrEmpty(user.Email) &&
                                        user.Email.Trim().Equals(email.Trim(), StringComparison.OrdinalIgnoreCase))
                                        return user;
                                }

                                // Value is an object with Email directly in plaintext
                                else if (val.ValueKind == JsonValueKind.Object && val.TryGetProperty("Email", out JsonElement emailElem))
                                {
                                    string firebaseEmail = emailElem.GetString();
                                    if (!string.IsNullOrEmpty(firebaseEmail) &&
                                        firebaseEmail.Trim().Equals(email.Trim(), StringComparison.OrdinalIgnoreCase))
                                    {
                                        return JsonSerializer.Deserialize<UserRecord>(val.GetRawText());
                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }

                    return null; // user not found
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching user from cloud: {ex.Message}");
                    return null;
                }
            }
        }


        // Update user record in the cloud
        private async Task<bool> UpdateUserInCloudAsync(string email, UserRecord updatedUser)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage getResponse = await client.GetAsync($"{FirebaseUrl}/Users.json");
                if (!getResponse.IsSuccessStatusCode) return false;

                string jsonString = await getResponse.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(jsonString) || jsonString == "null") return false;

                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    foreach (JsonProperty userProperty in doc.RootElement.EnumerateObject())
                    {
                        try
                        {
                            JsonElement val = userProperty.Value;

                            // Object with Data property
                            if (val.ValueKind == JsonValueKind.Object && val.TryGetProperty("Data", out JsonElement dataElem) && dataElem.ValueKind == JsonValueKind.String)
                            {
                                Console.WriteLine("Hello");
                                string encryptedBase64 = dataElem.GetString();
                                byte[] bytes = Convert.FromBase64String(encryptedBase64);
                                string decryptedJson = DecryptData(bytes);
                                var user = JsonSerializer.Deserialize<UserRecord>(decryptedJson);

                                //Find the email and decrypt it
                                if (user.Email.Trim().Equals(email.Trim(), StringComparison.OrdinalIgnoreCase))
                                {
                                    string updatedJson = JsonSerializer.Serialize(updatedUser);
                                    byte[] encryptedBytes = ProtectData(updatedJson);
                                    string updatedBase64 = Convert.ToBase64String(encryptedBytes);

                                    // Update the Data property
                                    string patchJson = $"{{\"Data\":\"{updatedBase64}\"}}";
                                    HttpResponseMessage patchResponse = await client.PatchAsync(
                                        $"{FirebaseUrl}/Users/{userProperty.Name}.json",
                                        new StringContent(patchJson, Encoding.UTF8, "application/json")
                                    );
                                    return patchResponse.IsSuccessStatusCode;
                                }
                            }
                        }
                        catch { continue; }
                    }
                }
                return false;
            }
        }

        //Generate a new salt
        private static byte[] GenerateSalt(int size = 16)
        {
            byte[] salt = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        //Hash the password with salt
        private static byte[] HashPassword(String password, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        //Encrypte data using DPAPI
        private static byte[] ProtectData(string plainText)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            return ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
        }


        // Decrypt data using DPAPI
        private static string DecryptData(byte[] encryptedData)
        {
            byte[] decrypted = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }

        //Set the criteria for a strong password
        private static bool IsStrongPassword(string password)
        {
            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSymbol = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasDigit && hasSymbol && password.Length >= 8;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Go back to the login page
            Login newForm = new Login();
            newForm.Show();
            this.Hide();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string newPassword = textBox1.Text.Trim();
            string confirmPassword = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in both password fields.");
                return;
            }

            if (!IsStrongPassword(newPassword))
            {
                MessageBox.Show("Password must include one upper case, one lower case, one symbol, and one number, and be at least 8 characters long.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords don't match.");
                return;
            }

            // 🔹 Try reading from cloud database first
            var cloudUser = await GetUserFromCloudAsync(userEmail);

            if (cloudUser != null)
            {
                // Re-hash and update
                byte[] newSalt = GenerateSalt();
                byte[] newHash = HashPassword(newPassword, newSalt, 100000);

                cloudUser.Salt = Convert.ToBase64String(newSalt);
                cloudUser.Hash = Convert.ToBase64String(newHash);
                cloudUser.Iterations = 100000;

                bool updated = await UpdateUserInCloudAsync(userEmail, cloudUser);
                if (updated)
                {
                    MessageBox.Show("Password reset successful!");
                }
                else
                {
                    MessageBox.Show("Error updating password in cloud database.");
                }
            }
            else
            {
                MessageBox.Show("User not found in cloud database.");
            }

            // Go back to login
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Change the picture and setting depending on the situation
            if (textBox1.PasswordChar == '*')
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
                button3.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
            }
            else
            {
                textBox1.PasswordChar = '*';
                textBox2.PasswordChar = '*';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
                button3.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Change the picture and setting depending on the situation
            if (textBox1.PasswordChar == '*')
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
                button3.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
            }
            else
            {
                textBox1.PasswordChar = '*';
                textBox2.PasswordChar = '*';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
                button3.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
            }
        }
    }
}
