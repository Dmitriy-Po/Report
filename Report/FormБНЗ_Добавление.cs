using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Linq;

namespace Report
{
    public partial class FormБНЗ_Добавление : Form
    {
        SQliteDB db;
        DataSet dataset;    
        public DataTable table0;
        DataTable table1, table_list;        
        SQLiteDataAdapter adapter, adapter_list;
        SQLiteCommandBuilder command, command_list;
        public List<КорректирующиеКоэффиценты> ListKK = new List<КорректирующиеКоэффиценты>();              


        public ushort StatusOperation { get; set; } /*Для выбора режима Редактирования, Добавления или Дубликата */
        public int CurrentDataRow { get; set; }

        public FormБНЗ_Добавление ()
        {
            InitializeComponent();                       
        }
        public void FillCombobox ()
        {
            int y = DateTime.Now.Year - 3;
            do
            {
                comboBoxYear.Items.Add(++y);
            } while (y != DateTime.Now.Year);            
        }     
        void FillDataGrid ()
        {
            string query =
                            "SELECT КорректирующиеКоэффиценты.код, Наименование, ЗначениеКоэффицента.Значение " +
                            "FROM КоррКоэффицентБазовогоНорматива " +
                            "JOIN КорректирующиеКоэффиценты ON КорректирующиеКоэффиценты.код = КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК " +
                            "JOIN ЗначениеКоэффицента ON ЗначениеКоэффицента.Корректирующие_ВК = КорректирующиеКоэффиценты.код " +
                            $"WHERE КоррКоэффицентБазовогоНорматива.Календарный_год LIKE '{comboBoxYear.SelectedItem.ToString()}%' " +
                            $"AND КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК = {CurrentDataRow}; " +

                            //Table1
                            "SELECT КорректирующиеКоэффиценты.код, Наименование, Уточнение FROM КорректирующиеКоэффиценты WHERE КорректирующиеКоэффиценты.код " +
                            "NOT IN(SELECT КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК FROM КоррКоэффицентБазовогоНорматива " +
                            $"WHERE КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК = {CurrentDataRow}); ";



            db = new SQliteDB();
            //using (SQLiteConnection conection = new SQLiteConnection(db.ConnectionDB))
            //{
            //    conection.Open();

                adapter = new SQLiteDataAdapter(query, db.ConnectionDB);
                dataset = new DataSet();

                table0 = new DataTable();
                table1 = new DataTable();                

                adapter.Fill(dataset);

                table0 = dataset.Tables[0];
                table1 = dataset.Tables[1];               

                dataGridViewKoef.DataSource = table0;
                dataGridViewKoef.Columns[0].Width = 25;
                dataGridViewKoef.Columns[1].Visible = false;
                dataGridViewKoef.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewKoef.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                ListKK.Clear();
                foreach (DataRow row in table1.Rows)
                {
                    ListKK.Add(new КорректирующиеКоэффиценты(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString()));
                }

            
        }
        void Operation ()
        {
            int id = 0;
            // 1 - добавление, 2 - по шаблону, 3 - редактирование.

            switch (StatusOperation)
            {
                case 1:
                case 2:
                    if (Duplicate())
                    {
                        MessageBox.Show("Такая строка уже существует");
                    }
                    else InsertRecord();

                    break;

                case 3:
                    if (IsCurrentRows(out id))
                    {
                        UpdateRecord(id);
                    }
                    //else MessageBox.Show("Такая строка уже существует");

                    break;
            }
        }
        bool Duplicate ()
        {
            DataTable table = GetTable();
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        DataTable GetTable ()
        {
            SQliteDB db = new SQliteDB();
            // Дубликат - исходя из выбранных полей на форме.            

            using (SQLiteConnection connection = new SQLiteConnection(db.ConnectionDB))
            {
                string query = "SELECT * FROM БазовыйНормативЗатрат WHERE "+
                                    $"БазовыйНормативЗатрат.Наименование = '{textBoxDesc.Text}'";

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
            }
        }
        bool IsCurrentRows (out int out_id)
        {
            DataTable table = GetTable();
            // Три состояния при редактировании, когда (ид совпадает, когда не совпадает) и когда отсувствует.

            try
            {
                if (table.Rows[0][0] != null)
                {
                    int fantom_id = Convert.ToInt32(table.Rows[0][0]);

                    if (CurrentDataRow == fantom_id)
                    {
                        out_id = CurrentDataRow;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Такая строка уже существует");
                        out_id = 0;
                        return false;
                    };
                }
            }
            catch (IndexOutOfRangeException)
            {
                // В случае, когда таблица вернула 0 результатов, это означает, 
                // что записей по указанным параметрам не существует, и обновление редактируемой записи возможно.
                out_id = CurrentDataRow;
                return true;
            }
            out_id = 0;
            return false;
        }
        void UpdateRecord (int id)//обновление
        {
            SQliteDB database = new SQliteDB();
            string DATE = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01").ToString("yyyy-MM-dd");

            using (SQLiteConnection connection = new SQLiteConnection(database.ConnectionDB))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand($"UPDATE БазовыйНормативЗатрат SET Наименование = '{textBoxDesc.Text}', " +
                                                            $"Полное_наименование = '{textBoxFillDesc.Text}', Комментарий = '{textBoxComment.Text}', " +
                                                            $"КалендарныйГод = '{DATE}' " +
                                                            $"WHERE БазовыйНормативЗатрат.код = {CurrentDataRow}; "+
                                                            /*добавить update к таблице БНЗСтоимостнойГруппы*/
                                                            $"UPDATE КоррКоэффицентБазовогоНорматива SET Календарный_год = '{DATE}' "+
                                                            $"WHERE КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК = {CurrentDataRow}"
                                                            , connection);

                command.ExecuteNonQuery();
            }
        }
        void InsertRecord ()//сохранение
        {
            SQliteDB database = new SQliteDB();
            string query = "SELECT * FROM БазовыйНормативЗатрат";

            using (SQLiteConnection connection = new SQLiteConnection(database.ConnectionDB))
            {
                connection.Open();

                adapter = new SQLiteDataAdapter(query, connection);
                table0 = new DataTable();

                DataRow new_row = table0.NewRow();

                adapter.Fill(table0);

                new_row[1] = textBoxDesc.Text;
                new_row[2] = textBoxFillDesc.Text;
                new_row[3] = textBoxComment.Text;

                string DATE = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01").ToString("yyyy-MM-dd");

                new_row[4] = DATE;

                table0.Rows.Add(new_row);

                SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter);

                adapter.Update(table0);

                //Вставка записей из DataGid.
                //string DATE = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01").ToString("yyyy-MM-dd");
                foreach (DataGridViewRow row in dataGridViewKoef.Rows)
                {
                    SQLiteCommand c = new SQLiteCommand("INSERT INTO КоррКоэффицентБазовогоНорматива(Календарный_год, Базовый_норматив_ВК, Корр_коэфф_ВК) " +
                    $"VALUES ('{DATE}', (select MAX(код) from БазовыйНормативЗатрат), {row.Cells["код"].Value.ToString()})", connection);
                    c.ExecuteNonQuery();
                }

            }
        }




