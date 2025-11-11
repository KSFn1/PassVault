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
        //Set the path for the image 
        private readonly string ImagePath = Path.Combine(Application.StartupPath, "images");

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
        private async Task<bool> ValidateLogin(string usernameInput, string password)
        {

            //Read info from the databass
            var users = await FirebaseHelper.GetAllUsersAsync();

            foreach (var kv in users)
            {
                try
                {
                    //Decrypt the data
                    string json = DecryptData(Convert.FromBase64String(kv.Value));
                    var user = JsonSerializer.Deserialize<UserRecord>(json);
                    if (user.username == usernameInput)
                    {
                        byte[] salt = Convert.FromBase64String(user.Salt);
                        byte[] hash = HashPassword(password, salt, user.Iterations);
                        return Convert.ToBase64String(hash) == user.Hash;
                    }
                }
                catch { continue; }
            }
            return false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Store the username and password into variables
            string inputUsername = textBox1.Text.Trim();
            string password = textBox2.Text;

            //Make sure textfields are not empty
            if (string.IsNullOrWhiteSpace(inputUsername) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill out both fields.");
                return;
            }

            // Return to the login page
            if (await ValidateLogin(inputUsername, password))
            {
                Landing landingForm = new Landing(inputUsername);
                landingForm.Show();
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
            // Go to the email page
            Email newForm = new Email();
            newForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Go to the register page
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
            //If the button is pressed switch the image and settings depending on the situation
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
