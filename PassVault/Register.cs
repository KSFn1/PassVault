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
using System.Web;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PassVault
{
    public partial class Register : Form
    {
        private const string ImagePath = @"F:\PassVault\PassVault\PassVault\images";
        public Register()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

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

        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login newForm = new Login();
            newForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Store the information in variables
            string username = textBox1.Text.Trim();
            string email = textBox3.Text.Trim();
            string password = textBox2.Text;
            string conPass = textBox4.Text;


            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(conPass))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Make sure the passwords match
            if (password != conPass)
            {
                MessageBox.Show("The inputted passwords don't match");
                return;
            }

            // Make sure the email is in the right format
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format");
                return;
            }

            // Encrypte the information
            byte[] salt = GenerateSalt();
            byte[] hash = HashPassword(password, salt, 100000);

            string saltBase64 = Convert.ToBase64String(salt);
            string hashBase64 = Convert.ToBase64String(hash);

            // Create an object to store
            var userRecord = new
            {
                username = username,
                Email = email,
                Salt = saltBase64,
                Hash = hashBase64,
                Iterations = 100000,
                CreatedAt = DateTime.Now
            };

            // Serialize and encrypte before saving
            string json = JsonSerializer.Serialize(userRecord);

            //Encrypte with windows DPAPI
            byte[] encrypted = ProtectData(json);

            //Convert to Base64
            string encryptedBase64 = Convert.ToBase64String(encrypted);

            // Save to file
            string path = Path.Combine(Application.StartupPath, "info.txt");
            File.AppendAllText(path, encryptedBase64 + Environment.NewLine);

            MessageBox.Show("Registration complete!");
            ClearFields();

            Login newForm = new Login();
            newForm.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0';
                textBox4.PasswordChar = '\0';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
                button2.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
            }
            else
            {
                textBox2.PasswordChar = '*';
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
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0';
                textBox4.PasswordChar = '\0';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
                button2.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeclosed.png"));
            }
            else
            {
                textBox2.PasswordChar = '*';
                textBox4.PasswordChar = '*';

                button5.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
                button2.Image = Image.FromFile(Path.Combine(ImagePath, "passwordeyeopen.png"));
            }
        }
    }
}
