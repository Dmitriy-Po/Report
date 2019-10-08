using System;
using Report.Classes;
using System.Windows.Forms;
using System.Linq;

namespace Report
{
    public partial class Form_КК_Добавление : Form
    {
        public Form_КК_Добавление ()
        {
            InitializeComponent();
        }
        void IsDuplicate()
        {

        }

        private void buttonSaveAndClose_Click (object sender, EventArgs e)
        {            
        }

        private void Form_КК_Добавление_Load(object sender, EventArgs e)
        {
            FormКоэффиценты c = new FormКоэффиценты();
            comboBoxFormEducation.Items.AddRange
                (c.ListEducation.Select(x => x.Desc).ToArray());

            comboBoxYear.Items.AddRange(c.ListCoef.Select(y => y.GetYear()).ToArray());
        }
    }
}
