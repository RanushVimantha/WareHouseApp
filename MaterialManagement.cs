using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHouseApp.Materials;
using WareHouseApp.Repositories;

namespace WareHouseApp
{
    public partial class MaterialManagement : UserControl
    {
        private MaterialRepository _repository;
        private int _selectedMaterialId = 0;

        public MaterialManagement()
        {
            InitializeComponent();
            _repository = new MaterialRepository();
            LoadMaterials();
            ClearForm();
        }

        private void LoadMaterials()
        {
            try
            {
                var materials = _repository.GetAll();
                dgvMaterials.DataSource = materials.ToList();
                dgvMaterials.Columns["MaterialID"].HeaderText = "ID";
                dgvMaterials.Columns["MaterialName"].HeaderText = "Name";
                dgvMaterials.Columns["Quantity"].HeaderText = "Quantity";
                dgvMaterials.Columns["Price"].HeaderText = "Price";
                dgvMaterials.Columns["Description"].HeaderText = "Description";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading materials: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtMaterialName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            _selectedMaterialId = 0;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaterialName.Text))
            {
                MessageBox.Show("Please enter material name.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out int qty) || qty < 0)
            {
                MessageBox.Show("Please enter a valid quantity (positive number).", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text) || !decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price (positive number).", "Validation Error", 
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
                var material = new Material
                {
                    MaterialName = txtMaterialName.Text.Trim(),
                    Quantity = int.Parse(txtQuantity.Text),
                    Price = decimal.Parse(txtPrice.Text),
                    Description = txtDescription.Text.Trim()
                };

                _repository.Add(material);
                MessageBox.Show("Material added successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMaterials();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding material: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedMaterialId == 0)
            {
                MessageBox.Show("Please select a material to update.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateInput()) return;

            try
            {
                var material = new Material
                {
                    MaterialID = _selectedMaterialId,
                    MaterialName = txtMaterialName.Text.Trim(),
                    Quantity = int.Parse(txtQuantity.Text),
                    Price = decimal.Parse(txtPrice.Text),
                    Description = txtDescription.Text.Trim()
                };

                _repository.Update(material);
                MessageBox.Show("Material updated successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMaterials();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating material: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedMaterialId == 0)
            {
                MessageBox.Show("Please select a material to delete.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this material?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _repository.Delete(_selectedMaterialId);
                    MessageBox.Show("Material deleted successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMaterials();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting material: " + ex.Message, "Error", 
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
            LoadMaterials();
        }

        private void dgvMaterials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMaterials.Rows[e.RowIndex];
                _selectedMaterialId = Convert.ToInt32(row.Cells["MaterialID"].Value);
                txtMaterialName.Text = row.Cells["MaterialName"].Value.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value?.ToString() ?? "";

                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
    }
}

