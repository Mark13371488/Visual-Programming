
namespace WinFormsApp1
{
    public partial class Login : Form
    {

        private const string adminLogin = "admin";
        private const string adminPassword = "admin";

        public Login()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (t_password.Text.ToString().Equals(adminPassword) && t_login.Text.ToString().Equals(adminLogin))
            {
                Form1 menu = new Form1(Role.Admin);
                this.Hide();
                menu.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter correct login and password");
                t_password.Text = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 menu = new Form1(Role.User);
            this.Hide();
            menu.ShowDialog();
            this.Close();
        }
    }
}
