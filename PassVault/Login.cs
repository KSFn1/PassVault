using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;


namespace PassVault
{
    public partial class Login : Form
    {
        private const string ImagePath = @"F:\PassVault\PassVault\PassVault\images";
        public Login()
        {
            InitializeComponent();
        }

        // Match the structure of saved user data
        private class UserRecord
        {
            public string username { get; set; }
            public string Email { get; set; }
            public string Salt { get; set; }
            public string Hash { get; set; }
            public int Iterations { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        // Hash a password with salt & irretations
        private static byte[] HashPassword(string password, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        // Decrypt the data
        private static string DecryptData(byte[] encryptedData)
        {
            byte[] decrypted = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }

        // Validate login info
        private bool ValidateLogin(String usernameInput, string password)
        {
            string path = Path.Combine(Application.StartupPath, "info.txt");

            if (!File.Exists(path))
            {
                return false;
            }

            string[] lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                try
                {
                    byte[] encryptedBytes = Convert.FromBase64String(line);
                    string json = DecryptData(encryptedBytes);

                    var user = JsonSerializer.Deserialize<UserRecord>(json);

                    if (user.username == usernameInput)
                    {
                        byte[] salt = Convert.FromBase64String(user.Salt);
                        byte[] hash = HashPassword(password, salt, user.Iterations);

                        string hashBase64 = Convert.ToBase64String(hash);

                        return hashBase64 == user.Hash;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputUsername = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(inputUsername) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fillout both fields");
                return;
            }

            if (ValidateLogin(inputUsername, password))
            {

                //Switch to the landing page
                Landing newForm = new Landing();
                newForm.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Email newForm = new Email();
            newForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register newForm = new Register();
            newForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
            }
            else
            {
                textBox2.PasswordChar = '*'; 

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
            }



        }
    }
}
