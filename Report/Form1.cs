using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Report
{
    public partial class FormListCountStudent : System.Windows.Forms.Form
    {
        public FormListCountStudent ()
        {
            InitializeComponent();

        }

        public void MySqlDataReader (string name_table, int num_col, ComboBox box)
        {
            SQliteDB sql = new SQliteDB();
            SQLiteDataReader r = sql.Select(name_table);
            
            while (r.Read())
            {
               box.Items.Add(r[num_col]);
               //box.Items.
            }            
            r.Close();
           
        }

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

        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            FormAddReport form = new FormAddReport();

           // MySqlDataReader("Filial", 0, form.comboBoxFilial);
            MySqlDataReader("Квалификации", 1, form.comboBoxSkill);
            MySqlDataReader("Специальности", 1, form.comboBoxSpecial);

            form.ShowDialog();
        }
    }
}
