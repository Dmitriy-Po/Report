using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Report.Forms
{
    public partial class FormListSpecialAndGroups : Form
    {
        public FormListSpecialAndGroups ()
        {
            InitializeComponent();
        }

        public int CurrentRow { get; set; }
        public string DATE { get; set; }


        SQliteDB DB;
        DataTable table;
        SQLiteDataAdapter adapter;
        SQLiteCommandBuilder command;

        void FillDataGrid ()
        {
            string s = "SELECT Специальности.код, наименование FROM Специальности " +
                            "WHERE Специальности.код NOT IN(SELECT СпециальностьСтоимостнойГруппы.код_специальность "+
                            $"FROM СпециальностьСтоимостнойГруппы WHERE СпециальностьСтоимостнойГруппы.код_группа = {CurrentRow}) ORDER BY 2";

            DB = new SQliteDB();
            using (SQLiteConnection cn = new SQLiteConnection(DB.ConnectionDB))
            {
                cn.Open();

                adapter = new SQLiteDataAdapter(s, cn);
                table = new DataTable();
                adapter.Fill(table);

                dataGridViewSpecial.DataSource = table;

                dataGridViewSpecial.Columns[0].Width = 30;
                dataGridViewSpecial.Columns["код"].Visible = false;
                dataGridViewSpecial.Columns["наименование"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        void AddRecord ()
        {
            DB = new SQliteDB();
            FormGroupAdd fadd = new FormGroupAdd();

            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();
                adapter = new SQLiteDataAdapter("SELECT СпециальностьСтоимостнойГруппы.код_группа, "+
                            "код_специальность FROM СпециальностьСтоимостнойГруппы", connection);

                table = new DataTable();

                adapter.Fill(table);

                foreach (DataGridViewRow row in dataGridViewSpecial.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        DataRow newrow = table.NewRow();

                        newrow["код_специальность"]     = Convert.ToInt32(row.Cells["код"].Value);
                        newrow["код_группа"]            = CurrentRow;
                        
                        table.Rows.Add(newrow);
                    }
                }

                command = new SQLiteCommandBuilder(adapter);
                adapter.Update(table);

                Close();
            }
        }

        private void FormListSpecialAndGroups_Load (object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void buttonClose_Click (object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click (object sender, EventArgs e)
        {
            AddRecord();
        }
    }
}
