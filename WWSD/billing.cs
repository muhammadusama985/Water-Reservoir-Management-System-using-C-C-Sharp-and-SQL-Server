using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WWSD
{
    public partial class billing : UserControl
    {
        private PrintDocument printDocument;

        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-9U3JDBI\SQLEXPRESS;Initial Catalog=WWSD;Integrated Security=True");

        public billing()
        {
            InitializeComponent();
            this.dataGridView4.CellClick += new DataGridViewCellEventHandler(this.dataGridView4_CellClick);
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button1.Click += new EventHandler(this.button1_Click);
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                displayData();
                displayData12();
            }
        }

        public void displayData()
        {
            try
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM SlPrd where date_delete is null;", connect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView4.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }
        public void displayData12()
        {
            try
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM Transactions;", connect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView3.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void billing_Load(object sender, EventArgs e)
        {

        }
        public void clearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.ReadOnly = true;
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView4.Rows[e.RowIndex];
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[4].Value.ToString();

            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            string customerName = textBox1.Text.Trim();
            string productName = textBox2.Text.Trim();
            string productPrice = textBox3.Text.Trim();
            string quantityText = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(productName) ||
                string.IsNullOrEmpty(productPrice) || string.IsNullOrEmpty(quantityText))
            {
                return;
            }

            if (!int.TryParse(quantityText, out int enteredQuantity))
            {
                MessageBox.Show("Please enter a valid quantity");
                return;
            }

            int selectedRowIndex = dataGridView4.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView4.Rows[selectedRowIndex];
            int productId = Convert.ToInt32(selectedRow.Cells["id"].Value);
            int productQuantity = Convert.ToInt32(selectedRow.Cells["Quantity"].Value);

            if (enteredQuantity > productQuantity)
            {
                MessageBox.Show("Entered quantity exceeds available stock");
                return;
            }

            try
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }

                // Update the product quantity in the database
                SqlCommand updateCommand = new SqlCommand(
                    "UPDATE SlPrd SET Quantity = Quantity - @Quantity WHERE id = @ProductID", connect);
                updateCommand.Parameters.AddWithValue("@Quantity", enteredQuantity);
                updateCommand.Parameters.AddWithValue("@ProductID", productId); // Use productId instead of SelID
                updateCommand.ExecuteNonQuery();

                // Update the product quantity in the DataGridView
                selectedRow.Cells["Quantity"].Value = productQuantity - enteredQuantity;

                // Add the item to the bill DataGridView
                decimal price = Convert.ToDecimal(productPrice);
                decimal total = enteredQuantity * price;

                dataGridView1.Rows.Add(customerName, productName, enteredQuantity, price, total);

                // Calculate grand total
                decimal grandTotal = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    grandTotal += Convert.ToDecimal(row.Cells["Total"].Value);
                }
                textBox5.Text = $"{grandTotal}";

                // Clear input fields
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveBillDataToDatabase(); // Save the data to the database

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
                ClearBillData();
            }
        }
        private void ClearBillData()
        {
            dataGridView1.Rows.Clear();
            textBox5.Clear();
        }

        private void SaveBillDataToDatabase()
        {
            try
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string customerName = row.Cells[0].Value?.ToString();
                    string productName = row.Cells[1].Value?.ToString();
                    int quantity = Convert.ToInt32(row.Cells[2].Value);
                    decimal price = Convert.ToDecimal(row.Cells[3].Value);
                    decimal total = Convert.ToDecimal(row.Cells[4].Value);

                    string query = "INSERT INTO Transactions (CustomerName, ProductName, Quantity, Price, Total) " +
                                   "VALUES (@CustomerName, @ProductName, @Quantity, @Price, @Total)";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Total", total);

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float rightMargin = e.MarginBounds.Right;
            float pageWidth = e.PageBounds.Width;
            float columnSpacing = 40; // Additional spacing between columns

            Font printFont = new Font("Arial", 10);
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font subTitleFont = new Font("Arial", 12, FontStyle.Regular);
            Font headerFont = new Font("Arial", 10, FontStyle.Bold);

            // Print Main Title with extra space above
            string title = "USAMA WHOLE SALE DEALER";
            SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
            float titleX = (pageWidth - titleSize.Width) / 2;
            yPos += 20; // Add space above the main title
            e.Graphics.DrawString(title, titleFont, Brushes.Black, titleX, yPos, new StringFormat());
            yPos += titleFont.GetHeight(e.Graphics) + 20; // Add space below the main title

            // Print Sub-title (e.g., Report Title and Date)
            string subTitle = "Customer Bill";
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string subTitleLine = subTitle + "  " + date;
            SizeF subTitleSize = e.Graphics.MeasureString(subTitleLine, subTitleFont);
            float subTitleX = (pageWidth - subTitleSize.Width) / 2;
            e.Graphics.DrawString(subTitleLine, subTitleFont, Brushes.Black, subTitleX, yPos, new StringFormat());
            yPos += subTitleFont.GetHeight(e.Graphics) + 30; // Add more space below the sub-title

            // Define column widths
            float[] columnWidths = { 100, 150, 80, 80, 100 }; // Adjust these values as needed
            float currentX = leftMargin;

            // Print column headers with extra space below
            e.Graphics.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos); // Top border
            yPos += 5;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                e.Graphics.DrawString(dataGridView1.Columns[i].HeaderText, headerFont, Brushes.Black, currentX, yPos, new StringFormat());
                currentX += columnWidths[i] + columnSpacing;
            }
            yPos += headerFont.GetHeight(e.Graphics) + 15; // Add extra space below headers
            e.Graphics.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos); // Bottom border
            yPos += 10;

            // Print each row
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                currentX = leftMargin;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[j].Value?.ToString(), printFont, Brushes.Black, currentX, yPos, new StringFormat());
                    currentX += columnWidths[j] + columnSpacing;
                }
                yPos += printFont.GetHeight(e.Graphics) + 5;
            }

            // Add a line before the grand total
            yPos += 20; // Add some space before the line
            e.Graphics.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Print grand total
            string grandTotalText = "Grand Total: " + textBox5.Text;
            SizeF grandTotalSize = e.Graphics.MeasureString(grandTotalText, titleFont);
            float grandTotalX = rightMargin - grandTotalSize.Width;
            e.Graphics.DrawString(grandTotalText, titleFont, Brushes.Black, grandTotalX, yPos, new StringFormat());

            // Clear the data in the DataGridView after printing
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            displayData12();
        }

        private void printDocument1_EndPrint(object sender, PrintEventArgs e)
        {
            ClearBillData();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
