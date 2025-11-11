using Microsoft.VisualBasic;
using System;
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
    public partial class Landing : Form
    {
        public Landing()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Landing_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Ask if the user wants to quit
            DialogResult result = MessageBox.Show("Are you sure you want to log out?","Confirm Logout", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

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

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            About newForm = new About();
            newForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Contact newForm = new Contact();
            newForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
         
        }
    }
}
