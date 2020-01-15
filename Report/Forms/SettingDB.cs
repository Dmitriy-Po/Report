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
            SQliteDB database = new SQliteDB();
            

            textBoxPuth.Text = database.GetConnection();
        }
    }
}
