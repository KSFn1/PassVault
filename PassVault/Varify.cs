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
    public partial class Varify : Form
    {
        private string correctCode;
        private string userEmail;
        public Varify(string email, string code)
        {
            InitializeComponent();
            userEmail = email;
            correctCode = code;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string enteredCode = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(enteredCode))
            {
                MessageBox.Show("Please enter the verification code");
                return;
            }

            if (enteredCode == correctCode)
            {

                correctCode = null; 
                ResetPass newForm = new ResetPass(userEmail);
                newForm.Show();
                this.Hide(); 

               
            }

            else
            {
                MessageBox.Show("Incorrect verification code was entered. Try again."); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
