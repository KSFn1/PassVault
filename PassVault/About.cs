using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PassVault
{
    public partial class About : Form
    {
        //Declare fields
        private string currentUser;

        //Create constructor
        public About(string username)
        {
            InitializeComponent();
            //Set the window size to maximum
            this.WindowState = FormWindowState.Maximized;
            //Carry the username over
            currentUser = username;

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //Switch back to the landiwng page
            Landing newForm = new Landing(currentUser);
            newForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //switch to contact us page
            Contact newForm = new Contact(currentUser);
            newForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Ask if the user wants to quit
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //If yes, quit
            if (result == DialogResult.Yes)
            {
                //Reset the username
                currentUser = null;
                // Closes the whole app
                Application.Exit();
            }
            else
            {
                // Do nothing — user chose No
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
