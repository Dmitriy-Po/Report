using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

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
            FillDataGrid();
            FillComboBoxes();

            /*---------------------------------------------------------*/
            comboBoxFil.SelectedIndexChanged += (s, e) => DataFilter();
            comboBoxSpec.SelectedIndexChanged += (s, e) => DataFilter();
            textBoxYear.TextChanged += (s, e) => DataFilter();
            checkBoxStudent_inv.Click += (s, e) => DataFilter();

        }
        public List<TableFilial>  ListFilial = new List<TableFilial>();
        public List<TableSKill>   ListSkill = new List<TableSKill>();
        public List<TableSpecial> ListSpecial = new List<TableSpecial>();
        public List<TableCountStudent> ListCountStudent = new List<TableCountStudent>();            
        
        public void FillComboBoxes ()
        {
            SqlMyDataReader("Filial", 2, comboBoxFil);            
            SqlMyDataReader("Специальности", 1, comboBoxSpec);
            //comboBoxFil.Items.Insert(0, "Все филиалы");
            //comboBoxSpec.Items.Insert(0, "Все специальности");
            comboBoxFil.SelectedIndex = 0;
            comboBoxSpec.SelectedIndex = 0;
            textBoxYear.Text = DateTime.Today.Year.ToString();
        }
        public void DataFilter ()
        {
            try
            {
                var newlist = ListCountStudent
                        .Where(x => x.Filial.Contains(comboBoxFil.SelectedItem.ToString()) &&
                                    x.Special.Contains(comboBoxSpec.SelectedItem.ToString()) &&
                                    x.year == Convert.ToInt32(textBoxYear.Text) &&
                                    x.student_inv.Equals(checkBoxStudent_inv.Checked))
                        .Select(x => x).Distinct().ToList();

                dataGridViewMain.Rows.Clear();
                for (int i = 0; i < newlist.Count(); i++)
                {
                    dataGridViewMain.Rows.Add(0,
                        newlist[i].year,
                        newlist[i].Filial,
                        newlist[i].Special,
                        newlist[i].Skill,
                        newlist[i].ochnoe,
                        newlist[i].zaochnoe,
                        newlist[i].ochno_zaocjnoe,
                        newlist[i].student_inv
                        );
                }
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

        #region select                
        public void RefreshDataGridCiew()
        {
            dataGridViewMain.Rows.Clear();
            FillDataGrid();
        }
        public void FillDataGrid()
        {
            //возможно стоит использовать datasource для получекния данных
            //но пока действует по выборе из базы данных
            string query = " SELECT " +
            "Filial.full_desc as 'Структурное подразделение', " +
            "Специальности.наименование as 'Специальность', " +
            "Квалификации.наименование as 'Квалификация', " +
            "ЧисленностьОбучающихся.очное as 'Очное', " +
            "ЧисленностьОбучающихся.очно_заочное as 'Очно-заочное', " +
            "ЧисленностьОбучающихся.заочное as 'Заочное', " +
            "ЧисленностьОбучающихся.студент_инвалид as 'Студент инвалид', " +
            "ЧисленностьОбучающихся.код as 'id', "+
            "ЧисленностьОбучающихся.год as 'year'"+
            "FROM ЧисленностьОбучающихся " +
            "INNER JOIN Filial ON " +
            "ЧисленностьОбучающихся.стуктурное_подразделение_ВК = Filial.id " +
            "INNER JOIN Специальности ON " +
            "ЧисленностьОбучающихся.специальность_ВК = Специальности.код " +
            "INNER JOIN Квалификации ON " +
            "ЧисленностьОбучающихся.квалификация_ВК = Квалификации.код";

            
            SQLiteConnection conn = new SQLiteConnection("Data Source=ReportDB.db; Version=3;");
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            
            
            conn.Open();
            SQLiteDataReader r = cmd.ExecuteReader();
            
            while (r.Read())
            {
                dataGridViewMain.Rows.Add(0, r[8], r[0], r[1], r[2], r[3], r[4], r[5], r[6], r[7]);
                ListCountStudent.Add(new TableCountStudent()
                {
                    Filial =         Convert.ToString(r[0]),
                    Skill =          Convert.ToString(r[2]),
                    Special =        Convert.ToString(r[1]),
                    ochnoe =         Convert.ToInt32(r[3]),
                    ochno_zaocjnoe = Convert.ToInt32(r[4]),
                    zaochnoe =       Convert.ToInt32(r[5]),
                    student_inv =    Convert.ToBoolean(r[6]),
                    id =             Convert.ToInt32(r[7]),
                    year =           Convert.ToInt32(r[8])

                });
            }
            r.Close();
            conn.Close();
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
        #endregion
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
            if (CountSelectedRows("Создать по шаблону"))
            {
                FormAddReport form = new FormAddReport();
                SqlMyDataReader("Filial", 2, form.comboBoxFilial);
                SqlMyDataReader("Квалификации", 1, form.comboBoxSkill);
                SqlMyDataReader("Специальности", 1, form.comboBoxSpecial);

                foreach (DataGridViewRow row in dataGridViewMain.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        form.textBoxYear.Text = row.Cells[1].Value.ToString();
                        form.textBoxОчное.Text = row.Cells[5].Value.ToString();
                        form.textBoxОчно_заочное.Text = row.Cells[6].Value.ToString();
                        form.textBoxЗаочное.Text = row.Cells[7].Value.ToString();

                        form.comboBoxFilial.SelectedItem = row.Cells[2].Value.ToString();
                        form.comboBoxSpecial.SelectedItem = row.Cells[3].Value.ToString();
                        form.comboBoxSkill.SelectedItem = row.Cells[4].Value.ToString();
                        form.checkBoxStdInv.Checked = (bool)row.Cells[8].Value;
                    }
                }
                form.ShowDialog(); 
            }

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
            //при нажатии на кнопку AddnewString - заполняются combobox
            FormAddReport form = new FormAddReport();

            SqlMyDataReader("Filial", 2, form.comboBoxFilial);
            SqlMyDataReader("Квалификации", 1, form.comboBoxSkill);
            SqlMyDataReader("Специальности", 1, form.comboBoxSpecial);

            form.ShowDialog();
        }

        public void FormListCountStudent_Load(object sender, EventArgs e)
        {
            
        }

        private void бДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDataGridCiew();
        }

        private void dataGridViewMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // textBox1.Text = dataGridViewMain.CurrentRow.Index.ToString();
           // dataGridViewMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void buttonDeleteSelected_Click(object sender, EventArgs e)
        {
            //доделать проверку
            //и попробовать сделать асинхронный метод
            List<string> ListId = new List<string>();
            SQliteDB q = new SQliteDB();
            foreach (DataGridViewRow rows in dataGridViewMain.Rows)
            {
                if (Convert.ToBoolean(rows.Cells[0].Value))
                {
                    ListId.Add(rows.Cells["ColumnID"].Value.ToString());
                }
            }
            q.Delete("ЧисленностьОбучающихся", "ЧисленностьОбучающихся.код", string.Join(",", ListId.ToArray()));
            RefreshDataGridCiew();            
        }


        public void buttonEditString_Click(object sender, EventArgs e)
        {
            if (CountSelectedRows("Редактирования"))
            {
                //сделать проверку на количество выделенных строк. Должна быть только одна.
                //пока что функция не определяет количество выделенных строк. - это будущий баг.
                FormAddReport form = new FormAddReport();

                SqlMyDataReader("Filial", 2, form.comboBoxFilial);
                SqlMyDataReader("Квалификации", 1, form.comboBoxSkill);
                SqlMyDataReader("Специальности", 1, form.comboBoxSpecial);

                //выбирается всегда последний элемент списка
                foreach (DataGridViewRow row in dataGridViewMain.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        form.textBoxYear.Text = row.Cells[1].Value.ToString();
                        form.textBoxОчное.Text = row.Cells[5].Value.ToString();
                        form.textBoxОчно_заочное.Text = row.Cells[6].Value.ToString();
                        form.textBoxЗаочное.Text = row.Cells[7].Value.ToString();

                        form.comboBoxFilial.SelectedItem = row.Cells[2].Value.ToString();
                        form.comboBoxSpecial.SelectedItem = row.Cells[3].Value.ToString();
                        form.comboBoxSkill.SelectedItem = row.Cells[4].Value.ToString();
                        form.checkBoxStdInv.Checked = (bool)row.Cells[8].Value;
                        form.GetCurrentrow_ID = Convert.ToInt32(row.Cells[9].Value);
                    }
                }
                form.ShowDialog(); 
            }
            
        }

        private void dataGridViewMain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            //RefreshDataGridCiew();
            SQliteDB q = new SQliteDB();
            DataContext x = new DataContext("Data Source=ReportDB.db;");

            //var table = from t in x.GetTable<TableFilial>()
            //            where t.id > 5
            //            select t;
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
    }
}
