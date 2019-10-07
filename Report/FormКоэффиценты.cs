using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Report.Classes;

namespace Report
{
    partial class FormКоэффиценты : Form
    {        
        public FormКоэффиценты ()
        {
            InitializeComponent();
            FormEducation.Fill(ListEducation);          
        }
        public List<FormEducation> ListEducation = new List<FormEducation>();

        private void buttonAddNewString_Click (object sender, EventArgs e)
        {
            Form_КК_Добавление f = new Form_КК_Добавление();
            f.comboBoxFormEducation.Items.AddRange
                (ListEducation.Select(x => x.Desc).ToArray());
            f.ShowDialog();
        }

        private void FormКоэффиценты_Load(object sender, EventArgs e)
        {
            int y = DateTime.Now.Year;
            for (int i = y-3; i <= y; i++)
            {
                comboBoxYear.Items.Add(i);
            }
            
            ////создаётся корректирующий коэффицент
            //КорректирующиеКоэффиценты к =
            //    new КорректирующиеКоэффиценты(

            //        3.15m,                                      //значение
            //        new Classes.ClassFormEducation("Очная"),    //форма обучения
            //        "2019"                                      //календарный год
            //        );

            //к.Наименование = "Затраты на ЗП";
            //к.ПолноеНаименование = "полное наименование";
            //к.Уточнение = "уточнение";
            //к.Комментарий = "комментарий";
            //к.СтудентИнвалид = false;

            //Mylist.Add(к);

            //foreach (var item in Mylist)
            //{
            //    dataGridViewCoeff.Rows.Add(0, Mylist[0].Наименование,
            //    Mylist[0].ПолноеНаименование, Mylist[0].Уточнение, Mylist[0].GetCoef(),
            //    Mylist[0].GetYear(), Mylist[0].GetForm());
            //}
            //FormListCountStudent formcount = new FormListCountStudent();
            //Form_КК_Добавление t = new Form_КК_Добавление();
            //formcount.SqlMyDataReader("FORM", 1, t.comboBoxFormEducation);


        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }
    }
}
