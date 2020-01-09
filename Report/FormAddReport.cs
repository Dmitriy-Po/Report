using Report;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Linq;
using System.Data;

namespace Report
{
    public partial class FormAddReport : Form
    {
        DataSet ds;
        SQliteDB DB;
        DataTable Table, TableGroup;
        SQLiteDataAdapter Adapter, AdapterGroup;
        SQLiteCommand Command;
        SQLiteCommandBuilder CommndBuilder;


        public int GetCurrentrow_ID { get; set; }
        public int StatusOperation { get; set; }


        public FormAddReport()
        {
            InitializeComponent();
        }
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
            DB = new SQliteDB();
            FormListCountStudent ListCount = new FormListCountStudent();
            // Дубликат - исходя из выбранных полей на форме.            

            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();

                int id_filial = ListCount.ListFilial.Where(x => x.full_desc.Contains(comboBoxFilial.SelectedItem.ToString())).Select(x => x.id).ElementAt(0);
                int id_spec = ListCount.ListSpecial.Where(x => x.desc.Contains(comboBoxSpecial.SelectedItem.ToString())).Select(x => x.id).ElementAt(0);
                int id_skill = ListCount.ListSkill.Where(x => x.desc.Contains(comboBoxSkill.SelectedItem.ToString())).Select(x => x.id).ElementAt(0);
                int std_int = Convert.ToInt16(checkBoxStdInv.Checked);

                string query = "SELECT * FROM ЧисленностьОбучающихся "+
                                    $"WHERE ЧисленностьОбучающихся.стуктурное_подразделение_ВК = {id_filial} "+
                                    $"AND ЧисленностьОбучающихся.специальность_ВК = {id_spec} "+
                                    $"AND ЧисленностьОбучающихся.квалификация_ВК = {id_skill} "+
                                    $"AND ЧисленностьОбучающихся.год LIKE '{comboBoxYear.SelectedItem.ToString()}%' "+
                                    $"AND ЧисленностьОбучающихся.студент_инвалид = '{std_int}'; ";

                Adapter = new SQLiteDataAdapter(query, connection);
                Table = new DataTable();
                Adapter.Fill(Table);

