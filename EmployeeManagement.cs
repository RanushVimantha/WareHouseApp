using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHouseApp.Entities;
using WareHouseApp.Repositories;

namespace WareHouseApp
{
    public partial class EmployeeManagement : UserControl
    {
        private EmployeeRepository _repository;
        private int _selectedEmployeeId = 0;

        public EmployeeManagement()
        {
            InitializeComponent();
            _repository = new EmployeeRepository();
            LoadEmployees();
            ClearForm();
            this.Resize += EmployeeManagement_Resize;
            CenterContent();
        }

        private void EmployeeManagement_Resize(object sender, EventArgs e)
        {
            CenterContent();
        }

        private void CenterContent()
        {
            // Calculate center position for all elements
            int centerX = (this.Width - panel1.Width) / 2;
            
            // Center the title
            lblTitle.Location = new Point(centerX, lblTitle.Location.Y);
            
            // Center the input panel
            panel1.Location = new Point(centerX, panel1.Location.Y);
            
            // Center the data grid
            dgvEmployees.Location = new Point(centerX, dgvEmployees.Location.Y);
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = _repository.GetAll();
                dgvEmployees.DataSource = employees.ToList();
                dgvEmployees.Columns["EmployeeID"].HeaderText = "ID";
                dgvEmployees.Columns["FirstName"].HeaderText = "First Name";
                dgvEmployees.Columns["LastName"].HeaderText = "Last Name";
                dgvEmployees.Columns["Role"].HeaderText = "Role";
                dgvEmployees.Columns["Email"].HeaderText = "Email";
                dgvEmployees.Columns["Phone"].HeaderText = "Phone";
                dgvEmployees.Columns["Address"].HeaderText = "Address";
                dgvEmployees.Columns["UserID"].HeaderText = "User ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtRole.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtUserID.Clear();
            _selectedEmployeeId = 0;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter first name.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter last name.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var employee = new Employee
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Role = txtRole.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    UserID = string.IsNullOrWhiteSpace(txtUserID.Text) ? 0 : int.Parse(txtUserID.Text)
                };

                _repository.Add(employee);
                MessageBox.Show("Employee added successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployees();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding employee: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedEmployeeId == 0)
            {
                MessageBox.Show("Please select an employee to update.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateInput()) return;

            try
            {
                var employee = new Employee
                {
                    EmployeeID = _selectedEmployeeId,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Role = txtRole.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    UserID = string.IsNullOrWhiteSpace(txtUserID.Text) ? 0 : int.Parse(txtUserID.Text)
                };

                _repository.Update(employee);
                MessageBox.Show("Employee updated successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployees();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating employee: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedEmployeeId == 0)
            {
                MessageBox.Show("Please select an employee to delete.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this employee?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _repository.Delete(_selectedEmployeeId);
                    MessageBox.Show("Employee deleted successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployees();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting employee: " + ex.Message, "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEmployees.Rows[e.RowIndex];
                _selectedEmployeeId = Convert.ToInt32(row.Cells["EmployeeID"].Value);
                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                txtRole.Text = row.Cells["Role"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";
                txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
                txtUserID.Text = row.Cells["UserID"].Value?.ToString() ?? "0";

                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
    }
}

