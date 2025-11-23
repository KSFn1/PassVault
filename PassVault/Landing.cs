using Microsoft.VisualBasic; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PassVault
{
    public partial class Landing : Form
    {
        // Model class
        public class Credential
        {
            public string Platform { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Website { get; set; }
        }

        // Declare the fields
        private string currentUser;
        private List<Credential> credentials = new List<Credential>();

        // Create the constructor
        public Landing(string username)
        {
            InitializeComponent();
            //Carry the username over
            currentUser = username;
            //Make the window full screen
            this.WindowState = FormWindowState.Maximized;

            label5.Text = $"WELCOME, {currentUser.ToUpper()}";
            //Load the data
            Load += async (s, e) =>
            {
                await LoadCredentials();
                RefreshTable();
                SetupGrid();
            };
            //Make the grid match the available width
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //Setup the grid 
        private void SetupGrid()
        {
            // Fully readonly
            dataGridView1.ReadOnly = true;

            // Cannot add/delete
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            // Cannot resize
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            // No multi-select
            dataGridView1.MultiSelect = false;

            // Row-select only
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Removes blue highlight when clicking
            dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;

            // Prevent selection display
            dataGridView1.ClearSelection();

            // Prevent editing via keyboard
            dataGridView1.Enabled = true; // users can still select rows for Modify/Delete
        }


        // Load credentials from cloud
        private async Task LoadCredentials()
        {

            //Read information from the databass
            credentials.Clear();
            var encryptedLines = await FirebaseHelper.LoadUserCredentialsAsync(currentUser);
            foreach (var line in encryptedLines)
            {
                //decrypt information
                string decryptedLine = DecryptString(line);
                if (string.IsNullOrEmpty(decryptedLine)) continue;

                string[] parts = decryptedLine.Split('|');
                if (parts.Length == 5)
                {
                    //Save information into variables
                    credentials.Add(new Credential
                    {
                        Platform = parts[0],
                        Username = parts[1],
                        Password = parts[2],
                        Email = parts[3],
                        Website = parts[4]
                    });
                }
            }
            //Refresh the table
            RefreshTable();
        }


        // Encrypt text using Windows DPAPI (CurrentUser scope)
        private static string EncryptString(string plainText)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        // Decrypt text
        private static string DecryptString(string encryptedText)
        {
            try
            {
                byte[] data = Convert.FromBase64String(encryptedText);
                byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decrypted);
            }
            catch
            {
                return null; // ignore invalid lines
            }
        }


        // Save credentials to cloud
        private async void SaveCredentials()
        {
            var lines = credentials.Select(c =>
                EncryptString($"{c.Platform}|{c.Username}|{c.Password}|{c.Email}|{c.Website}")
            ).ToList();

            await FirebaseHelper.SaveUserCredentialsAsync(currentUser, lines);
        }

        // Refresh the grid
        private void RefreshTable()
        {
            var sortedList = credentials.OrderBy(c => c.Platform).ToList();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new BindingList<Credential>(sortedList);
        }

        // Add button
        private async void buttonAdd_Click_1(object sender, EventArgs e)
        {
            //Save each input into a variable
            string platform = Interaction.InputBox("Enter Platform:", "Add Credential");
            string username = Interaction.InputBox("Enter Username:", "Add Credential");
            string password = Interaction.InputBox("Enter Password:", "Add Credential");
            string email = Interaction.InputBox("Enter Email:", "Add Credential");
            string website = Interaction.InputBox("Enter Website:", "Add Credential");

            //Make sure the textfield is not empty
            if (!string.IsNullOrWhiteSpace(platform))
            {
                credentials.Add(new Credential
                {
                    //Read the information
                    Platform = platform,
                    Username = username,
                    Password = password,
                    Email = email,
                    Website = website
                });

                //Save to databass
                await SaveCredentialsAsync();

                //Refresh
                RefreshTable();
            }
        }

        // Modify button
        private async void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                //Read all information and edit accordignly
                var cred = (Credential)dataGridView1.CurrentRow.DataBoundItem;
                cred.Platform = Interaction.InputBox("Edit Platform:", "Modify", cred.Platform);
                cred.Username = Interaction.InputBox("Edit Username:", "Modify", cred.Username);
                cred.Password = Interaction.InputBox("Edit Password:", "Modify", cred.Password);
                cred.Email = Interaction.InputBox("Edit Email:", "Modify", cred.Email);
                cred.Website = Interaction.InputBox("Edit Website:", "Modify", cred.Website);

                //Save to databass
                await SaveCredentialsAsync();
                //Refresh
                RefreshTable();
            }
        }

        // Delete button
        private async void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                //Index the element
                var cred = (Credential)dataGridView1.CurrentRow.DataBoundItem;
                //Confirm
                if (MessageBox.Show($"Delete {cred.Platform}?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Delete
                    credentials.Remove(cred);
                    //Update databass
                    await SaveCredentialsAsync();
                    //Refresh
                    RefreshTable();
                }
            }
        }

        //Helper method to update database
        private Task SaveCredentialsAsync() => FirebaseHelper.SaveUserCredentialsAsync(currentUser, credentials.Select(c =>
            EncryptString($"{c.Platform}|{c.Username}|{c.Password}|{c.Email}|{c.Website}")
        ).ToList());

        // Contact Us button
        private void button1_Click(object sender, EventArgs e)
        {
            Contact newForm = new Contact(currentUser);
            newForm.Show();
            this.Hide();
        }

        // About us button
        private void button2_Click(object sender, EventArgs e)
        {
            About newForm = new About(currentUser);
            newForm.Show();
            this.Hide();
        }

        // Logout button
        private void button3_Click(object sender, EventArgs e)
        {
            //Ask the user if they want to log out
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //Reset the username
                currentUser = null;
                //Close the application
                Application.Exit();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Landing_Load_1(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                RefreshTable();
                return;
            }

            searchTerm = textBox1.Text.Trim().ToLower(); // get input and ignore case

            // Filter credentials where any relevant field contains the search term
            var filteredList = credentials
                .Where(c =>
                    c.Platform.ToLower().Contains(searchTerm) ||
                    c.Username.ToLower().Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm) ||
                    c.Website.ToLower().Contains(searchTerm)
                )
                .OrderBy(c => c.Platform) // optional: keep alphabetical order
                .ToList();

            // Update DataGridView with filtered list
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new BindingList<Credential>(filteredList);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