                return Table;
            }
        }
        void InsertRecord ()//сохранение
        {
            DB = new SQliteDB();
            FormListCountStudent ListCount = new FormListCountStudent();
            string query = "SELECT * FROM ЧисленностьОбучающихся";

            int id_filial   = ListCount.ListFilial.Where(x => x.full_desc.Contains(comboBoxFilial.SelectedItem.ToString())).Select(x => x.id).ElementAt(0);
            int id_spec     = ListCount.ListSpecial.Where(x => x.desc.Contains(comboBoxSpecial.SelectedItem.ToString())).Select(x => x.id).ElementAt(0);
            int id_skill    = ListCount.ListSkill.Where(x => x.desc.Contains(comboBoxSkill.SelectedItem.ToString())).Select(x => x.id).ElementAt(0);


            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();

                Adapter = new SQLiteDataAdapter(query, connection);
                Table = new DataTable();
                DataRow new_row = Table.NewRow();

                Adapter.Fill(Table);

                new_row[1] = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01").ToString("yyyy-MM-dd");
                new_row[2] = textBoxОчное.Text;
                new_row[3] = textBoxОчно_заочное.Text;
                new_row[4] = textBoxЗаочное.Text;
                new_row[5] = checkBoxStdInv.Checked;
                new_row[6] = id_filial;
                new_row[7] = id_spec;
                new_row[8] = id_skill;

                SQLiteCommandBuilder command = new SQLiteCommandBuilder(Adapter);

                Table.Rows.Add(new_row);
                Adapter.Update(Table);
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

                    if (GetCurrentrow_ID == fantom_id)
                    {
                        out_id = GetCurrentrow_ID;
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
                out_id = GetCurrentrow_ID;
                return true;
            }
            out_id = 0;
            return false;
        }
        void UpdateRecord (int id)//обновление
        {
            FormListCountStudent ListCount = new FormListCountStudent();
            DB = new SQliteDB();

            string DATE     = Convert.ToDateTime(comboBoxYear.SelectedItem + "-01-01").ToString("yyyy-MM-dd");
            int id_filial   = ListCount.ListFilial.Where(x => x.full_desc.Contains(comboBoxFilial.SelectedItem.ToString())).Select(x => x.id).ElementAt(0);
            int id_spec     = ListCount.ListSpecial.Where(x => x.desc.Contains(comboBoxSpecial.SelectedItem.ToString())).Select(x => x.id).ElementAt(0);
            int id_skill    = ListCount.ListSkill.Where(x => x.desc.Contains(comboBoxSkill.SelectedItem.ToString())).Select(x => x.id).ElementAt(0);
            
            string update = "UPDATE ЧисленностьОбучающихся SET "+
                            $"год = '{DATE}', "+
                            $"очное = {textBoxОчное.Text}, "+
                            $"очно_заочное = {textBoxОчно_заочное.Text}, "+
                            $"заочное = {textBoxЗаочное.Text}, "+
                            $"студент_инвалид = {checkBoxStdInv.Checked}, "+
                            $"стуктурное_подразделение_ВК = {id_filial}, "+
                            $"специальность_ВК = {id_spec}, "+
                            $"квалификация_ВК = {id_skill} "+
                            $"WHERE ЧисленностьОбучающихся.код = {id}";

            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();
                Command = new SQLiteCommand(update, connection);
                Command.ExecuteNonQuery();
            }
        }


        #region Проверка ввода данных
        public void NO_Dublicate_ForUpdateRecord(/*List<TableCountStudent> editable_row*/)
        {
            //Функция для проверки на дубликаты при редактировании строки
            // - чтобы при редактировании не было возможности создать будликат записи.
            //Требуется сверять значения, каждый раз при сохранении редактиуемой строки,
            //в полях на форме с записями в коллекции ListCountStudent
            //FormListCountStudent fa = new FormListCountStudent();
            
            ////поиск дубликатов
            //var duplicate = fa.ListCountStudent
            //     .Where(x => x.Filial.Contains(comboBoxFilial.SelectedItem.ToString()) &&
            //     x.Special.Contains(comboBoxSpecial.SelectedItem.ToString()) &&
            //     x.year == Convert.ToInt32(textBoxYear.Text) &&
            //     x.student_inv.Equals(checkBoxStdInv.Checked)).Select(x => x.id);

            ////если переменная duplicate совпадает с редактируемой строкой, 
            ////тогда это текущая редактируемая строка, и сохранение возможно.
            //try
            //{
            //    if (editable_row[0].id == duplicate.ElementAt(0))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Такая строка уже существует");
            //        return false;
            //    }
            //}
            //catch (ArgumentOutOfRangeException)
            //{
            //    return true;
            //}

        }
        public void IsMatch() //функция проверяет наличие дубликатов при добавлении новой записи
        {
            //локальная переменная для определения корректного ввода ГОДА
            int local_year = 0;
            FormListCountStudent fa = new FormListCountStudent();
            /*
             * проверяем условие на совпадение строк со datagridview
             * если есть совпадение - значит строка уже существует и текущая запись дубликат
             * иначе выполнить сохранение. 
             * */
            //try
            //{
            //    if (int.TryParse(textBoxYear.Text, out local_year))
            //    {
            //        var is_match = fa.ListCountStudent
            //     .Select(
            //             x => x.Filial.Contains(fa.ListFilial[comboBoxFilial.SelectedIndex].full_desc) &&
            //             x.Special.Contains(fa.ListSpecial[comboBoxSpecial.SelectedIndex].desc) &&
            //             x.year == local_year &&
            //             x.student_inv.Equals(checkBoxStdInv.Checked)

            //      ).Any(x => x.Equals(true));
                                        
            //        if (!is_match)//если нет совпадений
            //        {
            //            //тогда сохранение                                               
            //            return true;
            //        }
            //        //иначе совпадения есть
            //        else
            //        {
            //            MessageBox.Show("Такая строка уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Заполните поле Календарный год", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //    }
            //}
            //catch (ArgumentOutOfRangeException)
            //{
            //    MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
            //    return false;
            //}            
        } 
        public bool IsCorrect()
        {
            try
            {
                Convert.ToInt32(textBoxОчное.Text);
                Convert.ToInt32(textBoxЗаочное.Text);
                Convert.ToInt32(textBoxОчно_заочное.Text);                
                return true;                
            }
            catch (FormatException)
            {
                MessageBox.Show("В полях <количество>, должно быть указанно целое положительное число или ноль.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

        }
        bool IsRequired()
        {
            int fil, spec, skill = 0;

            fil     = comboBoxFilial.SelectedIndex;
            spec    = comboBoxSpecial.SelectedIndex;
            skill   = comboBoxSkill.SelectedIndex;

            if ((fil >= 0) && (spec >= 0) && (skill >= 0))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }
        #endregion
        #region Функции сохранения и удаления
        public void save()
        {
            /* 
             * Кнопка для сохранения записи в таблицу ЧисленностьОбучающихся.
               Метод передаёт на форму FormListCountStudent 
                индексы выбранных полей, в выпадающих списках.
               По индексам выбирается элемент в коллекции. 
             */
            //if (comboBoxSkill.SelectedIndex >= 0)
            //{
            //    try
            //    {
            //        FormListCountStudent FormStudent = new FormListCountStudent();
            //        FormStudent.SaveStudent(new int[] {
            //        comboBoxFilial.SelectedIndex,
            //        comboBoxSpecial.SelectedIndex,
            //        comboBoxSkill.SelectedIndex,
            //        Convert.ToInt32(textBoxОчное.Text),
            //        Convert.ToInt32(textBoxОчно_заочное.Text),
            //        Convert.ToInt32(textBoxЗаочное.Text),
            //        Convert.ToInt32(textBoxYear.Text),
            //        Convert.ToInt32(checkBoxStdInv.Checked)
            //    });
            //        return true;
            //    }
            //    catch (FormatException)
            //    {
            //        MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //    }
            //    catch (ArgumentOutOfRangeException)
            //    {
            //        MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
        }
        public void update(TableCountStudent info)
        {
            //update set
            //SQliteDB db = new SQliteDB();
            //FormListCountStudent fa = new FormListCountStudent();

            ////в момент обновления (сохранения), берутся текущие данные из выпадающий списков
            //info.Filial     = fa.ListFilial[comboBoxFilial.SelectedIndex].full_desc;
            //info.Special    = fa.ListSpecial[comboBoxSpecial.SelectedIndex].desc;
            //info.Skill      = fa.ListSkill[comboBoxSkill.SelectedIndex].desc;
            //info.ochnoe     = Convert.ToInt32(textBoxОчное.Text);
            //info.zaochnoe   = Convert.ToInt32(textBoxЗаочное.Text);
            //info.ochno_zaocjnoe = Convert.ToInt32(textBoxОчно_заочное.Text);
            //info.year           = Convert.ToInt32(textBoxYear.Text);
            //info.student_inv    = checkBoxStdInv.Checked;

            //db.Update_TableCountStudent(info);

            //db.Update(new int[] {
            //    comboBoxFilial.SelectedIndex,
            //    comboBoxSpecial.SelectedIndex,
            //    comboBoxSkill.SelectedIndex,
            //    Convert.ToInt32(textBoxОчное.Text),
            //    Convert.ToInt32(textBoxОчно_заочное.Text),
            //    Convert.ToInt32(textBoxЗаочное.Text),
            //    Convert.ToInt32(textBoxYear.Text),
            //    Convert.ToInt32(checkBoxStdInv.Checked),
            //    id               
            //});

        }
        #endregion
        #region clearcomboboxes
        private void button2_Click(object sender, EventArgs e)
        {
            comboBoxFilial.ResetText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBoxSpecial.ResetText();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBoxSkill.ResetText();
        }
        #endregion
        
        public void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsCorrect() && IsRequired())
            {
                Operation();
            }
            
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (IsCorrect() && IsRequired())
            {
                Operation();
                Close(); 
            }

            #region OLD
            //сохранить и закрыть форму        
            //FormListCountStudent FormStudent = new FormListCountStudent();
            

            ////поиск в коллекции, посик поля с идентификатором записи
            //var ID_current_row = FormStudent.ListCountStudent
            //        .Where(x => x.id == GetCurrentrow_ID)
            //        .Select(x => x).ToList();

            //try
            //{
            //    if (ID_current_row.Count > 0)
            //    {
            //        if (NO_Dublicate_ForUpdateRecord(ID_current_row))
            //        {
            //            update(ID_current_row[0]);
            //            Close();
            //        }
            //    }
            //    else
            //    {
            //        if (IsMatch())
            //        {
            //            if (save())
            //            {
            //                Close();
            //            }  
            //        }
            //    }
            //}
            //catch (ArgumentOutOfRangeException)
            //{
            //    return;
            //}
            #endregion
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
