using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report
{
    public partial class FormContext : Form
    {
        public FormContext ()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged (object sender, EventArgs e)
        {
            
            
        }

        public void listBoxItem_DoubleClick (object sender, EventArgs e)
        {
                    }

        public void buttonSelect_Click (object sender, EventArgs e)
        {
            FormAddReport fad = new FormAddReport();
            fad.textBoxFilial.Text = "123131313";
            Close();
        }
    }
}
