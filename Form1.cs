using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHouseApp.People;
using WareHouseApp.Resources.AppStrings;

namespace WareHouseApp
{
    public partial class Form1 : Form
    {
        Admin admin = new Admin();
        LoginPage loginPage = new LoginPage();

        public Form1()
        {
            InitializeComponent();
            AddPlaceholders();
        }

        private void AddPlaceholders()
        {
            txtName.Text = "Username";
            txtName.ForeColor = Color.Gray;
            txtName.GotFocus += RemoveUsernamePlaceholder;
            txtName.LostFocus += AddUsernamePlaceholder;

            txtPassword.Text = "Password";
            txtPassword.ForeColor = Color.Gray;
            txtPassword.PasswordChar = '\0';
            txtPassword.GotFocus += RemovePasswordPlaceholder;
            txtPassword.LostFocus += AddPasswordPlaceholder;
        }

        private void RemoveUsernamePlaceholder(object sender, EventArgs e)
        {
            if (txtName.Text == "Username")
            {
                txtName.Text = "";
                txtName.ForeColor = Color.Black;
            }
        }

        private void AddUsernamePlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.Text = "Username";
                txtName.ForeColor = Color.Gray;
            }
        }

        private void RemovePasswordPlaceholder(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.PasswordChar = '*';
            }
        }

        private void AddPasswordPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.PasswordChar = '\0';
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            string userName = txtName.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(userName) || userName == "Username")
            {
                MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password) || password == "Password")
            {
                MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool isLoggedIn = admin.Login(userName, password);
                
                if (isLoggedIn)
                {
                    this.Hide();
                    DashBoard dashboard = new DashBoard(userName);
                    dashboard.FormClosed += (s, args) => this.Close();
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show(loginPage.LoginErrorMessageEn, loginPage.LoginErrorTitleEn, 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.PasswordChar = '\0';
                txtPassword.Text = "Password";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.PasswordChar = '\0';
                txtPassword.Text = "Password";
            }
        }

        private void linkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpForm signUpForm = new SignUpForm();
            signUpForm.ShowDialog();
        }
    }
}
