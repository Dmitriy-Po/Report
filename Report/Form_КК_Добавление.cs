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
            DataTable table;
            SQLiteDataAdapter adapter;
            // Дубликат - исходя из выбранных полей на форме.

            var id_coef = fk.ListCoef.Where(x => x.Наименование.Contains(textBoxNameCoef.Text)).Select(i => i.id);
            var id_form = fk.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.SelectedItem.ToString())).Select(i => i.id);
            string value_coef = textBoxCoeff.Text.Replace(",", ".");
            int year = Convert.ToInt32(comboBoxYear.SelectedItem);

            if (id_coef.Count() != 0)
            {
                using (SQLiteConnection connection = new SQLiteConnection(db.ConnectionDB))
                {
                    string query = "SELECT * FROM ЗначениеКоэффицента " +
                                    $"WHERE ЗначениеКоэффицента.Корректирующие_ВК = {Convert.ToInt32(id_coef.ElementAt(0))} " +
                                    $"AND ЗначениеКоэффицента.ФормаОбучения_ВК = {Convert.ToInt32(id_form.ElementAt(0))} " +
                                    $"AND ЗначениеКоэффицента.КаледндарныйГод LIKE '{year}%' " +
                                    $"AND ЗначениеКоэффицента.Значение = {value_coef};";

                    adapter = new SQLiteDataAdapter(query, connection);
                    table = new DataTable();
                    adapter.Fill(table);

                    return table;
                } 
            }
            else
            {
                return table = new DataTable();
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

            //DataTable table = new DataTable();
            //SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);                

            //adapter.Fill(table);

            //var row = from r in table.AsEnumerable()
            //          where Convert.ToInt32(r["код"]) == id
            //          select r;

            //DataRow new_row = row.FirstOrDefault();
            #endregion

            SQliteDB database = new SQliteDB();
            FormКоэффиценты fcoef = new FormКоэффиценты();            

            using (SQLiteConnection connection = new SQLiteConnection(database.ConnectionDB))
            {
                connection.Open();                

                var id_coef = fcoef.ListCoef.Where(x => x.Наименование.Contains(textBoxNameCoef.Text)).Select(i => i.id);
                var id_form = fcoef.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.SelectedItem.ToString())).Select(i => i.id);
                decimal value_coef = Convert.ToDecimal(textBoxCoeff.Text.Replace('.', ','));

                string DATE = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01").ToString("yyyy-MM-dd");                

                SQLiteCommand command = new SQLiteCommand("UPDATE ЗначениеКоэффицента SET "+
                                $"Значение = {textBoxCoeff.Text.Replace(",", ".")}, КаледндарныйГод = '{DATE}', "+
                                    $"Корректирующие_ВК = {Convert.ToInt32(id_coef.ElementAt(0))}, ФормаОбучения_ВК = {Convert.ToInt32(id_form.ElementAt(0))} " +
                                    $"WHERE ЗначениеКоэффицента.код = {id}", connection);


                command.ExecuteNonQuery();                

            }

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
            string query_KK = "SELECT * FROM КорректирующиеКоэффиценты";            

            using (SQLiteConnection connection = new SQLiteConnection(database.ConnectionDB))
            {
                connection.Open();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query_KK, connection);
                

                DataTable table = new DataTable();
                DataRow new_row = table.NewRow();

                adapter.Fill(table);

                //var id_coef = fcoef.ListCoef.Where(x => x.Наименование.Contains(comboBoxCorrectCoef.SelectedItem.ToString())).Select(i => i.id);
                var id_form = fcoef.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.SelectedItem.ToString())).Select(i => i.id);
                decimal value_coef = Convert.ToDecimal(textBoxCoeff.Text);
                string DATE = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01").ToString("yyyy-MM-dd");

                new_row[1] = textBoxNameCoef.Text;
                new_row[2] = textBoxFullDesc.Text;
                new_row[3] = textBoxDetail.Text;
                new_row[4] = textBoxComment.Text;
                new_row[5] = checkBoxStdInv.Checked;               


                SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter);

                table.Rows.Add(new_row);                
                adapter.Update(table);


                /*-------------------*/
                SQLiteCommand new_command = new SQLiteCommand("INSERT INTO ЗначениеКоэффицента(Значение, КаледндарныйГод, Корректирующие_ВК, ФормаОбучения_ВК) "+
                                                    $"VALUES({textBoxCoeff.Text.Replace(",", ".")}, '{DATE}', "+
                                                    $"((SELECT MAX(КорректирующиеКоэффиценты.код) FROM КорректирующиеКоэффиценты)), {id_form.ElementAt(0)})", connection);
                new_command.ExecuteNonQuery();
                               
                
            }
        }

        private void Form_КК_Добавление_Load(object sender, EventArgs e)
        {
            
        }
        bool IsCorrect ()
        {
            try
            {
                decimal k = Convert.ToDecimal(textBoxCoeff.Text);
                if (k >= 0)
                {
                    return true;
                }
                MessageBox.Show("Значение должно быть больше или равно нулю.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;                
            }
            catch (Exception)
            {
                MessageBox.Show("Введено не числовое значние. Введите число.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        bool IsRequired ()
        {
            int f, y = 0;
            f = comboBoxFormEducation.SelectedIndex;
            y = comboBoxYear.SelectedIndex;

            if ((!string.IsNullOrEmpty(textBoxNameCoef.Text)) && (f >= 0) && (y >=0))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void buttonSave_Click (object sender, EventArgs e)
        {
            if (IsCorrect() && IsRequired())
            {
                Operation();  
            }          
        }

        private void buttonSaveAndClose_Click (object sender, EventArgs e)
        {
            if (IsCorrect() && IsRequired())
            {
                Operation();
                Close(); 
            }
        }
    }
}
