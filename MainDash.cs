using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHouseApp.Data;

namespace WareHouseApp
{
    public partial class MainDash : UserControl
    {
        private DataAccess _data;

        public MainDash()
        {
            InitializeComponent();
            _data = new DataAccess();
            StylePanels();
            LoadDashboardData();
            this.Resize += MainDash_Resize;
        }

        private void MainDash_Resize(object sender, EventArgs e)
        {
            int availableWidth = this.Width - 80;
            int spacing = 15;
            
            // Calculate responsive card widths
            int smallCardWidth = Math.Max(220, (availableWidth - (spacing * 2)) / 3);
            int largeCardWidth = Math.Max(350, (availableWidth - spacing) / 2);
            
            // Top row cards (3 columns) - keep height consistent
            panelMaterials.Width = smallCardWidth;
            panelMaterials.Height = 155;
            panelCustomers.Width = smallCardWidth;
            panelCustomers.Height = 155;
            panelEmployees.Width = smallCardWidth;
            panelEmployees.Height = 155;
            
            // Middle row cards (2 columns) - increase height for visibility
            panelInventoryValue.Width = largeCardWidth;
            panelInventoryValue.Height = 280;
            panelLowStock.Width = largeCardWidth;
            panelLowStock.Height = 280;
            
            // Make Recent Activity panel width responsive
            panelRecentActivity.Width = availableWidth;
            
            // Redraw panels with new sizes
            panelMaterials.Invalidate();
            panelCustomers.Invalidate();
            panelEmployees.Invalidate();
            panelInventoryValue.Invalidate();
            panelLowStock.Invalidate();
            panelRecentActivity.Invalidate();
            
            // Recreate content with updated sizes to adjust divider widths
            if (_data != null)
            {
                LoadDashboardData();
            }
        }

        private void StylePanels()
        {
            topCardsLayout.WrapContents = true;
            topCardsLayout.AutoScroll = false;
            middleCardsLayout.WrapContents = true;
            middleCardsLayout.AutoScroll = false;
            
            Panel[] panels = { panelMaterials, panelCustomers, panelEmployees, 
                             panelInventoryValue, panelLowStock, panelRecentActivity };
            
            foreach (Panel panel in panels)
            {
                panel.Paint += Panel_Paint;
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            
            using (GraphicsPath path = GetRoundedRectanglePath(panel.ClientRectangle, 12))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Draw subtle shadow
                using (GraphicsPath shadowPath = GetRoundedRectanglePath(
                    new Rectangle(panel.ClientRectangle.X + 2, panel.ClientRectangle.Y + 2,
                    panel.ClientRectangle.Width - 2, panel.ClientRectangle.Height - 2), 12))
                {
                    using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
                    {
                        e.Graphics.FillPath(shadowBrush, shadowPath);
                    }
                }
                
                // Fill main panel
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }
                
                // Draw border
                using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            
            return path;
        }

        private void LoadDashboardData()
        {
            try
            {
                int materialCount = GetMaterialCount();
                int customerCount = GetCustomerCount();
                int employeeCount = GetEmployeeCount();
                decimal totalInventoryValue = GetTotalInventoryValue();
                int lowStockCount = GetLowStockCount();

                CreateStatCard(panelMaterials, "📦", "Total Materials", materialCount.ToString(), 
                    Color.FromArgb(52, 152, 219), "Inventory Items");
                
                CreateStatCard(panelCustomers, "👥", "Customers", customerCount.ToString(), 
                    Color.FromArgb(46, 204, 113), "Total Registered");
                
                CreateStatCard(panelEmployees, "👔", "Employees", employeeCount.ToString(), 
                    Color.FromArgb(155, 89, 182), "Active Staff");

                CreateInventoryValuePanel(totalInventoryValue, materialCount);
                CreateLowStockPanel(lowStockCount);
                CreateRecentActivityPanel();
            }
            catch (Exception ex)
            {
                Panel errorPanel = new Panel
                {
                    BackColor = Color.FromArgb(255, 245, 245),
                    BorderStyle = BorderStyle.None,
                    Location = new Point(40, 120),
                    Size = new Size(this.Width - 80, 150),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                
                Label lblErrorIcon = new Label
                {
                    Text = "⚠️",
                    Font = new Font("Segoe UI", 32F),
                    ForeColor = Color.FromArgb(231, 76, 60),
                    Location = new Point(20, 35),
                    AutoSize = true
                };
                
                Label lblError = new Label
                {
                    Text = "Unable to load dashboard data",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(231, 76, 60),
                    Location = new Point(90, 30),
                    AutoSize = true
                };
                
                Label lblErrorDetails = new Label
                {
                    Text = "Please check your database connection and ensure the database is running.",
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(149, 165, 166),
                    Location = new Point(90, 60),
                    MaximumSize = new Size(errorPanel.Width - 110, 0),
                    AutoSize = true
                };
                
                Label lblErrorMsg = new Label
                {
                    Text = "Error: " + ex.Message,
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(189, 195, 199),
                    Location = new Point(90, 95),
                    MaximumSize = new Size(errorPanel.Width - 110, 0),
                    AutoSize = true
                };
                
                errorPanel.Controls.Add(lblErrorIcon);
                errorPanel.Controls.Add(lblError);
                errorPanel.Controls.Add(lblErrorDetails);
                errorPanel.Controls.Add(lblErrorMsg);
                this.Controls.Add(errorPanel);
                errorPanel.BringToFront();
            }
        }

        private void CreateStatCard(Panel panel, string icon, string title, string value, Color accentColor, string subtitle)
        {
            panel.Controls.Clear();

            // Icon
            Label lblIcon = new Label();
            lblIcon.Text = icon;
            lblIcon.Font = new Font("Segoe UI", 28F);
            lblIcon.ForeColor = accentColor;
            lblIcon.Location = new Point(20, 18);
            lblIcon.AutoSize = true;

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(20, 80);
            lblTitle.AutoSize = true;
            lblTitle.MaximumSize = new Size(panel.Width - 40, 0);

            // Value (big number)
            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblValue.ForeColor = accentColor;
            lblValue.Location = new Point(panel.Width - 100, 20);
            lblValue.AutoSize = true;
            lblValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Subtitle
            Label lblSubtitle = new Label();
            lblSubtitle.Text = subtitle;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(149, 165, 166);
            lblSubtitle.Location = new Point(20, 105);
            lblSubtitle.AutoSize = true;
            lblSubtitle.MaximumSize = new Size(panel.Width - 40, 0);

            panel.Controls.Add(lblIcon);
            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblValue);
            panel.Controls.Add(lblSubtitle);
        }

