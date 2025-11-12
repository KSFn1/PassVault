using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassVault
{
    public partial class Email : Form
    {
        private string verificationCode;
        private const string FirebaseUrl = "https://passvault-eccee-default-rtdb.firebaseio.com";

        public Email()
        {
            InitializeComponent();
        }

        //Retrieve all the data
        private class UserRecord
        {
            public string username { get; set; }
            public string Email { get; set; }
            public string Salt { get; set; }
            public string Hash { get; set; }
            public int Iterations { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        private async Task<bool> EmailExistsInCloudAsync(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Use capital "Users" if that's how you're storing them (Firebase is case-sensitive)
                    HttpResponseMessage response = await client.GetAsync($"{FirebaseUrl}/Users.json");
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Failed to connect to Firebase ({response.StatusCode}).");
                        return false;
                    }

                    string jsonString = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(jsonString) || jsonString == "null")
                        return false;

                    using (JsonDocument doc = JsonDocument.Parse(jsonString))
                    {
                        foreach (JsonProperty userProperty in doc.RootElement.EnumerateObject())
                        {
                            try
                            {
                                JsonElement val = userProperty.Value;

                                
                                //Value is an object and has Data property -> Data contains Base64 encrypted JSON
                                if (val.ValueKind == JsonValueKind.Object && val.TryGetProperty("Data", out JsonElement dataElem) && dataElem.ValueKind == JsonValueKind.String)
                                {
                                    string encryptedBase64 = dataElem.GetString();
                                    if (string.IsNullOrEmpty(encryptedBase64)) continue;

                                    byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
                                    string decryptedJson = DecryptData(encryptedBytes);
                                    var user = JsonSerializer.Deserialize<UserRecord>(decryptedJson);
                                    if (user != null && !string.IsNullOrEmpty(user.Email) &&
                                        user.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                                        return true;
                                }
                            }
                            catch
                            {
                                // ignore malformed entries and continue scanning
                                continue;
                            }
                        }
                    }

                    return false; // not found
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking Firebase: {ex.Message}");
                    return false;
                }
            }
        }

        //Decrypt the data
        private static string DecryptData(byte[] encryptedData)
        {
            byte[] decrypted = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }

        //Check if email exists in the file
        private bool EmailExists(string email)
        {
            //Set the path for the file
            string path = Path.Combine(Application.StartupPath, "info.txt");

            // Make sure the path exists
            if (!File.Exists(path))
                return false;

            //Read all lines into an array
            string[] lines = File.ReadAllLines(path);

            //Go through each line and decrypt each line
            foreach (string line in lines)
            {
                try
                {
                    byte[] encryptedBytes = Convert.FromBase64String(line);
                    string json = DecryptData(encryptedBytes);
                    var user = JsonSerializer.Deserialize<UserRecord>(json);

                    //If the emails match, return
                    if (user.Email == email)
                        return true;
                }

                catch
                {
                    continue; 
                }
            }
            return false; 
        }

        //Generate a random 6 digit code
        private string GenerateCode()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
                int value = Math.Abs(BitConverter.ToInt32(bytes, 0)) % 1000000;
                return value.ToString("D6");
            }
        }

        //Create a method to send the email
        private void SendEmail(string recipientEmail, string code)
        {
            try
            {
                //Store the username and password of the gmail account
                string senderEmail = "passvaultms@gmail.com";
                string senderPassword = "ftky iwmz qabd wcml";

                //Send the actualy email
                MailMessage message = new MailMessage
                {
                    From = new MailAddress(senderEmail, "PassVault"),
                    Subject = "Your PassVault Code",
                    Body = $"Your password reset verification code is: {code}\n\nIf you did not request this, please ignore this email."
                };
                message.To.Add(recipientEmail);

                //Set up the port
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }

                MessageBox.Show("Verification code sent to your email.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Go back to the login page
            Login newForm = new Login();
            newForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter your email");
                return;
            }

            // 🔹 Check Firebase instead of local file
            bool exists = await EmailExistsInCloudAsync(email);
            if (!exists)
            {
                MessageBox.Show("This email is not registered in the system.");
                return;
            }

            verificationCode = GenerateCode();
            SendEmail(email, verificationCode);

            // Move to verification page
            Varify verifyForm = new Varify(email, verificationCode);
            verifyForm.Show();
            this.Hide();

        }
    }
}
