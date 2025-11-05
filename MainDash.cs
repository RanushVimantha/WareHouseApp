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
            int availableWidth = this.Width - 60;
            
            int cardWidth = Math.Max(200, Math.Min(240, (availableWidth - 40) / 3));
            panelMaterials.Width = cardWidth;
            panelCustomers.Width = cardWidth;
            panelEmployees.Width = cardWidth;
            
            int largeCardWidth = Math.Max(300, Math.Min(370, (availableWidth - 20) / 2));
            panelInventoryValue.Width = largeCardWidth;
            panelLowStock.Width = largeCardWidth;
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
            
            using (GraphicsPath path = GetRoundedRectanglePath(panel.ClientRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }
                
                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 1))
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
            catch (Exception)
            {
                Label lblError = new Label
                {
                    Text = "⚠️ Unable to load dashboard data.\nPlease check database connection.",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(231, 76, 60),
                    Location = new Point(250, 200),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lblError);
                lblError.BringToFront();
            }
        }

        private void CreateStatCard(Panel panel, string icon, string title, string value, Color accentColor, string subtitle)
        {
            panel.Controls.Clear();

            Label lblIcon = new Label();
            lblIcon.Text = icon;
            lblIcon.Font = new Font("Segoe UI", 30F);
            lblIcon.ForeColor = accentColor;
            lblIcon.Location = new Point(15, 20);
            lblIcon.AutoSize = true;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(15, 80);
            lblTitle.AutoSize = true;
            lblTitle.MaximumSize = new Size(panel.Width - 30, 0);

            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblValue.ForeColor = accentColor;
            lblValue.Location = new Point(105, 25);
            lblValue.AutoSize = true;

            Label lblSubtitle = new Label();
            lblSubtitle.Text = subtitle;
            lblSubtitle.Font = new Font("Segoe UI", 8F);
            lblSubtitle.ForeColor = Color.FromArgb(149, 165, 166);
            lblSubtitle.Location = new Point(15, 105);
            lblSubtitle.AutoSize = true;
            lblSubtitle.MaximumSize = new Size(panel.Width - 30, 0);

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
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(20, 15);
            lblTitle.AutoSize = true;
            lblTitle.MaximumSize = new Size(panelInventoryValue.Width - 40, 0);

            Label lblTotalValue = new Label();
            lblTotalValue.Text = "$" + totalValue.ToString("N2");
            lblTotalValue.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(39, 174, 96);
            lblTotalValue.Location = new Point(20, 50);
            lblTotalValue.AutoSize = true;
            lblTotalValue.MaximumSize = new Size(panelInventoryValue.Width - 40, 0);

            Label lblValueText = new Label();
            lblValueText.Text = "Total Inventory Value";
            lblValueText.Font = new Font("Segoe UI", 9F);
            lblValueText.ForeColor = Color.FromArgb(127, 140, 141);
            lblValueText.Location = new Point(20, 95);
            lblValueText.AutoSize = true;
            lblValueText.MaximumSize = new Size(panelInventoryValue.Width - 40, 0);

            Label lblItemsInfo = new Label();
            lblItemsInfo.Text = "📊 " + itemCount.ToString() + " items in stock";
            lblItemsInfo.Font = new Font("Segoe UI", 9F);
            lblItemsInfo.ForeColor = Color.FromArgb(52, 73, 94);
            lblItemsInfo.Location = new Point(20, 130);
            lblItemsInfo.AutoSize = true;
            lblItemsInfo.MaximumSize = new Size(panelInventoryValue.Width - 40, 0);

            decimal avgValue = 0;
            if (itemCount > 0)
            {
                avgValue = totalValue / itemCount;
            }
            Label lblAvgValue = new Label();
            lblAvgValue.Text = "💵 Avg: $" + avgValue.ToString("N2") + " per item";
            lblAvgValue.Font = new Font("Segoe UI", 9F);
            lblAvgValue.ForeColor = Color.FromArgb(52, 73, 94);
            lblAvgValue.Location = new Point(20, 155);
            lblAvgValue.AutoSize = true;
            lblAvgValue.MaximumSize = new Size(panelInventoryValue.Width - 40, 0);

            panelInventoryValue.Controls.Add(lblTitle);
            panelInventoryValue.Controls.Add(lblTotalValue);
            panelInventoryValue.Controls.Add(lblValueText);
            panelInventoryValue.Controls.Add(lblItemsInfo);
            panelInventoryValue.Controls.Add(lblAvgValue);
        }

        private void CreateLowStockPanel(int lowStockCount)
        {
            panelLowStock.Controls.Clear();

            Label lblTitle = new Label();
            lblTitle.Text = "⚠️ Stock Alert";
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(20, 15);
            lblTitle.AutoSize = true;
            lblTitle.MaximumSize = new Size(panelLowStock.Width - 40, 0);

            Color alertColor;
            if (lowStockCount > 0)
            {
                alertColor = Color.FromArgb(231, 76, 60);
            }
            else
            {
                alertColor = Color.FromArgb(39, 174, 96);
            }

            Label lblLowStockCount = new Label();
            lblLowStockCount.Text = lowStockCount.ToString();
            lblLowStockCount.Font = new Font("Segoe UI", 42F, FontStyle.Bold);
            lblLowStockCount.ForeColor = alertColor;
            lblLowStockCount.Location = new Point(20, 55);
            lblLowStockCount.AutoSize = true;

            string statusText = "";
            if (lowStockCount > 0)
            {
                statusText = "Items need restocking";
            }
            else
            {
                statusText = "All items stocked!";
            }
            
            Label lblStatus = new Label();
            lblStatus.Text = statusText;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = alertColor;
            lblStatus.Location = new Point(20, 120);
            lblStatus.AutoSize = true;
            lblStatus.MaximumSize = new Size(panelLowStock.Width - 40, 0);

            Label lblInfo = new Label();
            lblInfo.Text = "Items with qty < 100 units";
            lblInfo.Font = new Font("Segoe UI", 8F);
            lblInfo.ForeColor = Color.FromArgb(149, 165, 166);
            lblInfo.Location = new Point(20, 150);
            lblInfo.AutoSize = true;
            lblInfo.MaximumSize = new Size(panelLowStock.Width - 40, 0);

            panelLowStock.Controls.Add(lblTitle);
            panelLowStock.Controls.Add(lblLowStockCount);
            panelLowStock.Controls.Add(lblStatus);
            panelLowStock.Controls.Add(lblInfo);
        }

        private void CreateRecentActivityPanel()
        {
            panelRecentActivity.Controls.Clear();

            Label lblTitle = new Label();
            lblTitle.Text = "📋 System Status";
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(20, 15);
            lblTitle.AutoSize = true;
            lblTitle.MaximumSize = new Size(panelRecentActivity.Width - 40, 0);

            string[] activities = {
                "✅ Database connection: Active",
                "✅ All systems operational",
                "📦 Navigate to Materials to manage inventory",
                "👥 Navigate to Customers to view client list",
                "👔 Navigate to Employees to manage staff"
            };

            int yPos = 55;
            for (int i = 0; i < activities.Length; i++)
            {
                Label lblActivity = new Label();
                lblActivity.Text = activities[i];
                lblActivity.Font = new Font("Segoe UI", 9F);
                lblActivity.ForeColor = Color.FromArgb(52, 73, 94);
                lblActivity.Location = new Point(20, yPos);
                lblActivity.AutoSize = true;
                lblActivity.MaximumSize = new Size(panelRecentActivity.Width - 40, 0);
                panelRecentActivity.Controls.Add(lblActivity);
                yPos += 23;
            }

            panelRecentActivity.Controls.Add(lblTitle);
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
