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
        private bool ValidateLogin(String usernameInput, string password)
        {

            //Set the file path
            string path = Path.Combine(Application.StartupPath, "info.txt");

            //Make sure the file exists 
            if (!File.Exists(path))
            {
                return false;
            }

            //Store all data in a line
            string[] lines = File.ReadAllLines(path);

            //Go through each line and decrypt
            foreach (var line in lines)
            {
                try
                {
                    byte[] encryptedBytes = Convert.FromBase64String(line);
                    string json = DecryptData(encryptedBytes);

                    var user = JsonSerializer.Deserialize<UserRecord>(json);

                    //If the username is found, return the encrypted information
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
            //Read the username
            string inputUsername = textBox1.Text.Trim();
            //Read the password
            string password = textBox2.Text;

            //Make sure neither fields are empty
            if (string.IsNullOrWhiteSpace(inputUsername) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fillout both fields");
                return;
            }

            //If username and password match, proceed
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
    }
}
