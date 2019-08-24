using System;
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
    }
}
