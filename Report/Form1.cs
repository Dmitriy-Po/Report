using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Report.Forms;
using System.Data;
using System.IO;

namespace Report
{
    public partial class FormListCountStudent : System.Windows.Forms.Form
    {
        public FormListCountStudent ()
        {
            InitializeComponent();
            //заполнение коллекций
            TableFilial.Fill(ListFilial);
            TableSKill.Fill(ListSkill);
            TableSpecial.Fill(ListSpecial);

            /*---------------------------------------------------------*/
            comboBoxFil.SelectedIndexChanged += (s, e) => DataFilter();
            comboBoxSpec.SelectedIndexChanged += (s, e) => DataFilter();
            comboBoxYear.SelectedIndexChanged += (s, e) => DataFilter();
            checkBoxStudent_inv.Click += (s, e) => DataFilter();
        }

        SQliteDB DB;
        DataTable Table;
        SQLiteDataAdapter Adapter;
        SQLiteCommandBuilder Command;

        public List<TableFilial>  ListFilial = new List<TableFilial>();
        public List<TableSKill>   ListSkill = new List<TableSKill>();
        public List<TableSpecial> ListSpecial = new List<TableSpecial>();
        public List<TableCountStudent> ListCountStudent = new List<TableCountStudent>();
         
        int CurrentRow { get; set; }
        public void FillComboBoxes ()
        {
            SqlMyDataReader("Filial", 2, comboBoxFil);            
            SqlMyDataReader("Специальности", 1, comboBoxSpec);
            comboBoxFil.Items.Insert(0, "Все подразделения");
            comboBoxSpec.Items.Insert(0, "Все специальности");
            comboBoxYear.Items.Insert(0, "Все года");

            comboBoxFil.SelectedIndex = 0;
            comboBoxSpec.SelectedIndex = 0;
            comboBoxYear.SelectedIndex = 0;
           
            // Заполнение comboboxYear.            
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
                foreach (DataGridViewRow row in dataGridViewMain.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        list_id.Add(row.Cells["код"].Value);
                    }
                }

