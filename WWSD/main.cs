using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWSD
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void addBooks_btn_Click(object sender, EventArgs e)
        {
            menue1.Visible = true;
            home11.Visible = false;
            billing1.Visible = false;
            allReport1.Visible = false;
            expenseReport1.Visible = false;

        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            home11.Visible = true;
            menue1.Visible=false;
            billing1.Visible = false;
            expenseReport1.Visible = false;
            allReport1.Visible = false;

        }

        private void returnBooks_btn_Click(object sender, EventArgs e)
        {
            home11.Visible = false;
            menue1.Visible=false;
           billing1.Visible = true;
            expenseReport1.Visible = false;
            allReport1.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            home11.Visible = false;
            menue1.Visible=false;
            billing1.Visible=false;
            expenseReport1.Visible=false;
            allReport1.Visible=true;
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home11.Visible = false;
           menue1.Visible = false;
            billing1.Visible = false;
            expenseReport1.Visible=true;
            allReport1.Visible = false;
        }

        private void main_Load(object sender, EventArgs e)
        {
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                LoginScreen lForm = new LoginScreen();
                lForm.Show();
                this.Hide();
            }
        }
    }
}
