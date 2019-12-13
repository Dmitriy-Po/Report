using Report.Forms;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Report.Forms
{
    public partial class FormListBaseGroup : Form
    {
        public FormListBaseGroup ()
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
            string s = "SELECT БазовыйНормативЗатрат.код, Наименование FROM БазовыйНормативЗатрат WHERE БазовыйНормативЗатрат.код " +
                            "NOT IN(SELECT БНЗСтоимостнойГруппы.БНЗ_ВК FROM БНЗСтоимостнойГруппы WHERE " +
                            $"БНЗСтоимостнойГруппы.СтоимостнаяГруппаКалГода_ВК = {CurrentRow})";

            DB = new SQliteDB();
            using (SQLiteConnection cn = new SQLiteConnection(DB.ConnectionDB))
            {
                cn.Open();

                adapter = new SQLiteDataAdapter(s, cn);
                table = new DataTable();
                adapter.Fill(table);

                dataGridViewBaseNormal.DataSource = table;

                dataGridViewBaseNormal.Columns[0].Width = 30;
                dataGridViewBaseNormal.Columns["код"].Visible = false;
                dataGridViewBaseNormal.Columns["Наименование"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void FormListBaseGroup_Load (object sender, EventArgs e)
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
            FormGroupAdd fadd = new FormGroupAdd();

            using (SQLiteConnection connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();
                adapter = new SQLiteDataAdapter("select БНЗСтоимостнойГруппы.код, "+
                                                    "БНЗСтоимостнойГруппы.Бакалавриат, " +
                                                    "БНЗСтоимостнойГруппы.Магистратура, " +
                                                    "БНЗСтоимостнойГруппы.Аспирантура, " +
                                                    "БНЗСтоимостнойГруппы.СПО, " +
                                                    "БНЗСтоимостнойГруппы.КалендарныйГод, " +
                                                    "БНЗСтоимостнойГруппы.БНЗ_ВК, " +
                                                    "БНЗСтоимостнойГруппы.СтоимостнаяГруппаКалГода_ВК " +
                                                    "from БНЗСтоимостнойГруппы", connection);
                table = new DataTable();

                adapter.Fill(table);

                foreach (DataGridViewRow row in dataGridViewBaseNormal.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        DataRow newrow = table.NewRow();

                        newrow["Бакалавриат"]   = 0;
                        newrow["Магистратура"]  = 0;
                        newrow["Аспирантура"]   = 0;
                        newrow["СПО"]           = 0;

                        newrow["КалендарныйГод"]                = Convert.ToDateTime(DATE + "-01-01").ToString("yyyy-MM-dd");
                        newrow["СтоимостнаяГруппаКалГода_ВК"]   = CurrentRow;
                        newrow["БНЗ_ВК"]                        = Convert.ToInt32(row.Cells["код"].Value);

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