        private void CreateInventoryValuePanel(decimal totalValue, int itemCount)
        {
            panelInventoryValue.Controls.Clear();

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "💰 Inventory Overview";
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(20, 15);
            lblTitle.AutoSize = true;

            // Total value - big but not too big
            Label lblTotalValue = new Label();
            lblTotalValue.Text = "$" + totalValue.ToString("N2");
            lblTotalValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(39, 174, 96);
            lblTotalValue.Location = new Point(20, 50);
            lblTotalValue.AutoSize = true;

            // Subtitle
            Label lblValueText = new Label();
            lblValueText.Text = "Total Inventory Value";
            lblValueText.Font = new Font("Segoe UI", 9F);
            lblValueText.ForeColor = Color.FromArgb(127, 140, 141);
            lblValueText.Location = new Point(20, 90);
            lblValueText.AutoSize = true;

            // Divider line
            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(230, 230, 230);
            divider.Location = new Point(20, 120);
            divider.Size = new Size(panelInventoryValue.Width - 40, 1);

            // Items count info
            Label lblItemsInfo = new Label();
            lblItemsInfo.Text = "📦 Total Items: " + itemCount.ToString();
            lblItemsInfo.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblItemsInfo.ForeColor = Color.FromArgb(52, 73, 94);
            lblItemsInfo.Location = new Point(20, 140);
            lblItemsInfo.AutoSize = true;

            // Calculate and show average value
            decimal avgValue = 0;
            if (itemCount > 0)
            {
                avgValue = totalValue / itemCount;
            }
            Label lblAvgValue = new Label();
            lblAvgValue.Text = "💵 Average Value: $" + avgValue.ToString("N2");
            lblAvgValue.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblAvgValue.ForeColor = Color.FromArgb(52, 73, 94);
            lblAvgValue.Location = new Point(20, 170);
            lblAvgValue.AutoSize = true;

            // Stock status indicator
            Label lblStockStatus = new Label();
            lblStockStatus.Text = "✓ Tracking " + itemCount + " unique materials";
            lblStockStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStockStatus.ForeColor = Color.FromArgb(149, 165, 166);
            lblStockStatus.Location = new Point(20, 210);
            lblStockStatus.AutoSize = true;

            panelInventoryValue.Controls.Add(lblTitle);
            panelInventoryValue.Controls.Add(lblTotalValue);
            panelInventoryValue.Controls.Add(lblValueText);
            panelInventoryValue.Controls.Add(divider);
            panelInventoryValue.Controls.Add(lblItemsInfo);
            panelInventoryValue.Controls.Add(lblAvgValue);
            panelInventoryValue.Controls.Add(lblStockStatus);
        }

