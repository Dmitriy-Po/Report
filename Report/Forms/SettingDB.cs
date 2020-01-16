using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void FormSettingDB_Load(object sender, EventArgs e)
        {
            textBoxPuth.Text = Properties.Settings.Default.PuthDB;            
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
    }
}
