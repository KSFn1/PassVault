using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassVault
{
    public partial class Contact : Form
    {
        public Contact()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Ask if the user wants to quit
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //If yes, quit
            if (result == DialogResult.Yes)
            {
                // Closes the whole app
                Application.Exit();
            }
            else
            {
                // Do nothing — user chose No
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Switch back to the landiwng page
            Landing newForm = new Landing();
            newForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            //Store the information is variables
            string name = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();
            string message = textBox3.Text.Trim();

            //Make sure all text fields are filled out
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please fill in all fields before sending.");
                return;
            }

            try
            {
                //Save the message locally
                string path = Path.Combine(Application.StartupPath, "contact_messages.txt");
                string entry = $"Name: {name}\nEmail: {email}\nMessage: {message}\nDate: {DateTime.Now}\n---\n";
                File.AppendAllText(path, entry);


                //Send email to us
                SendEmailToUs(name, email, message);

                //Send confirmation to user
                SendUser(name, email);


                MessageBox.Show("Your message has been sent successfully. We’ll get back to you soon!");

                //Reset the inputted data
                textBox1.Text = textBox2.Text = textBox3.Text = null;

                //Return to the landing page
                Landing newForm = new Landing();
                newForm.Show();
                this.Hide();

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error sending message" + ex.Message);
            }

        }

        private void SendEmailToUs(string name, string userEmail, string message)
        {
            //Store information in variables
            string email = "passvaultms@gmail.com";
            string password = "ftky iwmz qabd wcml";

            //Send the email
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(email);
                mail.To.Add(email);
                mail.Subject = "New Contact Message From PassVault";
                mail.Body = $"Name: {name}\nEmail: {userEmail}\nMessage: {message}";

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(email, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        private void SendUser(string name, string userEmail)
        {
            //Store the variables
            string adminEmail = "passvaultms@gmail.com";
            string adminPassword = "ftky iwmz qabd wcml";

            //Send the email
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(adminEmail, "PassVault Support");
                mail.To.Add(userEmail);
                mail.Subject = "We received your message!";
                mail.Body = $"Hello {name},\n\nThank you for contacting PassVault. We’ve received your message and will get back to you shortly.\n\nBest regards,\nPassVault Team";

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(adminEmail, adminPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        private void Contact_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            About newForm = new About();
            newForm.Show();
            this.Hide();
        }
    }
}
