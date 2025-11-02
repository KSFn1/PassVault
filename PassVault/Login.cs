namespace PassVault
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            Email newForm = new Email(); 
            newForm.Show();             
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register newForm = new Register();
            newForm.Show();
            this.Hide(); 
        }
    }
}
