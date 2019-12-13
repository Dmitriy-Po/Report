using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace Report
{
    public partial class FormListKoef : Form
    {
        public FormListKoef ()
        {
            InitializeComponent();
        }
        SQliteDB DB;
        DataTable table;
        SQLiteDataAdapter adapter;
        SQLiteCommandBuilder command;


        public int CurrentRow { get; set; }
        public string DATE { get; set; }

        void FillDataGrid ()
        {
            string s = "SELECT КорректирующиеКоэффиценты.код, Наименование, Уточнение FROM КорректирующиеКоэффиценты WHERE КорректирующиеКоэффиценты.код " +
                        "NOT IN(SELECT КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК FROM КоррКоэффицентБазовогоНорматива " +
                         $"WHERE КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК = {CurrentRow}); ";

            DB = new SQliteDB();
            using (SQLiteConnection cn = new SQLiteConnection(DB.ConnectionDB))
            {
                cn.Open();

                adapter = new SQLiteDataAdapter(s, cn);
                table = new DataTable();
                adapter.Fill(table);

                dataGridViewAddKoef.DataSource = table;
                dataGridViewAddKoef.Columns["код"].Visible = false;
                dataGridViewAddKoef.Columns["Наименование"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }


        private void FormListKoef_Load (object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void buttonClose_Click (object sender, EventArgs e)
        {
            Close();            
        }

        private void buttonAdd_Click (object sender, EventArgs e)
        {
            DB = new SQliteDB();
            FormБНЗ_Добавление fadd = new FormБНЗ_Добавление();

            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();
                adapter = new SQLiteDataAdapter("SELECT * FROM КоррКоэффицентБазовогоНорматива", connection);
                table = new DataTable();

                adapter.Fill(table);                

                foreach (DataGridViewRow row in dataGridViewAddKoef.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        DataRow newrow = table.NewRow();
                        newrow["Календарный_год"]       = Convert.ToDateTime(DATE + "-01-01").ToString("yyyy-MM-dd");
                        newrow["Базовый_норматив_ВК"]   = CurrentRow;
                        newrow["Корр_коэфф_ВК"]         = Convert.ToInt32(row.Cells["код"].Value);

                        table.Rows.Add(newrow);  
                    }
                }

                command = new SQLiteCommandBuilder(adapter);
                adapter.Update(table);

                Close();
            }
        }
    }
}
