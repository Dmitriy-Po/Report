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

namespace Report.Forms
{
    public partial class FormGroups : Form
    {
        public FormGroups ()
        {
            InitializeComponent();
        }
        SQliteDB DB;
        DataTable Table;
        SQLiteDataAdapter Adapter;
        SQLiteCommandBuilder Command;

        void FillDataGrid ()
        {
            DB = new SQliteDB();

            SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB);
            connection.Open();

            Adapter = new SQLiteDataAdapter("select * from СтоимостнаяГруппаКалГода", connection);
            Table = new DataTable();
            Adapter.Fill(Table);                       

            dataGridViewGoups.DataSource = Table;
            connection.Close();

        }
        void FillComboBox ()
        {
            // Заполнение combobox.            
            int y = DateTime.Today.Year - 3;
            do
            {
                comboBoxYear.Items.Add(++y);
            } while (y != DateTime.Today.Year);
            comboBoxYear.SelectedItem = y;
        }
        void DeleteSelectedRows ()
        {
            DB = new SQliteDB();
            DialogResult result = MessageBox.Show("Вы действительно хотите удлать запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result.Equals(DialogResult.Yes))
            {
                foreach (DataGridViewRow row in dataGridViewGoups.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        dataGridViewGoups.Rows.Remove(row);
                    }
                }
                Adapter = new SQLiteDataAdapter("select СтоимостнаяГруппаКалГода.код from СтоимостнаяГруппаКалГода", DB.ConnectionDB);
                Command = new SQLiteCommandBuilder(Adapter);

                Adapter.Update(Table);

            }
        }
        bool CountSelectedRows (string tooltip)
        {
            UInt32 count = 0;
            foreach (DataGridViewRow row in dataGridViewGoups.Rows)
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
        void FillComponents (bool IsEditingMode)
        {
            // Заполение компонентов на форме из datagrid.
            FormGroupAdd fadd = new FormGroupAdd();
            fadd.FillCombobox();

            foreach (DataGridViewRow row in dataGridViewGoups.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    fadd.textBoxDesc.Text           = row.Cells["Наименование"].Value.ToString();
                    fadd.textBoxFillDesc.Text       = row.Cells["ПолноеНаименование"].Value.ToString();
                    fadd.textBoxComment.Text        = row.Cells["Комментарий"].Value.ToString();
                    fadd.comboBoxYear.SelectedItem = comboBoxYear.SelectedItem;

                    if (IsEditingMode)
                    {
                        fadd.Text = "Редактирование норматива";
                        fadd.StatusOperation = 3;
                        fadd.CurrentDataRow_id = Convert.ToInt32(row.Cells["код"].Value);    // получение строки для использования в функции IsDuplicate.
                    }
                    else
                    {
                        fadd.Text = "Создание по шаблону";
                        fadd.StatusOperation = 2;
                        fadd.CurrentDataRow_id = 0;    // 0 - для режима Создать по шаблону.
                    }
                }
            }
            
            fadd.ShowDialog();
            FillDataGrid();
        }



        private void FormGroups_Load (object sender, EventArgs e)
        {
            FillComboBox();
            FillDataGrid();
        }

        private void buttonDeleteSelected_Click (object sender, EventArgs e)
        {
            UInt32 count = 0;
            foreach (DataGridViewRow row in dataGridViewGoups.Rows)
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

        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            FormGroupAdd f = new FormGroupAdd();
            f.FillCombobox();
            f.Text = "Добавление группы";
            f.StatusOperation = 1;
            f.ShowDialog();
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
    }
}
