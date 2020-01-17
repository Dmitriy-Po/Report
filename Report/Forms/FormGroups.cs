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

        int CurrentRow { get; set; }
        void FillDataGrid ()
        {
            DB = new SQliteDB();
            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();
                DB = new SQliteDB();
                //string q = "SELECT DISTINCT(СтоимостнаяГруппаКалГода.код), " +
                //                "СтоимостнаяГруппаКалГода.Наименование,СтоимостнаяГруппаКалГода.ПолноеНаименование, " +
                //                "СтоимостнаяГруппаКалГода.Комментарий, СтоимостнаяГруппаКалГода.КалендарныйГод " +
                //                "FROM БНЗСтоимостнойГруппы " +
                //                "JOIN СтоимостнаяГруппаКалГода ON СтоимостнаяГруппаКалГода.код = БНЗСтоимостнойГруппы.СтоимостнаяГруппаКалГода_ВК " +
                //                $"WHERE БНЗСтоимостнойГруппы.КалендарныйГод LIKE '{comboBoxYear.SelectedItem.ToString()}%'";
                string q = $"SELECT СтоимостнаяГруппаКалГода.код, Наименование, "+
                    "ПолноеНаименование as 'Полное наименование', "+
                    "Комментарий, SUBSTR(КалендарныйГод, 1,4) as 'Календарный год' "+                    
                    $"FROM СтоимостнаяГруппаКалГода WHERE СтоимостнаяГруппаКалГода.КалендарныйГод LIKE '{comboBoxYear.SelectedItem.ToString()}%'";

                Adapter = new SQLiteDataAdapter(q, connection);

                Table = new DataTable();
                Adapter.Fill(Table);
                dataGridViewGoups.DataSource = Table;
                dataGridViewGoups.Columns[1].Visible = false;
                dataGridViewGoups.Columns[0].Width = 30;
                dataGridViewGoups.Columns["Полное наименование"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                try
                {
                    dataGridViewGoups.Rows[0].Selected = false;
                }
                catch (ArgumentOutOfRangeException)
                {
                    
                }
            };

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
            
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result.Equals(DialogResult.Yes))
            {
                List<object> list_id = new List<object>();
                DB = new SQliteDB();
                foreach (DataGridViewRow row in dataGridViewGoups.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        list_id.Add(row.Cells["код"].Value);
                    }
                }
                using (SQLiteConnection conn = new SQLiteConnection(DB.ConnectionDB))
                {
                    conn.Open();                   

                    string id = string.Join(",", list_id);
                    SQLiteCommand comm = new SQLiteCommand("DELETE FROM СтоимостнаяГруппаКалГода "+
                                $"WHERE СтоимостнаяГруппаКалГода.код IN({id})", conn);

                    comm.ExecuteNonQuery();
                }
                FillDataGrid();
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
                    fadd.textBoxFillDesc.Text       = row.Cells["Полное наименование"].Value.ToString();
                    fadd.textBoxComment.Text        = row.Cells["Комментарий"].Value.ToString();
                    fadd.comboBoxYear.SelectedItem = comboBoxYear.SelectedItem;

                    if (IsEditingMode)
                    {
                        fadd.Text = "Редактирование группы";
                        fadd.StatusOperation = 3;
                        fadd.CurrentDataRow_id = Convert.ToInt32(row.Cells["код"].Value);    // получение строки для использования в функции IsDuplicate.
                    }
                    else
                    {
                        fadd.Text = "Создание по шаблону";
                        fadd.StatusOperation = 2;
                        fadd.CurrentDataRow_id = Convert.ToInt32(row.Cells["код"].Value);    // 0 - для режима Создать по шаблону.
                    }
                    CurrentRow = row.Index;
                }
            }
            
            fadd.ShowDialog();
            FillDataGrid();
            dataGridViewGoups.Rows[CurrentRow].Cells[0].Value = true;
            dataGridViewGoups.Rows[CurrentRow].Selected = true;
        }
        int AddFictiveRow ()
        {
            DB = new SQliteDB();
            using (SQLiteConnection conn = new SQLiteConnection(DB.ConnectionDB))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO СтоимостнаяГруппаКалГода(Наименование) VALUES ('');", conn);
                command.ExecuteNonQuery();

                command.CommandText = "select MAX(СтоимостнаяГруппаКалГода.код) as 'код' FROM СтоимостнаяГруппаКалГода;";
                return Convert.ToInt32(command.ExecuteScalar());
            }
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
                MessageBox.Show($"Выберите одну или несколько строк для операции удаления", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else DeleteSelectedRows();
        }

        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            FormGroupAdd f = new FormGroupAdd();
            f.FillCombobox();
            f.Text = "Добавление группы";
            f.StatusOperation = 3;
            f.CurrentDataRow_id = AddFictiveRow();
            
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

        private void comboBoxYear_SelectionChangeCommitted (object sender, EventArgs e)
        {
            FillDataGrid();
        }
    }
}
