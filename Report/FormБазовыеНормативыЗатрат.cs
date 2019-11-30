using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report
{
    public partial class FormБазовыеНормативыЗатрат : Form
    {
        SQliteDB db;
        DataTable table;
        SQLiteDataAdapter adapter;
        SQLiteCommandBuilder command;

        public FormБазовыеНормативыЗатрат()
        {
            InitializeComponent();
        }
        void FillComponents(bool IsEditingMode)
        {
            // Заполение компонентов на форме из datagrid.
            FormБНЗ_Добавление fadd = new FormБНЗ_Добавление();
            fadd.FillCombobox();
            
            foreach (DataGridViewRow row in dataGridViewNormals.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    fadd.textBoxDesc.Text       = row.Cells["Наименование"].Value.ToString();
                    fadd.textBoxFillDesc.Text   = row.Cells["Полное_наименование"].Value.ToString();
                    fadd.textBoxComment.Text    = row.Cells["Комментарий"].Value.ToString();
                    fadd.comboBoxYear.SelectedItem = comboBoxYear.SelectedItem;

                    if (IsEditingMode)
                    {
                        fadd.Text = "Редактирование норматива";
                        fadd.StatusOperation = 3;
                        fadd.CurrentDataRow = Convert.ToInt32(row.Cells["код"].Value);    // получение строки для использования в функции IsDuplicate.
                    }
                    else
                    {
                        fadd.Text = "Создание по шаблону";
                        fadd.StatusOperation = 2;
                        fadd.CurrentDataRow = 0;    // 0 - для режима Создать по шаблону.
                    }
                }
            }            
            /*
             * Вот здесь добавить функцию заполения DataGrid - она сработает как обновление таблицы.
             * */
            fadd.ShowDialog();
        }
        bool CountSelectedRows (string tooltip)
        {
            UInt32 count = 0;
            foreach (DataGridViewRow row in dataGridViewNormals.Rows)
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
        void DeleteSelectedRows ()
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удлать запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result.Equals(DialogResult.Yes))
            {
                foreach (DataGridViewRow row in dataGridViewNormals.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        dataGridViewNormals.Rows.Remove(row);
                    }
                }

                using (SQLiteConnection connection = new SQLiteConnection(db.ConnectionDB))
                {
                    connection.Open();
                    command = new SQLiteCommandBuilder(adapter);

                    adapter.Update(table);
                }
            }
        }
        void FillDataGrid ()
        {
            // Зполение DataGrid.
            db = new SQliteDB();
            string query = "SELECT * FROM БазовыйНормативЗатрат";

            table = new DataTable();

            adapter = new SQLiteDataAdapter(query, db.ConnectionDB);

            adapter.Fill(table);

            dataGridViewNormals.DataSource = table;
            dataGridViewNormals.Columns[0].Width = 50;
            dataGridViewNormals.Columns[1].Width = 50;
            dataGridViewNormals.Columns["Наименование"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewNormals.Columns["Полное_наименование"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewNormals.Columns["Комментарий"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewNormals.Columns["код"].Visible = false;
        }


        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            FormБНЗ_Добавление f = new FormБНЗ_Добавление();
            f.FillCombobox();
            f.Text = "Добавление норматива";
            f.StatusOperation = 1;
            f.ShowDialog();
        }
        

        private void FormБазовыеНормативыЗатрат_Load (object sender, EventArgs e)
        {
            // Заполнение combobox.            
            int y = DateTime.Today.Year - 3;
            do
            {
                comboBoxYear.Items.Add(++y);
            } while (y != DateTime.Today.Year);
            comboBoxYear.SelectedItem = y;

            FillDataGrid();
        }

        private void buttonDeleteSelected_Click (object sender, EventArgs e)
        {
            UInt32 count = 0;
            foreach (DataGridViewRow row in dataGridViewNormals.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    count++;
                }
            }
            if (count == 0)
            {
                MessageBox.Show($"Выберите ОДНУ или несколько строк для операции удаления", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else DeleteSelectedRows();
            
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

        private void FormБазовыеНормативыЗатрат_Activated (object sender, EventArgs e)
        {
            //FillDataGrid();
        }
    }
}
