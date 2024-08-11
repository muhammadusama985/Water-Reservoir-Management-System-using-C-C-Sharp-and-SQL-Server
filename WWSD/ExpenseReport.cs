using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWSD
{
    public partial class ExpenseReport : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-9U3JDBI\SQLEXPRESS;Initial Catalog=WWSD;Integrated Security=True");

        public ExpenseReport()
        {
            InitializeComponent();
            displayData();
            textBox3.TextChanged += new EventHandler(ExpenseAmount_TextChanged);
            textBox7.TextChanged += new EventHandler(ExpenseAmount_TextChanged);
            textBox6.TextChanged += new EventHandler(ExpenseAmount_TextChanged);
            textBox5.TextChanged += new EventHandler(ExpenseAmount_TextChanged);
            textBox1.TextChanged += new EventHandler(ExpenseAmount_TextChanged);
        }

        private void clearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";

        }

        private void CalculateAndDisplayTotal()
        {
            decimal expenseAmount1 = decimal.TryParse(textBox3.Text, out decimal exp1) ? exp1 : 0;
            decimal expenseAmount2 = decimal.TryParse(textBox7.Text, out decimal exp2) ? exp2 : 0;
            decimal expenseAmount3 = decimal.TryParse(textBox6.Text, out decimal exp3) ? exp3 : 0;
            decimal expenseAmount4 = decimal.TryParse(textBox5.Text, out decimal exp4) ? exp4 : 0;
            decimal expenseAmount5 = decimal.TryParse(textBox1.Text, out decimal exp5) ? exp5 : 0;

            decimal totalAmount = expenseAmount1 + expenseAmount2 + expenseAmount3 + expenseAmount4 + expenseAmount5;

            label12.Text = $"Total: {totalAmount:C}";
        }

        private void ExpenseAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayTotal();
        }
        public void displayData()
        {
            try
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM Expenses2;", connect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-9U3JDBI\\SQLEXPRESS;Initial Catalog=WWSD;Integrated Security=True";

            string productName = textBox1.Text;
            string expenseName1 = textBox2.Text;
            decimal expenseAmount1 = decimal.TryParse(textBox3.Text, out decimal exp1) ? exp1 : 0;
            string expenseName2 = textBox4.Text;
            decimal expenseAmount2 = decimal.TryParse(textBox7.Text, out decimal exp2) ? exp2 : 0;
            string expenseName3 = textBox10.Text;
            decimal expenseAmount3 = decimal.TryParse(textBox6.Text, out decimal exp3) ? exp3 : 0;
            string expenseName4 = textBox9.Text;
            decimal expenseAmount4 = decimal.TryParse(textBox5.Text, out decimal exp4) ? exp4 : 0;
            string expenseName5 = textBox8.Text;
            decimal expenseAmount5 = decimal.TryParse(textBox11.Text, out decimal exp5) ? exp5 : 0;

            decimal totalAmount = expenseAmount1 + expenseAmount2 + expenseAmount3 + expenseAmount4 + expenseAmount5;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Expenses2 (Product_name, ExpenseName1, ExpenseAmount1, ExpenseName2, ExpenseAmount2, ExpenseName3, ExpenseAmount3, ExpenseName4, ExpenseAmount4, ExpenseName5, ExpenseAmount5, Total_Expense,issue_date) " +
                               "VALUES (@ProductName, @ExpenseName1, @ExpenseAmount1, @ExpenseName2, @ExpenseAmount2, @ExpenseName3, @ExpenseAmount3, @ExpenseName4, @ExpenseAmount4, @ExpenseName5, @ExpenseAmount5, @TotalAmount,@issdt)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@ExpenseName1", expenseName1);
                    command.Parameters.AddWithValue("@ExpenseAmount1", expenseAmount1);
                    command.Parameters.AddWithValue("@ExpenseName2", expenseName2);
                    command.Parameters.AddWithValue("@ExpenseAmount2", expenseAmount2);
                    command.Parameters.AddWithValue("@ExpenseName3", expenseName3);
                    command.Parameters.AddWithValue("@ExpenseAmount3", expenseAmount3);
                    command.Parameters.AddWithValue("@ExpenseName4", expenseName4);
                    command.Parameters.AddWithValue("@ExpenseAmount4", expenseAmount4);
                    command.Parameters.AddWithValue("@ExpenseName5", expenseName5);
                    command.Parameters.AddWithValue("@ExpenseAmount5", expenseAmount5);
                    command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    command.Parameters.AddWithValue("@issdt", DateTime.Now);


                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data saved successfully.");


                        label12.Text = $"Total: {totalAmount:C}";
                        displayData();
clearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Convert the date strings to DateTime objects
                DateTime startDate;
                DateTime endDate;
                if (!DateTime.TryParse(FromDate.Text, out startDate) || !DateTime.TryParse(Todate.Text, out endDate))
                {
                    MessageBox.Show("Invalid date format. Please enter a valid date.", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var select = "SELECT * FROM Expenses2 WHERE issue_date BETWEEN @start AND @end ;";

                var dataAdapter = new SqlDataAdapter(select, connect);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@start", startDate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@end", endDate);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connect != null)
                {
                    connect.Close();
                }
            }
        }
    }
    
    }