        private void FormБНЗ_Добавление_Load (object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void buttonAddKoef_Click (object sender, EventArgs e)
        {
            FormListKoef lk = new FormListKoef();
            lk.CurrentRow = CurrentDataRow;
            lk.DATE = comboBoxYear.SelectedItem.ToString();

            lk.ShowDialog();
            FillDataGrid();
        }

        private void comboBoxCorrectKoef_SelectionChangeCommitted (object sender, EventArgs e)
        {
            
        }

        private void FormБНЗ_Добавление_FormClosed (object sender, FormClosedEventArgs e)
        {
            db = new SQliteDB();
            using (SQLiteConnection c = new SQLiteConnection(db.ConnectionDB))
            {                
                c.Open();
                /*Проверка на удаление только по одному признаку - пустое поле наименование.
                 * Возможно, следут добавить ещё два условия для удаления. 
                 * */ 
                SQLiteCommand cm = new SQLiteCommand("DELETE FROM БазовыйНормативЗатрат WHERE БазовыйНормативЗатрат.Наименование = '';", c);
                cm.ExecuteNonQueryAsync();
            }
        }

        void DeleteSelectedRows ()
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result.Equals(DialogResult.Yes))
            {
                List<string> ListId = new List<string>();

                SQliteDB q = new SQliteDB();
                foreach (DataGridViewRow row in dataGridViewKoef.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        ListId.Add(row.Cells["код"].Value.ToString());
                    }
                }
                //foreach (DataGridViewRow row in dataGridViewKoef.Rows)
                //{
                //    if (Convert.ToBoolean(row.Cells[0].Value))
                //    {
                //        dataGridViewKoef.Rows.Remove(row);
                //    }
                //}


                //q.Delete("КоррКоэффицентБазовогоНорматива", "КоррКоэффицентБазовогоНорматива.код", string.Join(",", ListId.ToArray()));
                string s = string.Join(",", ListId.ToArray());

                using (SQLiteConnection connection = new SQLiteConnection(q.ConnectionDB))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("DELETE FROM КоррКоэффицентБазовогоНорматива WHERE " +
                                                                $"КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК IN({s}) AND " +
                                                                $"КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК = {CurrentDataRow}", connection);
                    command.ExecuteNonQuery();

                }
                FillDataGrid();
            }
        }

        private void buttonDeleteKoef_Click (object sender, EventArgs e)
        {
            UInt32 count = 0;
            foreach (DataGridViewRow row in dataGridViewKoef.Rows)
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
        bool IsCorrect ()
        {
            int year = comboBoxYear.SelectedIndex;
            if (!string.IsNullOrWhiteSpace(textBoxDesc.Text) && year >= 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }



        private void buttonSaveAndClose_Click (object sender, EventArgs e)
        {
            if (IsCorrect())
            {
                Operation();
                Close(); 
            }
        }

        private void buttonSave_Click (object sender, EventArgs e)
        {
            if (IsCorrect())
            {
                Operation(); 
            }
        }
    }
}
