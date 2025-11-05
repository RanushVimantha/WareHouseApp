using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WareHouseApp
{
    public partial class DashBoard : Form
    {
        private string userName;
        private UserControl currentControl;

        public DashBoard(string user)
        {
            InitializeComponent();
            userName = user;
            ConfigureDashboard();
        }

        private void ConfigureDashboard()
        {
            button1.Text = "🏠 Home";
            button2.Text = "📦 Materials";
            button3.Text = "👥 Customers";
            button4.Text = "👔 Employees";
            button5.Text = "ℹ️ About";
            button6.Text = "🚪 Logout";

            button7.Text = "👤 " + userName;
            button8.Text = "⚙️ Settings";
            button9.Text = "📊 Dashboard";

            button1.Click += btnHome_Click;
            button2.Click += btnMaterials_Click;
            button3.Click += btnCustomers_Click;
            button4.Click += btnEmployees_Click;
            button6.Click += btnLogout_Click;
            button7.Click += btnProfile_Click;

            StyleMenuButtons();
            ShowMainDash();
        }

        private void StyleMenuButtons()
        {
            Button[] menuButtons = { button1, button2, button3, button4, button5, button6 };
            
            foreach (Button btn in menuButtons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.RoyalBlue;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(10, 0, 0, 0);
                btn.Cursor = Cursors.Hand;
                
                btn.MouseEnter += (s, e) => {
                    if (btn.Enabled)
                        btn.BackColor = Color.DodgerBlue;
                };
                btn.MouseLeave += (s, e) => {
                    btn.BackColor = Color.RoyalBlue;
                };
            }

            button6.BackColor = Color.Crimson;
            button6.MouseEnter += (s, e) => button6.BackColor = Color.Red;
            button6.MouseLeave += (s, e) => button6.BackColor = Color.Crimson;
        }

        private void ShowControl(UserControl control)
        {
            if (currentControl != null)
            {
                this.Controls.Remove(currentControl);
                currentControl.Dispose();
            }

            currentControl = control;
            currentControl.Dock = DockStyle.Fill;
            this.Controls.Add(currentControl);
            currentControl.BringToFront();
        }

        private void ShowMainDash()
        {
            mainDash1.Visible = true;
            mainDash1.BringToFront();
            if (currentControl != null && currentControl != mainDash1)
            {
                this.Controls.Remove(currentControl);
                currentControl.Dispose();
                currentControl = null;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ShowMainDash();
        }

        private void btnMaterials_Click(object sender, EventArgs e)
        {
            mainDash1.Visible = false;
            ShowControl(new MaterialManagement());
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            mainDash1.Visible = false;
            ShowControl(new CustomerManagement());
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            mainDash1.Visible = false;
            ShowControl(new EmployeeManagement());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                this.Hide();
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Logged in as: {userName}", "Profile", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
