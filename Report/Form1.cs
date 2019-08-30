using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
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
            
        }
        public List<TableFilial>  ListFilial = new List<TableFilial>();
        public List<TableSKill>   ListSkill = new List<TableSKill>();
        public List<TableSpecial> ListSpecial = new List<TableSpecial>();
        public List<TableCountStudent> ListCountStudent = new List<TableCountStudent>();
        
        public void FillDataGrid()
        {
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
                dataGridViewMain.Rows.Add(0, r[8], r[0], r[1], r[2], r[3], r[4], r[5], r[6]);
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
                    ListSkill[column[1]].id,
                    ListSpecial[column[2]].id,
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
           
        }
    }
}
