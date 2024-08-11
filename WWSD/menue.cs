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
    public partial class menue : UserControl
    {
        public menue()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           product1.Visible = true;
            sellingproduct1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            product1.Visible = false;
           sellingproduct1.Visible = true;
        }
    }
}