                DB = new SQliteDB();
                using (SQLiteConnection conect = new SQLiteConnection(DB.ConnectionDB))
                {
                    conect.Open();
                    string id = string.Join(",", list_id);
                    SQLiteCommand comand = new SQLiteCommand("DELETE FROM ЧисленностьОбучающихся WHERE "+
                                    $"ЧисленностьОбучающихся.код IN({id})", conect);
                    comand.ExecuteNonQuery();
                }
                FillDataGrid();

            }
        }
        void FillComponents (bool IsEditingMode)
        {
            // Заполение компонентов на форме из datagrid.
            FormAddReport fadd = new FormAddReport();
            fadd.FillCombobox();
            SqlMyDataReader("Filial", 2, fadd.comboBoxFilial);
            SqlMyDataReader("Квалификации", 1, fadd.comboBoxSkill);
            SqlMyDataReader("Специальности", 1, fadd.comboBoxSpecial);

            foreach (DataGridViewRow row in dataGridViewMain.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    fadd.comboBoxFilial.SelectedItem      = row.Cells["Подразделение"].Value;
                    fadd.comboBoxSpecial.SelectedItem     = row.Cells["Специальность"].Value;
                    fadd.comboBoxSkill.SelectedItem       = row.Cells["Квалификация"].Value;
                    fadd.comboBoxYear.SelectedItem        = Convert.ToInt32(row.Cells["Год"].Value);
                    
                    fadd.textBoxОчное.Text          = row.Cells["Очное"].Value.ToString();
                    fadd.textBoxОчно_заочное.Text   = row.Cells["Очно_заочное"].Value.ToString();
                    fadd.textBoxЗаочное.Text        = row.Cells["Заочное"].Value.ToString();
                    fadd.checkBoxStdInv.Checked = Convert.ToBoolean(row.Cells["Студент_инвалид"].Value);

                    if (IsEditingMode)
                    {
                        fadd.Text = "Редактирование норматива";
                        fadd.StatusOperation = 3;
                        fadd.GetCurrentrow_ID = Convert.ToInt32(row.Cells["код"].Value);    // получение строки для использования в функции IsDuplicate.
                    }
                    else
                    {
                        fadd.Text = "Создание по шаблону";
                        fadd.StatusOperation = 2;
                        fadd.GetCurrentrow_ID = 0;    // 0 - для режима Создать по шаблону.
                    }
                    CurrentRow = row.Index;
                }
            }

            fadd.ShowDialog();
            FillDataGrid();
            dataGridViewMain.Rows[CurrentRow].Cells[0].Value = true;
            dataGridViewMain.Rows[CurrentRow].Selected = true;
        }

        public void DataFilter ()
        {
            try
            {
                var newlist = from result in ListCountStudent                              
                              select new
                              {
                                  код = result.id_filial,
                                  Год = result.year,
                                  Подразделение = result.Filial,
                                  Специальность = result.Special,
                                  Квалификация = result.Skill,
                                  Очное = result.ochnoe,
                                  Очно_заочное = result.ochno_zaocjnoe,
                                  Заочное = result.zaochnoe,
                                  Студент_инвалид = result.student_inv                                  
                              };


                if (comboBoxYear.SelectedIndex > 0)
                {

                    newlist = from result in newlist
                              where result.Год == Convert.ToInt32(comboBoxYear.SelectedItem)
                              select result;                              

                }

                if (comboBoxFil.SelectedIndex > 0)
                {
                    newlist = from result in newlist
                              where result.Подразделение.Contains(comboBoxFil.SelectedItem.ToString())
                              select result;
                }

                if (comboBoxSpec.SelectedIndex > 0)
                {
                    newlist = from result in newlist
                              where result.Специальность.Contains(comboBoxSpec.SelectedItem.ToString())
                              select result;
                }
                if (checkBoxStudent_inv.Checked)
                {
                    newlist = from result in newlist
                              where result.Студент_инвалид.Equals(checkBoxStudent_inv.Checked)
                              select result;
                }

                //var newlist = ListCountStudent.FindAll(x => x.Filial.Contains(comboBoxFil.SelectedItem.ToString()));
                //newlist.FindAll(x => x.Special.Contains(comboBoxSpec.SelectedItem.ToString()));

                
                dataGridViewMain.DataSource = newlist.ToArray();
                dataGridViewMain.ColumnHeadersHeight = 28;

                dataGridViewMain.Columns["код"].Visible = false;
                dataGridViewMain.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewMain.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            }
            catch (FormatException)
            {
                return;
            }
        }
        public bool CountSelectedRows (string tooltip)
        {
            UInt32 count = 0;            
            foreach (DataGridViewRow row in dataGridViewMain.Rows)
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

       
        public void FillDataGrid()
        {
            #region OLD
            //возможно стоит использовать datasource для получекния данных
            //но пока действует по выборе из базы данных
            //string query = " SELECT " +
            //"ЧисленностьОбучающихся.год as 'Год', " +
            //"Filial.id as 'Структурное код', "+
            //"Filial.full_desc as 'Филиал', " +
            //"Специальности.наименование as 'Специальность', " +
            //"Квалификации.наименование as 'Квалификация', " +
            //"ЧисленностьОбучающихся.очное as 'Очное', " +
            //"ЧисленностьОбучающихся.очно_заочное as 'Очно-заочное', " +
            //"ЧисленностьОбучающихся.заочное as 'Заочное', " +
            //"ЧисленностьОбучающихся.студент_инвалид as 'Студент инвалид', " +
            //"ЧисленностьОбучающихся.код as 'id' "+            
            //"FROM ЧисленностьОбучающихся " +
            //"INNER JOIN Filial ON " +
            //"ЧисленностьОбучающихся.стуктурное_подразделение_ВК = Filial.id " +
            //"INNER JOIN Специальности ON " +
            //"ЧисленностьОбучающихся.специальность_ВК = Специальности.код " +
            //"INNER JOIN Квалификации ON " +
            //"ЧисленностьОбучающихся.квалификация_ВК = Квалификации.код";


            
            //SQliteDB db = new SQliteDB();
                        
            //using (SQLiteConnection Connection = new SQLiteConnection(db.ConnectionDB))
            //{
            //    Connection.Open();
            //    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, Connection);
            //    DataSet ds = new DataSet();
            //    adapter.Fill(ds);

            //    dataGridViewMain.DataSource = ds.Tables[0];
            //    dataGridViewMain.Columns[2].Visible = false;
            //    dataGridViewMain.Columns[10].Visible = false;

            //    ListCountStudent.Clear();
            //    foreach (DataRow row in ds.Tables[0].AsEnumerable())
            //    {
            //        ListCountStudent.Add(new TableCountStudent(
            //            Convert.ToInt32(row[1]),
            //            Convert.ToString(row[2]),
            //            Convert.ToString(row[3]),
            //            Convert.ToString(row[4]),

            //            Convert.ToInt32(row[5]),
            //            Convert.ToInt32(row[6]),
            //            Convert.ToInt32(row[7]),
            //            Convert.ToInt32(row[0])));
            //    }               
            //}
            
            #endregion
            string query = " SELECT " +
            "SUBSTR(ЧисленностьОбучающихся.год, 1, 4) as 'Год', " +
            "Filial.id as 'Структурное код', " +
            "Filial.full_desc as 'Подразделение', " +
            "Специальности.наименование as 'Специальность', " +
            "Квалификации.наименование as 'Квалификация', " +
            "ЧисленностьОбучающихся.очное as 'Очное', " +
            "ЧисленностьОбучающихся.очно_заочное as 'Очно_заочное', " +
            "ЧисленностьОбучающихся.заочное as 'Заочное', " +
            "ЧисленностьОбучающихся.студент_инвалид as 'Студент_инвалид', " +
            "ЧисленностьОбучающихся.код as 'код' " +
            "FROM ЧисленностьОбучающихся " +
            "INNER JOIN Filial ON " +
            "ЧисленностьОбучающихся.стуктурное_подразделение_ВК = Filial.id " +
            "INNER JOIN Специальности ON " +
            "ЧисленностьОбучающихся.специальность_ВК = Специальности.код " +
            "INNER JOIN Квалификации ON " +
            "ЧисленностьОбучающихся.квалификация_ВК = Квалификации.код";

            DB = new SQliteDB();
            SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB);
            connection.Open();

            Adapter = new SQLiteDataAdapter(query, connection);
            Table = new DataTable();
            Adapter.Fill(Table);

            dataGridViewMain.DataSource = null;
            dataGridViewMain.DataSource = Table;
            dataGridViewMain.Columns[2].Visible = false;
            dataGridViewMain.Columns[10].Visible = false;

            dataGridViewMain.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewMain.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            ListCountStudent.Clear();
            foreach (DataRow row in Table.Rows)
            {
                ListCountStudent.Add(new TableCountStudent(
                        Convert.ToInt32(row["код"]), row["Подразделение"].ToString(), row["Специальность"].ToString(),
                        row["Квалификация"].ToString(), Convert.ToInt32(row["Очное"]), 
                        Convert.ToInt32(row["Очно_заочное"]), Convert.ToInt32(row["Заочное"]), Convert.ToInt32(row["Год"]), Convert.ToBoolean(row["Студент_инвалид"])));
            }

            connection.Close();
            try
            {
                dataGridViewMain.Rows[0].Selected = false;
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        public void SaveStudent(int[] column)
        {
            /*
             * Кнопка сохранения записи в таблицу ЧисленностьОбучающихся
             * 
             * */            
            SQliteDB mydb = new SQliteDB();
            mydb.Insert("ЧисленностьОбучающихся", 
                new int[] {
                    ListFilial[column[0]].id,
                    ListSpecial[column[1]].id,
                    ListSkill[column[2]].id,
                    column[3], column[4], column[5], column[6], column[7]
                    
                });
            
        }
        
        public void SqlMyDataReader (string name_table, int num_col, ComboBox box)
        {
            //заполнение combobox
            SQliteDB sql = new SQliteDB();
            SQLiteDataReader r = sql.Select(name_table);
            
            while (r.Read())
            {                
                box.Items.Add(r[num_col]);                        
            }           
            r.Close();
           
        }
        
        
        #region buttons
        private void exitToolStripMenuItem_Click (object sender, EventArgs e)
        {
            Close();
        }

        private void fileToolStripMenuItem_Click (object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick (object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter (object sender, EventArgs e)
        {

        }

        private void button1_MouseHover (object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(buttonAddNewString, "Создать новую строку");
        }

        private void button2_Click (object sender, EventArgs e)
        {
            if (CountSelectedRows("Добавить по шаблону"))
            {
                FillComponents(false);
            }

            #region OLD
            //if (CountSelectedRows("Создать по шаблону"))
            //{
            //    FormAddReport form = new FormAddReport();
            //    SqlMyDataReader("Filial", 2, form.comboBoxFilial);
            //    SqlMyDataReader("Квалификации", 1, form.comboBoxSkill);
            //    SqlMyDataReader("Специальности", 1, form.comboBoxSpecial);

            //    foreach (DataGridViewRow row in dataGridViewMain.Rows)
            //    {
            //        if (Convert.ToBoolean(row.Cells[0].Value))
            //        {
            //            form.textBoxYear.Text = row.Cells[1].Value.ToString();
            //            form.textBoxОчное.Text = row.Cells[5].Value.ToString();
            //            form.textBoxОчно_заочное.Text = row.Cells[6].Value.ToString();
            //            form.textBoxЗаочное.Text = row.Cells[7].Value.ToString();

            //            form.comboBoxFilial.SelectedItem = row.Cells[2].Value.ToString();
            //            form.comboBoxSpecial.SelectedItem = row.Cells[3].Value.ToString();
            //            form.comboBoxSkill.SelectedItem = row.Cells[4].Value.ToString();
            //            form.checkBoxStdInv.Checked = Convert.ToBoolean(row.Cells["id"].Value);
            //        }
            //    }
            //    form.ShowDialog(); 
            //}
            #endregion

        }

        private void buttonAddStingPattern_MouseHover (object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(buttonAddStingPattern, "Создать по шаблону");
        }

        private void buttonEditString_MouseHover (object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(buttonEditString, "Редактировать");
        }

        private void buttonDeleteSelected_MouseHover (object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(buttonDeleteSelected, "Удалить несколько");
        }
        #endregion
        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            FormAddReport f = new FormAddReport();
            f.FillCombobox();

            SqlMyDataReader("Filial", 2, f.comboBoxFilial);
            SqlMyDataReader("Квалификации", 1, f.comboBoxSkill);
            SqlMyDataReader("Специальности", 1, f.comboBoxSpecial);

            f.Text = "Добавление";
            f.StatusOperation = 1;

            f.ShowDialog();
            FillDataGrid();

            //при нажатии на кнопку AddnewString - заполняются combobox
            //FormAddReport form = new FormAddReport();

            //SqlMyDataReader("Filial", 2, form.comboBoxFilial);
            //SqlMyDataReader("Квалификации", 1, form.comboBoxSkill);
            //SqlMyDataReader("Специальности", 1, form.comboBoxSpecial);

            //form.ShowDialog();
        }

        public void FormListCountStudent_Load(object sender, EventArgs e)
        {
            FillComboBoxes();
            FillDataGrid();
        }
                

        private void dataGridViewMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // textBox1.Text = dataGridViewMain.CurrentRow.Index.ToString();
           // dataGridViewMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void buttonDeleteSelected_Click(object sender, EventArgs e)
        {
            #region OLD
            //доделать проверку
            //и попробовать сделать асинхронный метод
            //List<string> ListId = new List<string>();
            //SQliteDB q = new SQliteDB();
            //foreach (DataGridViewRow rows in dataGridViewMain.Rows)
            //{
            //    if (Convert.ToBoolean(rows.Cells[0].Value))
            //    {
            //        ListId.Add(rows.Cells["id"].Value.ToString());
            //    }
            //}
            //q.Delete("ЧисленностьОбучающихся", "ЧисленностьОбучающихся.код", string.Join(",", ListId.ToArray()));
            //RefreshDataGridCiew();
            #endregion
            UInt32 count = 0;
            foreach (DataGridViewRow row in dataGridViewMain.Rows)
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


        public void buttonEditString_Click(object sender, EventArgs e)
        {
            #region OLD
            //if (CountSelectedRows("Редактирования"))
            //{
            //    //сделать проверку на количество выделенных строк. Должна быть только одна.
            //    //пока что функция не определяет количество выделенных строк. - это будущий баг.
            //    FormAddReport form = new FormAddReport();

            //    SqlMyDataReader("Filial", 2, form.comboBoxFilial);
            //    SqlMyDataReader("Квалификации", 1, form.comboBoxSkill);
            //    SqlMyDataReader("Специальности", 1, form.comboBoxSpecial);

            //    //выбирается всегда последний элемент списка
            //    foreach (DataGridViewRow row in dataGridViewMain.Rows)
            //    {
            //        if (Convert.ToBoolean(row.Cells[0].Value))
            //        {
            //            form.textBoxYear.Text = row.Cells[1].Value.ToString();
            //            form.textBoxОчное.Text          = row.Cells[6].Value.ToString();
            //            form.textBoxОчно_заочное.Text   = row.Cells[7].Value.ToString();
            //            form.textBoxЗаочное.Text        = row.Cells[8].Value.ToString();

            //            form.comboBoxFilial.SelectedItem    = row.Cells["Филиал"].Value;
            //            form.comboBoxSpecial.SelectedItem   = row.Cells["Специальность"].Value;
            //            form.comboBoxSkill.SelectedItem     = row.Cells["Квалификация"].Value;

            //            form.checkBoxStdInv.Checked = Convert.ToBoolean(row.Cells["Студент инвалид"].Value);
            //            form.GetCurrentrow_ID = Convert.ToInt32(row.Cells[9].Value);
            //        }
            //    }
            //    form.ShowDialog(); 
            //}
            #endregion
            if (CountSelectedRows("Редактирования"))
            {
                FillComponents(true);
            }
        }

        private void dataGridViewMain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }
                

        private void базовыеНормативыЗатратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormБазовыеНормативыЗатрат f = new FormБазовыеНормативыЗатрат();
            f.ShowDialog();
        }

        private void корректирующиеКоэффицентыToolStripMenuItem_Click (object sender, EventArgs e)
        {
            FormКоэффиценты f = new FormКоэффиценты();
            f.ShowDialog();
        }

        private void сформироватьОтчётToolStripMenuItem_Click (object sender, EventArgs e)
        {
            FormСозданиеОтчёта report = new FormСозданиеОтчёта();
            report.ShowDialog();
        }

        private void группыToolStripMenuItem_Click (object sender, EventArgs e)
        {
            FormGroups gr = new FormGroups();
            gr.ShowDialog();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingDB db = new FormSettingDB();
            db.ShowDialog();
        }
    }
}
