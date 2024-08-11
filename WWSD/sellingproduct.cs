using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWSD
{
    public partial class sellingproduct : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-9U3JDBI\SQLEXPRESS;Initial Catalog=WWSD;Integrated Security=True");

        public sellingproduct()
        {
            InitializeComponent();
            displayData1();
            RefreshData();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                displayData1();
            }
        }
        public void clearFields()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void RefreshData()
        {
            displayData1();
        }
        public void displayData1()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""
          || comboBox1.Text == ""
           || textBox2.Text == ""
           || textBox3.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        connect.Open();
                        string insertData = "INSERT INTO SlPrd " +
                            "(Product_name, Category, Quantity, Sell_Price,  date_insert) " +
                            "VALUES(@bookTitle, @author, @published_date, @status,  @dateInsert)";

                        using (SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@bookTitle", textBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@author", comboBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@published_date", textBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@status", textBox3.Text.Trim());
                            cmd.Parameters.AddWithValue("@dateInsert", DateTime.Today);

                            cmd.ExecuteNonQuery();

                            displayData1();

                            MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            clearFields();


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }
        private int SellID = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""
           || comboBox1.Text == ""
            || textBox2.Text == ""
            || textBox3.Text == "")
            {
                MessageBox.Show("Please fill in all fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    // Assuming `connect` is properly instantiated elsewhere
                    if (connect.State != ConnectionState.Open)
                    {
                        DialogResult check = MessageBox.Show("Are you sure you want to UPDATE Sell ID:" + SellID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (check == DialogResult.Yes)
                        {
                            connect.Open();
                            string updateData = "UPDATE SlPrd SET Product_name = @bookTitle, Category = @author, Quantity = @published_date, Sell_Price = @status, date_update = @dateUpdate WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@bookTitle", textBox1.Text.Trim());
                                cmd.Parameters.AddWithValue("@author", comboBox1.Text.Trim());
                                cmd.Parameters.AddWithValue("@published_date", textBox2.Text);
                                cmd.Parameters.AddWithValue("@status", textBox3.Text.Trim());
                                cmd.Parameters.AddWithValue("@id", SellID);
                                cmd.Parameters.AddWithValue("@dateUpdate", DateTime.Today);

                                cmd.ExecuteNonQuery();

                                displayData1();

                                MessageBox.Show("Updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                clearFields();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""
         || comboBox1.Text == ""
          || textBox2.Text == ""
          || textBox3.Text == "")
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (connect.State != ConnectionState.Open)
                    {
                        DialogResult check = MessageBox.Show("Are you sure you want to DELETE Sell ID:" +SellID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (check == DialogResult.Yes)
                        {
                            connect.Open();
                            string deleteData = "DELETE FROM SlPrd WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                            {
                                cmd.Parameters.AddWithValue("@id", SellID);

                                cmd.ExecuteNonQuery();

                                displayData1();

                                MessageBox.Show("Deleted successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                clearFields();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                SellID = (int)row.Cells[0].Value;
                textBox1.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
                textBox2.Text = row.Cells[3].Value.ToString();
                textBox3.Text = row.Cells[4].Value.ToString();
            }
        }
    }
}
