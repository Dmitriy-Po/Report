using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Report.Classes;

namespace Report
{
    partial class FormКоэффиценты : Form
    {        
        public FormКоэффиценты ()
        {
            InitializeComponent();
            FormEducation.Fill(ListEducation);
            КорректирующиеКоэффиценты.Fill(ListCoef);
        }
        public List<FormEducation> ListEducation = new List<FormEducation>();
        public List<КорректирующиеКоэффиценты> ListCoef = new List<КорректирующиеКоэффиценты>();




        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            Form_КК_Добавление f = new Form_КК_Добавление();            
            f.ShowDialog();
        }

        private void FormКоэффиценты_Load(object sender, EventArgs e)
        {
            //заполнение cpmbobox датами из коллекции коэффицентов                       
            comboBoxYear.Items.AddRange
                (ListCoef.Select(z => z.GetYear()).Distinct().ToArray());            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //выборка из коллекции на основе выпадающего списка по годам.
            dataGridViewCoeff.Rows.Clear();
            foreach (var item in ListCoef.Where(x => Convert.ToDateTime(x.GetYear())
                        == Convert.ToDateTime(comboBoxYear.SelectedItem))
                        .Select(x => x.GetCoef()))
            {
                dataGridViewCoeff.Rows.Add(0, item); 
            }
                
        }
    }
}
