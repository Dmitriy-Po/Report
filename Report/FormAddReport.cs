using Report;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public void save()
        {
            /* 
             * Кнопка для сохранения записи в таблицу ЧисленностьОбучающихся.
               Метод передаёт на форму FormListCountStudent 
                индексы выбранных полей, в выпадающих списках.
               По индексам выбирается элемент в коллекции. 
             */
            FormListCountStudent FormStudent = new FormListCountStudent();

            FormStudent.SaveStudent(new int[] {
                comboBoxFilial.SelectedIndex,
                comboBoxSpecial.SelectedIndex,
                comboBoxSkill.SelectedIndex,
                Convert.ToInt32(textBoxОчное.Text),
                Convert.ToInt32(textBoxОчно_заочное.Text),
                Convert.ToInt32(textBoxЗаочное.Text),
                Convert.ToInt32(textBoxYear.Text),
                Convert.ToInt32(checkBoxStdInv.Checked)
            });
        }

        #region clearcomboboxes
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
        #endregion
        public void buttonSave_Click (object sender, EventArgs e)
        {
            save();//сохранить
        }
        
        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            //сохранить и закрыть форму
            save();
            Close();
        }
    }
}
