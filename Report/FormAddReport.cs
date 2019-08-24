using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Report
{
    public partial class FormAddReport : Form
    {
        public FormAddReport ()
        {
            InitializeComponent();
        }

        public string text
        {
            get { return textBoxFilial.Text; }
            set { textBoxFilial.Text = value; }
        }

        private void label2_Click (object sender, EventArgs e)
        {

        }

        private void buttonSave_MouseHover (object sender, EventArgs e)
        {
        	//добавлен небольшой коментарий
            ToolTip t = new ToolTip();
            t.SetToolTip(buttonSave, "Сохранить");
        }

        private void buttonSaveAndClose_MouseHover (object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(buttonSaveAndClose, "Сохранить и закрыть");
        }

        private void button1_Click (object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=ReportDB.db; Version=3;");
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Filial", conn);
            FormContext fc = new FormContext();

            conn.Open();
            try
            {
                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    fc.listBoxItem.Items.Add(r[1]+" | "+r[2]);
                }
                r.Close();
            }
            catch (SQLiteException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            conn.Close();
            fc.ShowDialog();
        }

        private void button2_Click (object sender, EventArgs e)
        {
            
        }
    }
}
