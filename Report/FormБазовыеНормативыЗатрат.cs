using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Report
{
    public partial class FormБазовыеНормативыЗатрат : Form
    {
        SQliteDB db;
        DataTable table;
        SQLiteDataAdapter adapter;
        SQLiteCommand command;

        int CurrentRow { get; set; }
        public FormБазовыеНормативыЗатрат()
        {
            InitializeComponent();
        }
        int AddFictiveRow ()
        {
            // Удаление пустых строк.
            db = new SQliteDB();
            using (SQLiteConnection conn = new SQLiteConnection(db.ConnectionDB))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO БазовыйНормативЗатрат(Наименование) VALUES ('');", conn);
                command.ExecuteNonQuery();

                command.CommandText = "select MAX(БазовыйНормативЗатрат.код) as 'код' FROM БазовыйНормативЗатрат;";
                return Convert.ToInt32(command.ExecuteScalar());
            }
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
                        fadd.Text = "Редактирование статьи затрат";
                        fadd.StatusOperation = 3;
                        fadd.CurrentDataRow = Convert.ToInt32(row.Cells["код"].Value);    // получение строки для использования в функции IsDuplicate.
                    }
                    else
                    {
                        fadd.Text = "Создание по шаблону";
                        fadd.StatusOperation = 2;
                        fadd.CurrentDataRow = Convert.ToInt32(row.Cells["код"].Value);    // 0 - для режима Создать по шаблону.
                    }
                    CurrentRow = row.Index;
                }
            }         
            
            fadd.ShowDialog();
            FillDataGrid();
            dataGridViewNormals.Rows[CurrentRow].Cells[0].Value = true;
            dataGridViewNormals.Rows[CurrentRow].Selected = true;
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
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result.Equals(DialogResult.Yes))
            {
                db = new SQliteDB();
                List<object> list_id = new List<object>();

                foreach (DataGridViewRow row in dataGridViewNormals.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        list_id.Add(row.Cells["код"].Value);
                    }
                }
                               
                string id = string.Join(",", list_id);
                using (SQLiteConnection c = new SQLiteConnection(db.ConnectionDB))
                {
                    c.Open();
                    command = new SQLiteCommand("DELETE FROM БазовыйНормативЗатрат " +
                                            $"WHERE БазовыйНормативЗатрат.код IN({id})", c);

                    command.ExecuteNonQuery(); 
                }
                FillDataGrid();
            }
        }
        void FillDataGrid ()
        {
            // Зполение DataGrid.
            db = new SQliteDB();
            
            string query =
                            $"SELECT БазовыйНормативЗатрат.код, БазовыйНормативЗатрат.Наименование," +
                                "БазовыйНормативЗатрат.Полное_наименование,"+
                                "БазовыйНормативЗатрат.Комментарий "+                            
                                $"FROM БазовыйНормативЗатрат WHERE БазовыйНормативЗатрат.КалендарныйГод LIKE '{comboBoxYear.SelectedItem.ToString()}%'";
                            //"SELECT DISTINCT(БазовыйНормативЗатрат.код), "+
                            //"БазовыйНормативЗатрат.Наименование, "+
                            //"Полное_наименование, "+
                            //"Комментарий "+
                            //"FROM КоррКоэффицентБазовогоНорматива JOIN БазовыйНормативЗатрат ON БазовыйНормативЗатрат.код = КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК "+
                            //$"WHERE КоррКоэффицентБазовогоНорматива.Календарный_год LIKE '{comboBoxYear.SelectedItem.ToString()}%'";

            table = new DataTable();

            adapter = new SQLiteDataAdapter(query, db.ConnectionDB);

            adapter.Fill(table);

            dataGridViewNormals.DataSource = table;
            dataGridViewNormals.Columns[0].Width = 50;
            dataGridViewNormals.Columns[1].Width = 50;
            dataGridViewNormals.Columns["Наименование"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewNormals.Columns["Полное_наименование"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewNormals.Columns["Комментарий"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewNormals.Columns["код"].Visible = false;
            try
            {
                dataGridViewNormals.Rows[0].Selected = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }
        }


        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            FormБНЗ_Добавление f = new FormБНЗ_Добавление();
            f.FillCombobox();
            f.comboBoxYear.SelectedItem = comboBoxYear.SelectedItem;
            f.Text = "Добавление статьи затрат";
            f.StatusOperation = 3;
            f.CurrentDataRow = AddFictiveRow();
            f.ShowDialog();
            FillDataGrid();
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

        private void comboBoxYear_SelectionChangeCommitted (object sender, EventArgs e)
        {
            FillDataGrid();
        }
    }
}
