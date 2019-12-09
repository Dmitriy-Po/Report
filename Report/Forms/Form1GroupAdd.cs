using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace Report.Forms
{
    public partial class FormGroupAdd : Form
    {
        public FormGroupAdd ()
        {
            InitializeComponent();
        }
        DataSet ds;
        SQliteDB DB;
        DataTable Table, TableGroup;
        SQLiteDataAdapter Adapter, AdapterGroup;
        SQLiteCommand Command;
        SQLiteCommandBuilder CommndBuilder;
        List<БазовыеНормативыЗатрат> ListNormals = new List<БазовыеНормативыЗатрат>();

        public ushort StatusOperation { get; set; } /*Для выбора режима Редактирования, Добавления или Дубликата */
        public int CurrentDataRow_id { get; set; }


        #region Operations
        public void FillCombobox ()
        {
            // Заполнение combobox.            
            int y = DateTime.Today.Year - 3;
            do
            {
                comboBoxYear.Items.Add(++y);
            } while (y != DateTime.Today.Year);
            comboBoxYear.SelectedItem = y;
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
            DB = new SQliteDB();
            // Дубликат - исходя из выбранных полей на форме.            

            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();
                string query = "SELECT * FROM СтоимостнаяГруппаКалГода " +
                               $"WHERE СтоимостнаяГруппаКалГода.Наименование = '{textBoxDesc.Text}' " +
                               $" AND СтоимостнаяГруппаКалГода.КалендарныйГод LIKE '{comboBoxYear.SelectedItem.ToString()}%';";

                Adapter = new SQLiteDataAdapter(query, connection);
                Table = new DataTable();
                Adapter.Fill(Table);

                return Table;
            }
        }
        void InsertRecord ()//сохранение
        {
            DB = new SQliteDB();
            string query = "SELECT * FROM СтоимостнаяГруппаКалГода";

            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();

                Adapter = new SQLiteDataAdapter(query, connection);
                Table = new DataTable();
                DataRow new_row = Table.NewRow();

                Adapter.Fill(Table);

                new_row[1] = textBoxDesc.Text;
                new_row[2] = textBoxFillDesc.Text;
                new_row[3] = textBoxComment.Text;
                new_row[4] = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01").ToString("yyyy-MM-dd");

                Table.Rows.Add(new_row);

                SQLiteCommandBuilder command = new SQLiteCommandBuilder(Adapter);                
                Adapter.Update(Table);
            }
        }
        void UpdateRecord (int id)//обновление
        {
            DB = new SQliteDB();
            string DATE = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01").ToString("yyyy-MM-dd");

            string update = "UPDATE СтоимостнаяГруппаКалГода SET " +
                            $"Наименование = '{textBoxDesc.Text}', ПолноеНаименование = '{textBoxFillDesc.Text}', " +
                            $"Комментарий = '{textBoxComment.Text}', КалендарныйГод = '{DATE}' " +
                            $"WHERE СтоимостнаяГруппаКалГода.код = {CurrentDataRow_id}; " +
                            $"UPDATE БНЗСтоимостнойГруппы SET КалендарныйГод = '{DATE}' "+
                            $"WHERE БНЗСтоимостнойГруппы.СтоимостнаяГруппаКалГода_ВК = {CurrentDataRow_id}";


            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();

                Command = new SQLiteCommand(update, connection);

                Command.ExecuteNonQuery();
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

                    if (CurrentDataRow_id == fantom_id)
                    {
                        out_id = CurrentDataRow_id;
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
                out_id = CurrentDataRow_id;
                return true;
            }
            out_id = 0;
            return false;
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
        #endregion

        void FillDataGridGroups ()
        {
            DB = new SQliteDB();
            string select = "SELECT БНЗСтоимостнойГруппы.код, " +
                            "БазовыйНормативЗатрат.Наименование as 'Базовый норматив', "+
                            "БНЗСтоимостнойГруппы.Бакалавриат, " +
                            "БНЗСтоимостнойГруппы.Магистратура, " +
                            "БНЗСтоимостнойГруппы.Аспирантура, " +
                            "БНЗСтоимостнойГруппы.СПО, " +
                            "БНЗСтоимостнойГруппы.КалендарныйГод, " +
                            "БНЗСтоимостнойГруппы.БНЗ_ВК, " +
                            "БНЗСтоимостнойГруппы.СтоимостнаяГруппаКалГода_ВК " +
                            "FROM БНЗСтоимостнойГруппы JOIN БазовыйНормативЗатрат ON БазовыйНормативЗатрат.код = БНЗСтоимостнойГруппы.БНЗ_ВК " +
                            $"WHERE БНЗСтоимостнойГруппы.СтоимостнаяГруппаКалГода_ВК = {CurrentDataRow_id} " +
                            $"AND БНЗСтоимостнойГруппы.КалендарныйГод LIKE '{comboBoxYear.SelectedItem.ToString()}%'; " +

                            //Table 1.
                            "SELECT БазовыйНормативЗатрат.код, Наименование FROM БазовыйНормативЗатрат WHERE БазовыйНормативЗатрат.код " +
                            "NOT IN(SELECT БНЗСтоимостнойГруппы.БНЗ_ВК FROM БНЗСтоимостнойГруппы WHERE " +
                            $"БНЗСтоимостнойГруппы.СтоимостнаяГруппаКалГода_ВК = {CurrentDataRow_id})";


            ds = new DataSet();
            AdapterGroup = new SQLiteDataAdapter(select, DB.ConnectionDB);
            TableGroup = new DataTable();
            AdapterGroup.Fill(ds);

            TableGroup = ds.Tables[0];

            dataGridViewБНЗ_Группы.DataSource = TableGroup;
            /*формирование внешнего вида таблицы*/

            ListNormals.Clear();
            //comboBoxNormativ.Items.Clear();
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                ListNormals.Add(new БазовыеНормативыЗатрат(Convert.ToInt32(row[0]), row[1].ToString()));
            }
            //comboBoxNormativ.Items.AddRange(ListNormals.Select(x => x.Desc).ToArray());

        }
        void DeleteSelectedRows ()
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result.Equals(DialogResult.Yes))
            {
                List<object> list_id = new List<object>();
                foreach (DataGridViewRow row in dataGridViewБНЗ_Группы.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        list_id.Add(row.Cells["код"].Value);
                    }
                }

                DB = new SQliteDB();
                using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
                {
                    connection.Open();
                    string id = string.Join(",", list_id);

                    Command = new SQLiteCommand("DELETE FROM БНЗСтоимостнойГруппы " +
                                $"WHERE БНЗСтоимостнойГруппы.код IN({id})", connection);
                    Command.ExecuteNonQuery();
                }
                FillDataGridGroups();
            }
        }


        private void buttonSaveAndClose_Click (object sender, EventArgs e)
        {
            Operation();
            Close();
        }

        private void buttonDeleteKoef_Click (object sender, EventArgs e)
        {
            DeleteSelectedRows();
        }

        private void buttonSave_Click (object sender, EventArgs e)
        {
            Operation();
        }

        private void comboBoxNormativ_SelectionChangeCommitted (object sender, EventArgs e)
        {
            ComboBox item = (ComboBox)sender;

            AdapterGroup = new SQLiteDataAdapter("select БНЗСтоимостнойГруппы.код, "+
                                                    "БНЗСтоимостнойГруппы.Бакалавриат, "+
                                                    "БНЗСтоимостнойГруппы.Магистратура, "+
                                                    "БНЗСтоимостнойГруппы.Аспирантура, "+
                                                    "БНЗСтоимостнойГруппы.СПО, "+
                                                    "БНЗСтоимостнойГруппы.КалендарныйГод, "+
                                                    "БНЗСтоимостнойГруппы.БНЗ_ВК, "+
                                                    "БНЗСтоимостнойГруппы.СтоимостнаяГруппаКалГода_ВК "+
                                                    "from БНЗСтоимостнойГруппы", DB.ConnectionDB);

            DataRow newrow = TableGroup.NewRow();

            newrow["Бакалавриат"] = 0;
            newrow["Магистратура"] = 0;
            newrow["Аспирантура"] = 0;
            newrow["СПО"] = 0;

            newrow["КалендарныйГод"] = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01");
            newrow["СтоимостнаяГруппаКалГода_ВК"] = CurrentDataRow_id;
            newrow["БНЗ_ВК"] = Convert.ToInt32(ListNormals.Where(x => x.Desc.Contains(item.SelectedItem.ToString())).Select(x => x.id).ElementAt(0));

            CommndBuilder = new SQLiteCommandBuilder(AdapterGroup);
            TableGroup.Rows.Add(newrow);
            AdapterGroup.Update(TableGroup);

            item.Items.Remove(item.SelectedItem);
            FillDataGridGroups();
        }

        private void buttonAddKoef_Click (object sender, EventArgs e)
        {
            FormListBaseGroup fb = new FormListBaseGroup();
            fb.DATE = comboBoxYear.SelectedItem.ToString();
            fb.CurrentRow = CurrentDataRow_id;
            fb.ShowDialog();
            FillDataGridGroups();
        }

        private void FormGroupAdd_FormClosing (object sender, FormClosingEventArgs e)
        {
            DB = new SQliteDB();
            using (SQLiteConnection c = new SQLiteConnection(DB.ConnectionDB))
            {
                c.Open();
                /*Проверка на удаление только по одному признаку - пустое поле наименование.
                 * Возможно, следут добавить ещё два условия для удаления. 
                 * */
                SQLiteCommand cm = new SQLiteCommand("DELETE FROM СтоимостнаяГруппаКалГода WHERE СтоимостнаяГруппаКалГода.Наименование = '';", c);
                cm.ExecuteNonQueryAsync();
            }
        }

        private void FormGroupAdd_Load (object sender, EventArgs e)
        {
            FillDataGridGroups();
        }

        private void dataGridViewБНЗ_Группы_CellEndEdit (object sender, DataGridViewCellEventArgs e)
        {
            string select = "SELECT БНЗСтоимостнойГруппы.код, " +                            
                            "БНЗСтоимостнойГруппы.Бакалавриат, " +
                            "БНЗСтоимостнойГруппы.Магистратура, " +
                            "БНЗСтоимостнойГруппы.Аспирантура, " +
                            "БНЗСтоимостнойГруппы.СПО " +
                            "FROM БНЗСтоимостнойГруппы";


            AdapterGroup = new SQLiteDataAdapter(select, DB.ConnectionDB);
            CommndBuilder = new SQLiteCommandBuilder(AdapterGroup);
            AdapterGroup.Update(TableGroup);

        }
    }
}
