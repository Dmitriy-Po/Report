using System;
using Report.Classes;
using System.Windows.Forms;
using System.Linq;
using System.Data.SQLite;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Report
{
    public partial class Form_КК_Добавление : Form
    {
        public Form_КК_Добавление ()
        {
            InitializeComponent();
        }
        public int CurrentDataRow { get; set; }
        public ushort StatusOperation { get; set; }
        public void FillElements ()
        {
            // заполнение combobox данными из табилцы Форма обучения, Даты.
            FormКоэффиценты c = new FormКоэффиценты();
            comboBoxFormEducation.Items.AddRange(c.ListEducation.Select(x => x.Desc).ToArray());
            comboBoxCorrectCoef.Items.AddRange(c.ListCoef.Select(x => x.Наименование).ToArray());

            int y = DateTime.Now.Year - 3;
            do
            {
                comboBoxYear.Items.Add(++y);
            } while (y != DateTime.Now.Year);
        }

        DataTable GetTable ()
        {
            FormКоэффиценты fk = new FormКоэффиценты();
            SQliteDB db = new SQliteDB();


            // Дубликат - исходя из выбранных полей на форме.

            var id_coef = fk.ListCoef.Where(x => x.Наименование.Contains(comboBoxCorrectCoef.SelectedItem.ToString())).Select(i => i.id);
            var id_form = fk.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.SelectedItem.ToString())).Select(i => i.id);
            string value_coef = textBoxCoeff.Text.Replace(",", ".");
            int year = Convert.ToInt32(comboBoxYear.SelectedItem);

            using (SQLiteConnection connection = new SQLiteConnection(db.ConnectionDB))
            {
                string query = "SELECT * FROM ЗначениеКоэффицента " +
                                $"WHERE ЗначениеКоэффицента.Корректирующие_ВК = {Convert.ToInt32(id_coef.ElementAt(0))} " +
                                $"AND ЗначениеКоэффицента.ФормаОбучения_ВК = {Convert.ToInt32(id_form.ElementAt(0))} " +
                                $"AND ЗначениеКоэффицента.КаледндарныйГод LIKE '{year}%' " +
                                $"AND ЗначениеКоэффицента.Значение = {value_coef};";

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
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
        bool IsCurrentRows (out int out_id)
        {
            DataTable table = GetTable();
            // Три состояния при редактировании, когда ид совпадает, когда не совпадает и когда отсувствует.
            int fantom_id = Convert.ToInt32(table.Rows[0][0]);

            if (fantom_id == CurrentDataRow)
            {
                out_id = CurrentDataRow;
                return true;
            }
            else
            {
                out_id = 0;
                return false;
            };
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
                    else MessageBox.Show("Такая строка уже существует");

                    break;
            }
        }


        string[] ValuesOfCoef()
        {
            SQliteDB db = new SQliteDB();
            FormКоэффиценты form_coef = new FormКоэффиценты();
            int[] id = new int[2];            

            #region получение id - последней записи в бд.
            SQLiteDataReader reader = db.Select_any_query("SELECT MAX(КорректирующиеКоэффиценты.код) as last_id" +
                    " FROM КорректирующиеКоэффиценты");

            while (reader.Read())
            {
                id[0] = reader.GetInt32(0);
            }
            reader.Close();
            db.ConnectionDB.Close();

            #endregion
            //запись из коллекции - формы обучения.
            id[1] = form_coef.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.Text)).Select(x => x.id).ElementAt(0);

            string[] val = { Math.Round( Convert.ToDecimal(textBoxCoeff.Text), 2 ).ToString(),
                comboBoxYear.Text +"-01-01", id[0].ToString(), id[1].ToString() };

            val[0] = val[0].Replace(",", "."); //замена символа запятой на точку

            return val;

        }

        void UpdateRecord (int id)//обновление
        {
            #region OLD
            //SQliteDB db = new SQliteDB();
            //FormКоэффиценты form_coef = new FormКоэффиценты();

            //string[] FieldsCorrCoef = { "Наименование", "ПолноеНаименование", "Комментарий", "Уточнение", "СтудентИнвалид" };
            //object[] ValuesOfCorrCoef = { textBoxDesc.Text, textBoxFullDesc.Text, textBoxComment.Text, textBoxDetail.Text, Convert.ToInt16(checkBoxStdInv.Checked) };

            //db.Update_new("КорректирующиеКоэффиценты", FieldsCorrCoef, ValuesOfCorrCoef, "WHERE код = "+ idkk.ToString());

            //string[] FieldsCoef = { "Значение", "КаледндарныйГод", "Корректирующие_ВК", "ФормаОбучения_ВК" };

            //int id_formEducation = form_coef.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.Text)).Select(x => x.id).ElementAt(0);
            //string[] ValuesOfCoeff = { ValuesOfCoef()[0], ValuesOfCoef()[1], id.ToString(),  id_formEducation.ToString()};

            //db.Update_new("ЗначениеКоэффицента", FieldsCoef, ValuesOfCoeff, "WHERE Корректирующие_ВК = " + id.ToString() + " AND ФормаОбучения_ВК = " + id_formEducation.ToString());
            #endregion
            
            SQliteDB database = new SQliteDB();
            FormКоэффиценты fcoef = new FormКоэффиценты();
            string query = "SELECT * FROM ЗначениеКоэффицента";

            using (SQLiteConnection connection = new SQLiteConnection(database.ConnectionDB))
            {
                connection.Open();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter);
                DataTable table = new DataTable();

                adapter.InsertCommand = new SQLiteCommand(connection);
                adapter.Fill(table);

                var row = from r in table.AsEnumerable()
                          where Convert.ToInt32(r["код"]) == id
                          select r;

                
                var id_coef = fcoef.ListCoef.Where(x => x.Наименование.Contains(comboBoxCorrectCoef.SelectedItem.ToString())).Select(i => i.id);
                var id_form = fcoef.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.SelectedItem.ToString())).Select(i => i.id);
                decimal value_coef = Convert.ToDecimal(textBoxCoeff.Text.Replace('.', ','));

                DataRow new_row = row.FirstOrDefault();

                new_row[1] = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01");
                new_row[2] = Convert.ToInt32(id_coef.ElementAt(0));
                new_row[3] = Convert.ToInt32(id_form.ElementAt(0));
                new_row[4] = value_coef;


                table.AcceptChanges();
                adapter.Update(table);
                command.Dispose();
            };

        }
        void InsertRecord ()//сохранение
        {
            #region OLD
            // Функция добавляет запись в базу данных.
            //SQliteDB db = new SQliteDB();

            //string[] ColumnsCorrCoef = { "Наименование", "ПолноеНаименование", "Комментарий", "Уточнение", "СтудентИнвалид" };
            //object[] ValuesOfCorrCoef = { textBoxDesc.Text, textBoxFullDesc.Text, textBoxComment.Text, textBoxDetail.Text, Convert.ToInt16(checkBoxStdInv.Checked) };

            //db.Insert_New("КорректирующиеКоэффиценты",
            //    string.Join(", ", ColumnsCorrCoef),
            //    string.Join("', '", ValuesOfCorrCoef));

            //string[] ColumnsValueCoef = { "Значение", "КаледндарныйГод", "Корректирующие_ВК", "ФормаОбучения_ВК" };
            //string[] ValueCoef = ValuesOfCoef();
            ////delete FROM КорректирующиеКоэффиценты WHERE NOT EXISTS
            ////(select ЗначениеКоэффицента.код FROM ЗначениеКоэффицента WHERE ЗначениеКоэффицента.Корректирующие_ВК = КорректирующиеКоэффиценты.код)
            //db.Insert_New("ЗначениеКоэффицента",
            //    string.Join(", ", ColumnsValueCoef),
            //    string.Join("', '", ValueCoef));
            #endregion
            SQliteDB database = new SQliteDB();
            FormКоэффиценты fcoef = new FormКоэффиценты();
            string query = "SELECT * FROM ЗначениеКоэффицента";

            using (SQLiteConnection connection = new SQLiteConnection(database.ConnectionDB))
            {
                connection.Open();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable table = new DataTable();
                DataRow new_row = table.NewRow();

                adapter.Fill(table);

                var id_coef = fcoef.ListCoef.Where(x => x.Наименование.Contains(comboBoxCorrectCoef.SelectedItem.ToString())).Select(i => i.id);
                var id_form = fcoef.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.SelectedItem.ToString())).Select(i => i.id);
                decimal value_coef = Convert.ToDecimal(textBoxCoeff.Text.Replace('.', ','));


                new_row[1] = Convert.ToDateTime(comboBoxYear.SelectedItem+"-01-01");
                new_row[2] = Convert.ToInt32(id_coef.ElementAt(0));
                new_row[3] = Convert.ToInt32(id_form.ElementAt(0));
                new_row[4] = value_coef;

                SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter);

                table.Rows.Add(new_row);
                adapter.Update(table);
                command.Dispose();
            };
        }
        //void IsCurrentRows (out int id, out int id_kk)
        //{
        //FormКоэффиценты fk = new FormКоэффиценты();

            //Редактируемая строка, взятая из коллекции ListCoef.
            //var DuplicateId = fk.ListCoef.Where(
            //    x => x.Наименование.Equals(textBoxDesc.Text, StringComparison.OrdinalIgnoreCase) &&
            //         x.GetForm().Equals(comboBoxFormEducation.Text, StringComparison.OrdinalIgnoreCase) &&
            //         x.СтудентИнвалид.Equals(checkBoxStdInv.Checked) &&
            //         x.GetYear().Substring(6, 4).Equals(comboBoxYear.Text) &&
            //         x.GetCoef() == Math.Round(Convert.ToDecimal(textBoxCoeff.Text), 2))
            //         .Select(x => x.GetIdCoeff());


            // Id - выбранной строки в DataGrid.
            //int row_id = Convert.ToInt32(CurrentDataRow.Cells["id"].Value);
            //int row_id_kk = Convert.ToInt32(CurrentDataRow.Cells["id_kk"].Value);

            /*Если выбранная (из DataGrid) редактируемая строка совпадает, по параметрам 
             * <указанным в поях формы>, с строкой в коллекции  ListCoef, тогда 
                    - это текущая редактиремая строка и сохраненеи возможно.
                Строки в DataGrid и ListCoef сравниваются по полю id.    

             * Фокус в том, что при нажатии кнопки Редактировать, переменная DataGrid получает, из столбца 'id'
             * идентификатор строки (записи в бд). А при нажати кнопки Создать по шаблону, переменная DataGrid
             * идентификатор не получает. Вот и вся магия :) 
             * */

            //try
            //{
            //    if (row_id == DuplicateId.ElementAt(0)) // True - совпадение текущей редактируемой строки.
            //    {
            //        id = row_id;
            //        id_kk = row_id_kk;
            //        return true;
            //    }
            //    else // Flase - при редактированиия получилось набрать дубликат, исходя из значений полей на форме.
            //    {
            //        id = 0;
            //        id_kk = 0;
            //        return false;
            //    };
            //}
            //catch (ArgumentOutOfRangeException) // Случай, когда не найдено дубликатов. Коллекция будет пуста.
            //{
            //    id_kk = row_id_kk;
            //    id = row_id;
            //    return true;
            //}
            
        //}

        private void Form_КК_Добавление_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonSave_Click (object sender, EventArgs e)
        {
            Operation();
        }

        private void buttonSaveAndClose_Click (object sender, EventArgs e)
        {
            Operation();
            Close();
        }
    }
}
