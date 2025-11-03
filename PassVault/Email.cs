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
        public Email()
        {
            InitializeComponent();
        }

        private class UserRecord
        {
            public string username { get; set; }
            public string Email { get; set; }
            public string Salt { get; set; }
            public string Hash { get; set; }
            public int Iterations { get; set; }
            public DateTime CreatedAt { get; set; }
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
            string path = Path.Combine(Application.StartupPath, "info.txt");
            if (!File.Exists(path))
                return false;

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                try
                {
                    byte[] encryptedBytes = Convert.FromBase64String(line);
                    string json = DecryptData(encryptedBytes);
                    var user = JsonSerializer.Deserialize<UserRecord>(json);

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

        private void SendEmail(string recipientEmail, string code)
        {
            try
            {
                string senderEmail = "passvaultms@gmail.com";
                string senderPassword = "jqww mebj igzf mnkw";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(senderEmail, "PassVault");
                message.To.Add(recipientEmail);
                message.Subject = "Your PassVault Code";
                message.Body = $"Your password reset verification code is: {code}\n\nIf you did not request this, please ignore this email.";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtp.EnableSsl = true;

                smtp.Send(message);
                MessageBox.Show("Verification code sent to your email.");

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login newForm = new Login();
            newForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim(); 

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter your email");
                return; 
            }

            if (!EmailExists(email))
            {
                MessageBox.Show("This email is not registered in the system");
                return;
            }

            verificationCode = GenerateCode();
            SendEmail(email, verificationCode);

            Varify newForm = new Varify(email, verificationCode);
            newForm.Show();
            this.Hide(); 
           
        }
    }
}
