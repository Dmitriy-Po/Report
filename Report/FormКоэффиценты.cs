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

        void FillDataGrid ()
        {
            SQliteDB db = new SQliteDB();
            string query = "SELECT " +
                            "КорректирующиеКоэффиценты.Наименование, " +
                            "КорректирующиеКоэффиценты.ПолноеНаименование as 'Полное наименование', " +
                            "КорректирующиеКоэффиценты.Уточнение, " +
                            "ЗначениеКоэффицента.Значение as 'Значение коэффицента', " +
                            "SUBSTR(ЗначениеКоэффицента.КаледндарныйГод, 0, 5) as 'Календарный год', " +
                            "КорректирующиеКоэффиценты.Комментарий,  " +
                            "ФормаОбучения.наименование as 'Форма обучения', " +
                            "ЗначениеКоэффицента.код " +
                            "FROM ЗначениеКоэффицента LEFT JOIN КорректирующиеКоэффиценты ON ЗначениеКоэффицента.Корректирующие_ВК = КорректирующиеКоэффиценты.код " +
                            "JOIN ФормаОбучения ON ФормаОбучения.код = ЗначениеКоэффицента.ФормаОбучения_ВК " +
                            $"WHERE ЗначениеКоэффицента.КаледндарныйГод LIKE '{comboBoxYear.SelectedItem}%'";


            DataTable table = new DataTable();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, db.ConnectionDB);

            //SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter);


            adapter.Fill(table);

            dataGridViewCoeff.DataSource = table;
            dataGridViewCoeff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCoeff.Columns[0].Width = 50;
            dataGridViewCoeff.Columns["код"].Visible = false;
        }
        void FillComponents (bool IsEditingMode)
        {
            //заполение компонентов на форме Коэфицентов добавления, из datagrid
            Form_КК_Добавление f = new Form_КК_Добавление();
            f.FillElements();

            foreach (DataGridViewRow row in dataGridViewCoeff.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    f.textBoxNameCoef.Text = row.Cells["Наименование"].Value.ToString();
                    f.textBoxFullDesc.Text = row.Cells["Полное наименование"].Value.ToString();
                    f.textBoxComment.Text = row.Cells["Комментарий"].Value.ToString();
                    f.textBoxDetail.Text = row.Cells["Уточнение"].Value.ToString();

                    f.textBoxCoeff.Text = row.Cells["Значение коэффицента"].Value.ToString();
                    f.comboBoxFormEducation.SelectedItem    = row.Cells["Форма обучения"].Value;
                    f.comboBoxYear.SelectedItem             = Convert.ToInt32(row.Cells["Календарный год"].Value);
                    
                    if (IsEditingMode)
                    {
                        f.Text = "Редактирование";
                        f.StatusOperation = 3;
                        f.CurrentDataRow = Convert.ToInt32(row.Cells["код"].Value);    // получение строки для использования в функции IsDuplicate.
                    }
                    else
                    {
                        f.Text = "Создание по шаблону";
                        f.StatusOperation = 2;
                        f.CurrentDataRow = 0;    // 0 - для режима Создать по шаблону.
                    }
                }
            }
            
            f.ShowDialog();
            FillDataGrid();
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
            f.FillElements();
            f.Text = "Добавление";
            f.StatusOperation = 1;            
            f.ShowDialog();
            FillDataGrid();
        }

        private void FormКоэффиценты_Load(object sender, EventArgs e)
        {
            // Заполнение combobox.            
            int y = DateTime.Today.Year - 3;
            do
            {
                comboBoxYear.Items.Add(++y);
            } while (y != DateTime.Today.Year);
            comboBoxYear.SelectedItem = y;
            FillDataGrid();
            //comboBox1_SelectionChangeCommitted(sender, e);
        }

        public void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void buttonAddStingPattern_Click (object sender, EventArgs e)
        {
            if (CountSelectedRows("Добавить по шаблону"))
            {
                FillComponents(false);
            }
            
        }

        private void buttonEditString_Click (object sender, EventArgs e)
        {
            if (CountSelectedRows("Редактирования"))
            {
                FillComponents(true); 
            }
        }

        private void buttonDeleteSelected_Click (object sender, EventArgs e)
        {
            List<string> ListId = new List<string>();

            SQliteDB q = new SQliteDB();
            foreach (DataGridViewRow row in dataGridViewCoeff.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    ListId.Add(row.Cells["код"].Value.ToString());
                    dataGridViewCoeff.Rows.Remove(row);
                }
            }
            q.Delete("ЗначениеКоэффицента", "ЗначениеКоэффицента.код", string.Join(",", ListId.ToArray()));            
        }        
    }
}
