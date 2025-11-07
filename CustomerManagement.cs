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
    public partial class CustomerManagement : UserControl
    {
        private CustomerRepository _repository;
        private int _selectedCustomerId = 0;

        public CustomerManagement()
        {
            InitializeComponent();
            _repository = new CustomerRepository();
            LoadCustomers();
            ClearForm();
            this.Resize += CustomerManagement_Resize;
            CenterContent();
        }

        private void CustomerManagement_Resize(object sender, EventArgs e)
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
            dgvCustomers.Location = new Point(centerX, dgvCustomers.Location.Y);
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _repository.GetAll();
                dgvCustomers.DataSource = customers.ToList();
                dgvCustomers.Columns["CustomerID"].HeaderText = "ID";
                dgvCustomers.Columns["FirstName"].HeaderText = "First Name";
                dgvCustomers.Columns["LastName"].HeaderText = "Last Name";
                dgvCustomers.Columns["Email"].HeaderText = "Email";
                dgvCustomers.Columns["Phone"].HeaderText = "Phone";
                dgvCustomers.Columns["Address"].HeaderText = "Address";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            _selectedCustomerId = 0;
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
                var customer = new Customer
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                _repository.Add(customer);
                MessageBox.Show("Customer added successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to update.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateInput()) return;

            try
            {
                var customer = new Customer
                {
                    CustomerID = _selectedCustomerId,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                _repository.Update(customer);
                MessageBox.Show("Customer updated successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this customer?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _repository.Delete(_selectedCustomerId);
                    MessageBox.Show("Customer deleted successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting customer: " + ex.Message, "Error", 
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
            LoadCustomers();
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                _selectedCustomerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";
                txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";

                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
    }
}

