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
            
           
        }

        private void button2_Click (object sender, EventArgs e)
        {
            comboBoxFilial.ResetText();
        }

        private void button3_Click (object sender, EventArgs e)
        {
            comboBoxSpecial.ResetText();
        }

        private void button5_Click (object sender, EventArgs e)
        {
            comboBoxSkill.ResetText();
        }

        private void buttonSave_Click (object sender, EventArgs e)
        {

        }
    }
}
