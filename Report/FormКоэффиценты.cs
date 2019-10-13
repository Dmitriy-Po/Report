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


        void FillComponents ()
        {
            //заполение компонентов на форме Коэфицентов добавления, из datagrid
            Form_КК_Добавление f = new Form_КК_Добавление();

            foreach (DataGridViewRow row in dataGridViewCoeff.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    f.textBoxDesc.Text = row.Cells[1].Value.ToString();
                    f.textBoxFullDesc.Text = row.Cells[2].Value.ToString();
                    f.textBoxDetail.Text = row.Cells[3].Value.ToString();
                    f.textBoxCoeff.Text = row.Cells[4].Value.ToString();
                    f.comboBoxYear.Text = row.Cells[5].Value.ToString();
                    f.textBoxComment.Text = row.Cells[6].Value.ToString();
                    f.comboBoxFormEducation.Text =
                        ListCoef.Where(x => x.id == Convert.ToInt32(row.Cells["id"].Value))
                        .Select(x => x.GetForm())
                        .ElementAt(0);
                }
            }
            f.ShowDialog();
        }
        public bool CountSelectedRows (string tooltip)
        {
            UInt32 count = 0;
            foreach (DataGridViewRow row in dataGridViewCoeff.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    count++;
                }
            }
            if (count > 1 || count == 0)
            {
                MessageBox.Show($"Выберите ОДНУ строку для операции: [{tooltip}]", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else return true;
        }
        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            Form_КК_Добавление f = new Form_КК_Добавление();            
            f.ShowDialog();
        }

        private void FormКоэффиценты_Load(object sender, EventArgs e)
        {
            //заполнение combobox датами из коллекции коэффицентов
            /*Возможно, при наступлении 3000 года, потомкам придётся сменить год (поменять цифру 2 на 3)*/
            
            comboBoxYear.Items.AddRange
                (ListCoef.Select(z => z.GetYear().Substring(6, 4))                
                .Distinct().ToArray());
                     
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //выборка из коллекции на основе выпадающего списка по годам.
            dataGridViewCoeff.Rows.Clear();
            foreach (var item in ListCoef.Where(x => Convert.ToInt32(x.GetYear().Substring(6, 4))
                        == Convert.ToInt32(comboBoxYear.SelectedItem))
                        .Select(x => x))
            {
                dataGridViewCoeff.Rows.Add(0, item.Наименование, 
                    item.ПолноеНаименование, item.Уточнение, 
                    item.GetCoef(), item.GetYear().Substring(6, 4), 
                    item.Комментарий, item.id);
            }
                
        }

        private void buttonAddStingPattern_Click (object sender, EventArgs e)
        {
            if (CountSelectedRows("Добавить по шаблону"))
            {
                FillComponents();
            }
            
        }

        private void buttonEditString_Click (object sender, EventArgs e)
        {
            if (CountSelectedRows("Редактирования"))
            {
                FillComponents(); 
            }
        }

        private void buttonDeleteSelected_Click (object sender, EventArgs e)
        {

        }
    }
}
