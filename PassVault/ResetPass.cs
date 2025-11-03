using System;
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
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PassVault
{
    public partial class ResetPass : Form
    {
        //Set the path for the image 
        private readonly string ImagePath = Path.Combine(Application.StartupPath, "images");
        private string userEmail;
        public ResetPass(string email)
        {
            InitializeComponent();
            //Set the email
            userEmail = email;
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

        private void button2_Click(object sender, EventArgs e)
        {
            //Read the passwords
            string newPassword = textBox1.Text.Trim();
            string confirmPassword = textBox2.Text.Trim();

            //Make sure noth fields are filled out
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in both password fields.");
                return;
            }

            //Make sure the password matches the criteria
            if (!IsStrongPassword(newPassword))
            {
                MessageBox.Show("Password must include one upper case, one lower case, one symbol, and one number, and is 8 letters long.");
                return;
            }


            //Make sure both passwords match
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords don't match.");
                return;
            }

            //Set the path for the file
            string path = Path.Combine(Application.StartupPath, "info.txt");

            //Make sure the file exists
            if (!File.Exists(path))
            {
                MessageBox.Show("No user data found.");
                return;
            }

            //Read all lines
            string[] lines = File.ReadAllLines(path);
            List<string> updatedLines = new List<string>();

            bool userFound = false;

            // Go through each line and decrypt
            foreach (string line in lines)
            {
                try
                {
                    byte[] encryptedBytes = Convert.FromBase64String(line);
                    string json = DecryptData(encryptedBytes);
                    var user = JsonSerializer.Deserialize<UserRecord>(json);

                    // Match email and update password
                    if (user.Email == userEmail)
                    {
                        userFound = true;

                        byte[] newSalt = GenerateSalt();
                        byte[] newHash = HashPassword(newPassword, newSalt, 100000);

                        user.Salt = Convert.ToBase64String(newSalt);
                        user.Hash = Convert.ToBase64String(newHash);
                        user.Iterations = 100000;

                        string updatedJson = JsonSerializer.Serialize(user);
                        byte[] encrypted = ProtectData(updatedJson);
                        string encryptedBase64 = Convert.ToBase64String(encrypted);

                        updatedLines.Add(encryptedBase64);
                    }
                    else
                    {
                        // Keep other user records unchanged
                        updatedLines.Add(line);
                    }
                }
                catch
                {
                    // Skip unreadable lines safely
                    updatedLines.Add(line);
                }
            }

            if (!userFound)
            {
                MessageBox.Show("User not found.");
                return;
            }

            // Overwrite the file with updated user list
            File.WriteAllLines(path, updatedLines);

            MessageBox.Show("Password reset successful! You can now log in with your new password.");

            // Return to login
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
