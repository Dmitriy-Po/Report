using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Linq;

namespace Report
{
    public partial class FormБНЗ_Добавление : Form
    {
        SQliteDB db;
        DataSet dataset;    
        DataTable table0;
        DataTable table1;        
        SQLiteDataAdapter adapter;
        SQLiteCommandBuilder command;
        public List<КорректирующиеКоэффиценты> ListKK = new List<КорректирующиеКоэффиценты>();
        


        public ushort StatusOperation { get; set; } /*Для выбора режима Редактирования, Добавления или Дубликата */
        public int CurrentDataRow { get; set; }

        public FormБНЗ_Добавление ()
        {
            InitializeComponent();            
        }
        public void FillCombobox ()
        {
            int y = DateTime.Now.Year - 3;
            do
            {
                comboBoxYear.Items.Add(++y);
            } while (y != DateTime.Now.Year);
        }       
        public void FillList()
        {
            string q = "SELECT КорректирующиеКоэффиценты.код, Наименование, Уточнение FROM КорректирующиеКоэффиценты WHERE КорректирующиеКоэффиценты.код " +
                       "NOT IN(SELECT КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК FROM КоррКоэффицентБазовогоНорматива " +
                      $" WHERE КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК = {CurrentDataRow}); ";

            db = new SQliteDB();
            SQLiteConnection connection = new SQLiteConnection(db.ConnectionDB);

            connection.Open();
            adapter = new SQLiteDataAdapter(q, connection);
            table1 = new DataTable();

            adapter.Fill(table1);

            foreach (DataRow row in table1.Rows)
            {
                ListKK.Add(new КорректирующиеКоэффиценты(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString()));
            }
            comboBoxCorrectKoef.Items.AddRange(ListKK.Select(x => x.Наименование).ToArray());
            connection.Close();
        }
        private void FormБНЗ_Добавление_Load (object sender, EventArgs e)
        {
            string query =
                            // Table 0.
                            "SELECT КорректирующиеКоэффиценты.Наименование, Уточнение " +
                            "FROM КоррКоэффицентБазовогоНорматива " +
                            "JOIN КорректирующиеКоэффиценты ON КорректирующиеКоэффиценты.код = КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК " +
                            $"WHERE КоррКоэффицентБазовогоНорматива.Календарный_год LIKE '{comboBoxYear.SelectedItem.ToString()}%' " +
                            $"AND КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК = {CurrentDataRow}; ";
                            
                            

            db = new SQliteDB();
            using (SQLiteConnection conection = new SQLiteConnection(db.ConnectionDB))
            {
                conection.Open();
                              
                adapter = new SQLiteDataAdapter(query, conection);
                
                table0 = new DataTable();               

                adapter.Fill(table0);               
                

                dataGridViewKoef.DataSource = table0;
                dataGridViewKoef.Columns[0].Width = 25;
                dataGridViewKoef.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewKoef.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;                
               
            }
            

        }

        private void buttonAddKoef_Click (object sender, EventArgs e)
        {
           
        }

        private void comboBoxCorrectKoef_SelectionChangeCommitted (object sender, EventArgs e)
        {

        }
    }
}
