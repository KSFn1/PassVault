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

            //Set the email and generated code
            userEmail = email;
            correctCode = code;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Go back to the login page
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Read the inputted code
            string enteredCode = textBox1.Text.Trim();

            //Make sure the field is not empty
            if (string.IsNullOrWhiteSpace(enteredCode))
            {
                MessageBox.Show("Please enter the verification code");
                return;
            }

            //If inputted code matches, proceed
            if (enteredCode == correctCode)
            {
                // Reset the code
                correctCode = null; 

                //Open the ResetPass page
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
            userEmail = null;
            correctCode = null;

            Login newForm = new Login();
            newForm.Show();
            this.Hide();
        }
    }
}