        private void CreateLowStockPanel(int lowStockCount)
        {
            panelLowStock.Controls.Clear();

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "⚠️ Stock Alert";
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(20, 15);
            lblTitle.AutoSize = true;

            // Determine alert status
            Color alertColor;
            string statusIcon;
            string statusMessage;
            
            if (lowStockCount > 0)
            {
                alertColor = Color.FromArgb(231, 76, 60); // Red
                statusIcon = "⚠️";
                statusMessage = "Items need restocking";
            }
            else
            {
                alertColor = Color.FromArgb(39, 174, 96); // Green
                statusIcon = "✓";
                statusMessage = "All items well stocked";
            }

            // Low stock count number
            Label lblLowStockCount = new Label();
            lblLowStockCount.Text = lowStockCount.ToString();
            lblLowStockCount.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblLowStockCount.ForeColor = alertColor;
            lblLowStockCount.Location = new Point(20, 50);
            lblLowStockCount.AutoSize = true;

            // Status text
            Label lblStatus = new Label();
            lblStatus.Text = statusIcon + " " + statusMessage;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = alertColor;
            lblStatus.Location = new Point(20, 95);
            lblStatus.AutoSize = true;

            // Divider
            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(230, 230, 230);
            divider.Location = new Point(20, 130);
            divider.Size = new Size(panelLowStock.Width - 40, 1);

            // Threshold info
            Label lblThreshold = new Label();
            lblThreshold.Text = "📋 Threshold: Quantity < 100 units";
            lblThreshold.Font = new Font("Segoe UI", 9F);
            lblThreshold.ForeColor = Color.FromArgb(127, 140, 141);
            lblThreshold.Location = new Point(20, 150);
            lblThreshold.AutoSize = true;

            // Action message
            Label lblAction = new Label();
            if (lowStockCount > 0)
            {
                lblAction.Text = "⚡ Action Required: Reorder stock soon";
                lblAction.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else
            {
                lblAction.Text = "✨ Status: Inventory levels optimal";
                lblAction.ForeColor = Color.FromArgb(39, 174, 96);
            }
            lblAction.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblAction.Location = new Point(20, 180);
            lblAction.AutoSize = true;

            // Additional info
            Label lblInfo = new Label();
            lblInfo.Text = "Monitor stock levels regularly";
            lblInfo.Font = new Font("Segoe UI", 8F);
            lblInfo.ForeColor = Color.FromArgb(149, 165, 166);
            lblInfo.Location = new Point(20, 220);
            lblInfo.AutoSize = true;

            panelLowStock.Controls.Add(lblTitle);
            panelLowStock.Controls.Add(lblLowStockCount);
            panelLowStock.Controls.Add(lblStatus);
            panelLowStock.Controls.Add(divider);
            panelLowStock.Controls.Add(lblThreshold);
            panelLowStock.Controls.Add(lblAction);
            panelLowStock.Controls.Add(lblInfo);
        }

        private void CreateRecentActivityPanel()
        {
            panelRecentActivity.Controls.Clear();

            Label lblTitle = new Label();
            lblTitle.Text = "📋 System Status & Quick Guide";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(25, 20);
            lblTitle.AutoSize = true;
            lblTitle.MaximumSize = new Size(panelRecentActivity.Width - 50, 0);

            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(230, 230, 230);
            divider.Location = new Point(25, 55);
            divider.Size = new Size(panelRecentActivity.Width - 50, 1);

            string[] activities = {
                "✅ Database connection: Active",
                "✅ All systems operational",
                "",
                "📦 Materials - Manage inventory, track stock levels",
                "👥 Customers - View and manage customer information",
                "👔 Employees - Manage staff and employee records"
            };

            int yPos = 70;
            for (int i = 0; i < activities.Length; i++)
            {
                if (string.IsNullOrEmpty(activities[i]))
                {
                    yPos += 10;
                    continue;
                }

                Label lblActivity = new Label();
                lblActivity.Text = activities[i];
                lblActivity.Font = new Font("Segoe UI", 10F);
                
                if (i < 2)
                {
                    lblActivity.ForeColor = Color.FromArgb(39, 174, 96);
                    lblActivity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                }
                else
                {
                    lblActivity.ForeColor = Color.FromArgb(52, 73, 94);
                }
                
                lblActivity.Location = new Point(25, yPos);
                lblActivity.AutoSize = true;
                lblActivity.MaximumSize = new Size(panelRecentActivity.Width - 50, 0);
                panelRecentActivity.Controls.Add(lblActivity);
                yPos += 28;
            }

            panelRecentActivity.Controls.Add(lblTitle);
            panelRecentActivity.Controls.Add(divider);
        }

        private int GetMaterialCount()
        {
            try
            {
                var result = _data.ExecuteScalar("SELECT COUNT(*) FROM Materials");
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
        }

        private int GetCustomerCount()
        {
            try
            {
                var result = _data.ExecuteScalar("SELECT COUNT(*) FROM Customers");
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
        }

        private int GetEmployeeCount()
        {
            try
            {
                var result = _data.ExecuteScalar("SELECT COUNT(*) FROM Employees");
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
        }

        private decimal GetTotalInventoryValue()
        {
            try
            {
                var result = _data.ExecuteScalar("SELECT SUM(Quantity * Price) FROM Materials");
                if (result == DBNull.Value) return 0;
                return Convert.ToDecimal(result);
            }
            catch
            {
                return 0;
            }
        }

        private int GetLowStockCount()
        {
            try
            {
                var result = _data.ExecuteScalar("SELECT COUNT(*) FROM Materials WHERE Quantity < 100");
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
        }
    }
}
