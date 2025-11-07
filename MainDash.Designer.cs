
namespace WareHouseApp
{
    partial class MainDash
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.topCardsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMaterials = new System.Windows.Forms.Panel();
            this.panelCustomers = new System.Windows.Forms.Panel();
            this.panelEmployees = new System.Windows.Forms.Panel();
            this.middleCardsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.panelInventoryValue = new System.Windows.Forms.Panel();
            this.panelLowStock = new System.Windows.Forms.Panel();
            this.panelRecentActivity = new System.Windows.Forms.Panel();
            this.mainTableLayout.SuspendLayout();
            this.topCardsLayout.SuspendLayout();
            this.middleCardsLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.AutoScroll = true;
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Controls.Add(this.lblTitle, 0, 0);
            this.mainTableLayout.Controls.Add(this.topCardsLayout, 0, 1);
            this.mainTableLayout.Controls.Add(this.middleCardsLayout, 0, 2);
            this.mainTableLayout.Controls.Add(this.panelRecentActivity, 0, 3);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.Padding = new System.Windows.Forms.Padding(20);
            this.mainTableLayout.RowCount = 4;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.mainTableLayout.Size = new System.Drawing.Size(820, 700);
            this.mainTableLayout.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTitle.Location = new System.Drawing.Point(23, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(774, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 Dashboard";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // topCardsLayout
            // 
            this.topCardsLayout.Controls.Add(this.panelMaterials);
            this.topCardsLayout.Controls.Add(this.panelCustomers);
            this.topCardsLayout.Controls.Add(this.panelEmployees);
            this.topCardsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topCardsLayout.Location = new System.Drawing.Point(23, 93);
            this.topCardsLayout.Name = "topCardsLayout";
            this.topCardsLayout.Padding = new System.Windows.Forms.Padding(5);
            this.topCardsLayout.Size = new System.Drawing.Size(774, 169);
            this.topCardsLayout.TabIndex = 1;
            // 
            // panelMaterials
            // 
            this.panelMaterials.BackColor = System.Drawing.Color.White;
            this.panelMaterials.Location = new System.Drawing.Point(8, 8);
            this.panelMaterials.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.panelMaterials.Name = "panelMaterials";
            this.panelMaterials.Size = new System.Drawing.Size(240, 155);
            this.panelMaterials.TabIndex = 0;
            // 
            // panelCustomers
            // 
            this.panelCustomers.BackColor = System.Drawing.Color.White;
            this.panelCustomers.Location = new System.Drawing.Point(263, 8);
            this.panelCustomers.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.panelCustomers.Name = "panelCustomers";
            this.panelCustomers.Size = new System.Drawing.Size(240, 155);
            this.panelCustomers.TabIndex = 1;
            // 
            // panelEmployees
            // 
            this.panelEmployees.BackColor = System.Drawing.Color.White;
            this.panelEmployees.Location = new System.Drawing.Point(518, 8);
            this.panelEmployees.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.panelEmployees.Name = "panelEmployees";
            this.panelEmployees.Size = new System.Drawing.Size(240, 155);
            this.panelEmployees.TabIndex = 2;
            // 
            // middleCardsLayout
            // 
            this.middleCardsLayout.Controls.Add(this.panelInventoryValue);
            this.middleCardsLayout.Controls.Add(this.panelLowStock);
            this.middleCardsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.middleCardsLayout.Location = new System.Drawing.Point(23, 268);
            this.middleCardsLayout.Name = "middleCardsLayout";
            this.middleCardsLayout.Padding = new System.Windows.Forms.Padding(5);
            this.middleCardsLayout.Size = new System.Drawing.Size(774, 294);
            this.middleCardsLayout.TabIndex = 2;
            // 
            // panelInventoryValue
            // 
            this.panelInventoryValue.BackColor = System.Drawing.Color.White;
            this.panelInventoryValue.Location = new System.Drawing.Point(8, 8);
            this.panelInventoryValue.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.panelInventoryValue.Name = "panelInventoryValue";
            this.panelInventoryValue.Size = new System.Drawing.Size(370, 280);
            this.panelInventoryValue.TabIndex = 0;
            // 
            // panelLowStock
            // 
            this.panelLowStock.BackColor = System.Drawing.Color.White;
            this.panelLowStock.Location = new System.Drawing.Point(393, 8);
            this.panelLowStock.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.panelLowStock.Name = "panelLowStock";
            this.panelLowStock.Size = new System.Drawing.Size(370, 280);
            this.panelLowStock.TabIndex = 1;
            // 
            // panelRecentActivity
            // 
            this.panelRecentActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRecentActivity.BackColor = System.Drawing.Color.White;
            this.panelRecentActivity.Location = new System.Drawing.Point(23, 568);
            this.panelRecentActivity.MinimumSize = new System.Drawing.Size(0, 220);
            this.panelRecentActivity.Name = "panelRecentActivity";
            this.panelRecentActivity.Size = new System.Drawing.Size(774, 260);
            this.panelRecentActivity.TabIndex = 3;
            // 
            // MainDash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.mainTableLayout);
            this.Name = "MainDash";
            this.Size = new System.Drawing.Size(820, 700);
            this.mainTableLayout.ResumeLayout(false);
            this.mainTableLayout.PerformLayout();
            this.topCardsLayout.ResumeLayout(false);
            this.middleCardsLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel topCardsLayout;
        private System.Windows.Forms.Panel panelMaterials;
        private System.Windows.Forms.Panel panelCustomers;
        private System.Windows.Forms.Panel panelEmployees;
        private System.Windows.Forms.FlowLayoutPanel middleCardsLayout;
        private System.Windows.Forms.Panel panelInventoryValue;
        private System.Windows.Forms.Panel panelLowStock;
        private System.Windows.Forms.Panel panelRecentActivity;
    }
}
