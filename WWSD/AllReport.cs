using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWSD
{
    public partial class AllReport : UserControl
    {
        public AllReport()
        {
            InitializeComponent();
            LoadData();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                LoadData();
            }
        }


        private void LoadData()
        {
            string connectionString = "Data Source=DESKTOP-9U3JDBI\\SQLEXPRESS;Initial Catalog=WWSD;Integrated Security=True";
            string query = @"
          SELECT 
    bp.Product_name,
    bp.Buy_Price,
    sp.Sell_Price,
    e.Total_Expense,
    t.CustomerName,
    t.Quantity AS Transaction_Quantity,
    t.Price AS Transaction_Price,
    t.Total AS Transaction_Total,
    (CAST(sp.Sell_Price AS DECIMAL(10, 2)) - CAST(bp.Buy_Price AS DECIMAL(10, 2))) AS Profit_Per_Unit,
    ((CAST(sp.Sell_Price AS DECIMAL(10, 2)) - CAST(bp.Buy_Price AS DECIMAL(10, 2))) * t.Quantity) AS Total_Profit,
    (((CAST(sp.Sell_Price AS DECIMAL(10, 2)) - CAST(bp.Buy_Price AS DECIMAL(10, 2))) * t.Quantity) - e.Total_Expense) AS Net_Profit
FROM 
    dbo.ByPrd bp
INNER JOIN 
    dbo.SlPrd sp ON bp.Product_name = sp.Product_name
INNER JOIN 
    dbo.Expenses2 e ON bp.Product_name = e.Product_name
INNER JOIN 
    dbo.Transactions t ON bp.Product_name = t.ProductName;
";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }


    }
}
