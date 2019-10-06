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
    public partial class FormБазовыеНормативыЗатрат : Form
    {
        public FormБазовыеНормативыЗатрат()
        {
            InitializeComponent();
           // БазовыеНормативыЗатрат BS = new БазовыеНормативыЗатрат("Группа 1", "2016",
              //  new decimal[] { 37.01m, 42.56m, 47.70m, 0.0m });
            

            //КорректирующиеКоэффиценты KS = new КорректирующиеКоэффиценты(1.15m);           
           
        }

        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            FormБНЗ_Добавление f = new FormБНЗ_Добавление();
            f.ShowDialog();
        }

        private void dataGridView1_CellContentClick (object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
