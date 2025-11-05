using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WareHouseApp.Data;

namespace WareHouseApp
{
    public partial class SignUpForm : Form
    {
        private DataAccess _data;

        public SignUpForm()
        {
            InitializeComponent();
            _data = new DataAccess();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Check if username already exists
                string checkSql = "SELECT COUNT(*) FROM Users WHERE UserName = @username";
                var result = _data.ExecuteScalar(checkSql, new SqlParameter("@username", txtUsername.Text));
                
                if (Convert.ToInt32(result) > 0)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert new user (default role: Admin)
                string insertSql = "INSERT INTO Users (UserName, [Password], [Role]) VALUES (@username, @password, 'Admin')";
                int rowsAffected = _data.ExecuteNonQuery(insertSql,
                    new SqlParameter("@username", txtUsername.Text),
                    new SqlParameter("@password", txtPassword.Text));

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Account created successfully! You can now login.", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating account: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}

