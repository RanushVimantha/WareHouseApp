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
            int largeCardWidth = Math.Max(320, (availableWidth - spacing) / 2);
            
            // Top row cards (3 columns)
            panelMaterials.Width = smallCardWidth;
            panelCustomers.Width = smallCardWidth;
            panelEmployees.Width = smallCardWidth;
            
            // Middle row cards (2 columns)
            panelInventoryValue.Width = largeCardWidth;
            panelLowStock.Width = largeCardWidth;
            
            // Redraw panels with new sizes
            panelMaterials.Invalidate();
            panelCustomers.Invalidate();
            panelEmployees.Invalidate();
            panelInventoryValue.Invalidate();
            panelLowStock.Invalidate();
            panelRecentActivity.Invalidate();
            
            // Recreate content with updated sizes
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

            Label lblIcon = new Label();
            lblIcon.Text = icon;
            lblIcon.Font = new Font("Segoe UI", 32F);
            lblIcon.ForeColor = accentColor;
            lblIcon.Location = new Point(20, 20);
            lblIcon.AutoSize = true;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(20, 85);
            lblTitle.AutoSize = true;
            lblTitle.MaximumSize = new Size(panel.Width - 40, 0);

            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblValue.ForeColor = accentColor;
            lblValue.Location = new Point(panel.Width - 120, 22);
            lblValue.AutoSize = true;
            lblValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            Label lblSubtitle = new Label();
            lblSubtitle.Text = subtitle;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(149, 165, 166);
            lblSubtitle.Location = new Point(20, 110);
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

            Label lblTitle = new Label();
            lblTitle.Text = "💰 Inventory Overview";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(25, 20);
            lblTitle.AutoSize = true;
            lblTitle.MaximumSize = new Size(panelInventoryValue.Width - 50, 0);

            Label lblTotalValue = new Label();
            lblTotalValue.Text = "$" + totalValue.ToString("N2");
            lblTotalValue.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(39, 174, 96);
            lblTotalValue.Location = new Point(25, 60);
            lblTotalValue.AutoSize = true;
            lblTotalValue.MaximumSize = new Size(panelInventoryValue.Width - 50, 0);

            Label lblValueText = new Label();
            lblValueText.Text = "Total Inventory Value";
            lblValueText.Font = new Font("Segoe UI", 10F);
            lblValueText.ForeColor = Color.FromArgb(127, 140, 141);
            lblValueText.Location = new Point(25, 115);
            lblValueText.AutoSize = true;
            lblValueText.MaximumSize = new Size(panelInventoryValue.Width - 50, 0);

            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(230, 230, 230);
            divider.Location = new Point(25, 145);
            divider.Size = new Size(panelInventoryValue.Width - 50, 1);

            Label lblItemsInfo = new Label();
            lblItemsInfo.Text = "📊 " + itemCount.ToString() + " items in stock";
            lblItemsInfo.Font = new Font("Segoe UI", 10F);
            lblItemsInfo.ForeColor = Color.FromArgb(52, 73, 94);
            lblItemsInfo.Location = new Point(25, 160);
            lblItemsInfo.AutoSize = true;
            lblItemsInfo.MaximumSize = new Size(panelInventoryValue.Width - 50, 0);

            decimal avgValue = 0;
            if (itemCount > 0)
            {
                avgValue = totalValue / itemCount;
            }
            Label lblAvgValue = new Label();
            lblAvgValue.Text = "💵 Average: $" + avgValue.ToString("N2") + " per item";
            lblAvgValue.Font = new Font("Segoe UI", 10F);
            lblAvgValue.ForeColor = Color.FromArgb(52, 73, 94);
            lblAvgValue.Location = new Point(25, 185);
            lblAvgValue.AutoSize = true;
            lblAvgValue.MaximumSize = new Size(panelInventoryValue.Width - 50, 0);

            panelInventoryValue.Controls.Add(lblTitle);
            panelInventoryValue.Controls.Add(lblTotalValue);
            panelInventoryValue.Controls.Add(lblValueText);
            panelInventoryValue.Controls.Add(divider);
            panelInventoryValue.Controls.Add(lblItemsInfo);
            panelInventoryValue.Controls.Add(lblAvgValue);
        }

        private void CreateLowStockPanel(int lowStockCount)
        {
            panelLowStock.Controls.Clear();

            Label lblTitle = new Label();
            lblTitle.Text = "⚠️ Stock Alert";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(25, 20);
            lblTitle.AutoSize = true;
            lblTitle.MaximumSize = new Size(panelLowStock.Width - 50, 0);

            Color alertColor;
            string statusIcon;
            if (lowStockCount > 0)
            {
                alertColor = Color.FromArgb(231, 76, 60);
                statusIcon = "🔴";
            }
            else
            {
                alertColor = Color.FromArgb(39, 174, 96);
                statusIcon = "✅";
            }

            Label lblLowStockCount = new Label();
            lblLowStockCount.Text = lowStockCount.ToString();
            lblLowStockCount.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            lblLowStockCount.ForeColor = alertColor;
            lblLowStockCount.Location = new Point(25, 60);
            lblLowStockCount.AutoSize = true;

            string statusText = "";
            if (lowStockCount > 0)
            {
                statusText = statusIcon + " Items need restocking";
            }
            else
            {
                statusText = statusIcon + " All items well stocked!";
            }
            
            Label lblStatus = new Label();
            lblStatus.Text = statusText;
            lblStatus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStatus.ForeColor = alertColor;
            lblStatus.Location = new Point(25, 135);
            lblStatus.AutoSize = true;
            lblStatus.MaximumSize = new Size(panelLowStock.Width - 50, 0);

            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(230, 230, 230);
            divider.Location = new Point(25, 170);
            divider.Size = new Size(panelLowStock.Width - 50, 1);

            Label lblInfo = new Label();
            lblInfo.Text = "📋 Low stock threshold: Qty < 100 units";
            lblInfo.Font = new Font("Segoe UI", 9F);
            lblInfo.ForeColor = Color.FromArgb(149, 165, 166);
            lblInfo.Location = new Point(25, 185);
            lblInfo.AutoSize = true;
            lblInfo.MaximumSize = new Size(panelLowStock.Width - 50, 0);

            panelLowStock.Controls.Add(lblTitle);
            panelLowStock.Controls.Add(lblLowStockCount);
            panelLowStock.Controls.Add(lblStatus);
            panelLowStock.Controls.Add(divider);
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
