using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report
{
    public partial class Form_КК_Добавление : Form
    {
        public Form_КК_Добавление ()
        {
            InitializeComponent();
        }

        private void buttonSaveAndClose_Click (object sender, EventArgs e)
        {
            //создаётся корректирующий коэффицент
            КорректирующиеКоэффиценты к = 
                new КорректирующиеКоэффиценты(

                    Convert.ToDecimal(textBoxCoeff.Text),           //значение
                    comboBoxFormEducation.SelectedItem.ToString(),  //форма обучения
                    comboBoxYear.SelectedItem.ToString()            //календарный год
                    );

            к.Наименование       = textBoxDesc.Text;
            к.ПолноеНаименование = textBoxFullDesc.Text;
            к.Уточнение          = textBoxDetail.Text;
            к.Комментарий        = textBoxComment.Text;
            к.СтудентИнвалид     = checkBoxStdInv.Checked; 

        }
    }
}
