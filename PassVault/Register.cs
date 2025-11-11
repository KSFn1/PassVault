using System;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace PassVault
{
    public partial class Register : Form
    {
        //Set the path for the image 
        private readonly string ImagePath = Path.Combine(Application.StartupPath, "images");
        public Register()
        {
            InitializeComponent();
        }

        //Obtain the user data
        private class UserRecord
        {
            public string username { get; set; }
            public string Email { get; set; }
            public string Salt { get; set;  }
            public string Hash { get; set;  }
            public int Iterations { get; set; }

            public DateTime CreatedAt { get; set; }
        }

        //Decrypt the data
        private static string DecryptData(byte[] encryptedData)
        {
            byte[] decrypted = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }

        // Make sure the inputted password matches the criteria
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

        //Encrypt the data
        private static byte[] GenerateSalt(int size = 16)
        {
            byte[] salt = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] HashPassword(string password, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        private static byte[] ProtectData(string plainText)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            return ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //Method to clear all fields once called
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Go back to the login page
            Login newForm = new Login();
            newForm.Show();
            this.Hide();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Store the information in variables
            string username = textBox2.Text.Trim();
            string email = textBox1.Text.Trim();
            string password = textBox3.Text;
            string conPass = textBox4.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(conPass))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password != conPass)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            if (!IsStrongPassword(password))
            {
                MessageBox.Show("Password must include one uppercase, one lowercase, one symbol, one number, and be at least 8 characters.");
                return;
            }

            // Check duplicates using Firebase
            var allUsers = await FirebaseHelper.GetAllUsersAsync();
            foreach (var kv in allUsers)
            {
                try
                {
                    string json = DecryptData(Convert.FromBase64String(kv.Value));
                    var user = JsonSerializer.Deserialize<UserRecord>(json);
                    if (user.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("This email is already registered.");
                        return;
                    }
                    if (user.username.Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("This username is already taken.");
                        return;
                    }
                }
                catch { continue; }
            }

            //Generate hash and salt
            byte[] salt = GenerateSalt();
            byte[] hash = HashPassword(password, salt, 100000);

            //Encrypt infromation
            var userRecord = new UserRecord
            {
                username = username,
                Email = email,
                Salt = Convert.ToBase64String(salt),
                Hash = Convert.ToBase64String(hash),
                Iterations = 100000,
                CreatedAt = DateTime.Now
            };

            string encryptedBase64 = Convert.ToBase64String(ProtectData(JsonSerializer.Serialize(userRecord)));

            // Save to Firebase
            await FirebaseHelper.SaveUserAsync(username, encryptedBase64);
            await FirebaseHelper.SaveUserCredentialsAsync(username, new System.Collections.Generic.List<string>());

            MessageBox.Show("Registration complete!");
            ClearFields();

            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            //Switch the image and settings depending on the situation
            if (textBox3.PasswordChar == '*')
            {
                textBox3.PasswordChar = '\0';
                textBox4.PasswordChar = '\0';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
                button2.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
            }
            else
            {
                textBox3.PasswordChar = '*';
                textBox4.PasswordChar = '*';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
                button2.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //Switch the image and settings depending on the situation
            if (textBox3.PasswordChar == '*')
            {
                textBox3.PasswordChar = '\0';
                textBox4.PasswordChar = '\0';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
                button2.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
            }
            else
            {
                textBox3.PasswordChar = '*';
                textBox4.PasswordChar = '*';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
                button2.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
            }
        }
    }
}
