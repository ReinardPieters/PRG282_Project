using PRG282Project.BusinessProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using PRG282Project.Presentation;
using System.Threading;
namespace PRG282Project
{
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
            txtUsername.TextChanged += TextBoxes_TextChanged;

            txtPassword.TextChanged += TextBoxes_TextChanged;

            void TextBoxes_TextChanged(object sender, EventArgs e)
            {
              
                btnLogin.Enabled = !string.IsNullOrWhiteSpace(txtPassword.Text) && !string.IsNullOrWhiteSpace(txtUsername.Text);
                txtPassword.PasswordChar = '*';
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text;
            string enteredPassword = txtPassword.Text;

            UserManager userManager = new UserManager();

            bool isValidUser = userManager.ValidateUser(enteredUsername, enteredPassword);

            if (isValidUser)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMain main = new frmMain();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            UserManager userManager = new UserManager();

            try
            {
                userManager.AddUser(username, password);
                MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Good bye","See you soon :)");
            Application.Exit();
        }
    }
}
