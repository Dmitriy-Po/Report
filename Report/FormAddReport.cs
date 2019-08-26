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

        public void buttonSave_Click (object sender, EventArgs e)
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
                comboBoxSkill.SelectedIndex
            });
           
            

            //db.Insert("ЧисленностьОбучающихся", ListFilial, ListSpecial, ListSkill, combobox.selectedindex);

        }

        private void FormAddReport_Load (object sender, EventArgs e)
        {

        }
    }
}
