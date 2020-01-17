using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report.Forms
{
    public partial class FormSettingDB : Form
    {
        public FormSettingDB()
        {
            InitializeComponent();
        }

        SQLiteConnection Connection;
        SQLiteDataAdapter Adapter;
        DataTable Table;
        SQliteDB DB;

        
        private void FormSettingDB_Load(object sender, EventArgs e)
        {
            textBoxPuth.Text = Properties.Settings.Default.PuthDB;            
        }
        void ImportListCountStudent(string[] Lines)
        {
            DB = new SQliteDB();
            using (Connection = new SQLiteConnection(DB.ConnectionDB))
            {
                string[] Columns = null;
                string SelectCountStudent = "SELECT * FROM ЧисленностьОбучающихся";

                Connection.Open();

                Adapter = new SQLiteDataAdapter(SelectCountStudent, Connection);
                Table = new DataTable();
                Adapter.Fill(Table);
                DataRow NewRow;

                
                foreach (var line in Lines)
                {
                    Columns = line.Split(',');
                    NewRow = Table.NewRow();

                    NewRow["год"] = Columns[0];

                    NewRow["очное"] = Columns[0];
                    NewRow["очно_заочное"] = Columns[0];
                    NewRow["заочное"] = Columns[0];

                    NewRow["студент_инвалид"] = Columns[0];

                    NewRow["стуктурное_подразделение_ВК"] = Columns[0];
                    NewRow["специальность_ВК"] = Columns[0];
                    NewRow["квалификация_ВК"] = Columns[0];

                    Table.Rows.Add(NewRow);
                }
                SQLiteCommandBuilder build = new SQLiteCommandBuilder(Adapter);
                Adapter.Update(Table);
            }

        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this.Owner, "После выбора файла базы данных - приложение будет перезагруженно.\nПродолжить?",
                "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                OpenFileDialog file = new OpenFileDialog();

                file.Filter = "Files DB | *.db";
                file.Title = "Укажите путь к файлу базы данных";

                if (file.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PuthDB = file.FileName;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(this.Owner, "Настройки сохранены, программа будет перезапущена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
            }
        }

        private void buttonImportCSV_Click(object sender, EventArgs e)
        {
            //ReadCSV();

            OpenFileDialog file_csv = new OpenFileDialog();
            //file_csv.Filter = "CSV - файлы|*.csv";
            file_csv.Filter = "Текстовые файлы|*.txt";
            file_csv.Title = "Импорт данных";
            file_csv.Multiselect = false;

            DialogResult result = file_csv.ShowDialog();

            if (result == DialogResult.OK)
            {
                string[] FilePuth = file_csv.FileNames;
                string[] FileName = file_csv.SafeFileNames;
                string[] LinesInFile = null;

                int count = FilePuth.Count();
                for (int i = 0; i < count; i++)
                {
                    LinesInFile = File.ReadAllLines(FilePuth[i]);                    
                    switch (FileName[i])
                    {
                        case "ЧисленностьОбучающихся.txt":
                            {
                                ImportListCountStudent(LinesInFile);
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
        }
    }
}
