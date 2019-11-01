using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Report.Classes;

namespace Report.Forms
{
    public partial class FormСозданиеОтчёта : Form
    {
        public FormСозданиеОтчёта ()
        {
            InitializeComponent();
        }
        void Fill ()
        {
            List<БазовыеНормативыЗатрат> All_normals = new List<БазовыеНормативыЗатрат>();
            БазовыеНормативыЗатрат normal1 = new БазовыеНормативыЗатрат("Затраты на ЗП", "2019");



            СтоимостнаяГруппаКалендарногоГода group1 = new СтоимостнаяГруппаКалендарногоГода("Группа 1", "2019");
            СтоимостнаяГруппаКалендарногоГода group2 = new СтоимостнаяГруппаКалендарногоГода("Группа 2", "2019");
            СтоимостнаяГруппаКалендарногоГода group3 = new СтоимостнаяГруппаКалендарногоГода("Группа 3", "2019");


            БазовыйНормативЗатратСтоимостнойГруппы norm1 = new БазовыйНормативЗатратСтоимостнойГруппы(new decimal[] { 37.01m, 42.56m, 47.7m, 1m }, "2019");
            БазовыйНормативЗатратСтоимостнойГруппы norm2 = new БазовыйНормативЗатратСтоимостнойГруппы(new decimal[] { 37.01m, 42.56m, 47.7m, 71.55m }, "2019");
            БазовыйНормативЗатратСтоимостнойГруппы norm3 = new БазовыйНормативЗатратСтоимостнойГруппы(new decimal[] { 44.41m, 46.26m, 57.24m, 74.55m }, "2019");


            group1.SetNormal(norm1);
            group2.SetNormal(norm2);
            group3.SetNormal(norm3);


            normal1.SetNormativ(norm1);
            normal1.SetNormativ(norm2);
            normal1.SetNormativ(norm3);



            //FormEducation form_education1 = new FormEducation("Очно-заочная");
            //form_education1.SetCorrectCoef(val_coef6);


            /*добавиьть форму обучения*/


            КорректирующийКоэффицентБазовогоНорматива correct_base_normal1 = new КорректирующийКоэффицентБазовогоНорматива("2019");

            КорректирующиеКоэффиценты correct_coef1 = new КорректирующиеКоэффиценты("Образовательные стандарты", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef2 = new КорректирующиеКоэффиценты("2", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef3 = new КорректирующиеКоэффиценты("3", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef4 = new КорректирующиеКоэффиценты("4", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef5 = new КорректирующиеКоэффиценты("5", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef6 = new КорректирующиеКоэффиценты("6", "ПолноеОписание", "Уточнение", "Комментарий", false);

            ЗначениеКоэффицента val_coef1 = new ЗначениеКоэффицента(1.15m, "2019");
            ЗначениеКоэффицента val_coef2 = new ЗначениеКоэффицента(2, "2019");
            ЗначениеКоэффицента val_coef3 = new ЗначениеКоэффицента(1.2m, "2019");
            ЗначениеКоэффицента val_coef4 = new ЗначениеКоэффицента(1, "2019");
            ЗначениеКоэффицента val_coef5 = new ЗначениеКоэффицента(1.1m, "2019");
            ЗначениеКоэффицента val_coef6 = new ЗначениеКоэффицента(1.25m, "2019");



            correct_coef1.SetValueCoef(val_coef1);
            correct_coef2.SetValueCoef(val_coef2);
            correct_coef3.SetValueCoef(val_coef3);
            correct_coef4.SetValueCoef(val_coef4);
            correct_coef5.SetValueCoef(val_coef5);
            correct_coef6.SetValueCoef(val_coef6);



 /**/       correct_coef1.SetCorrectCoef(correct_base_normal1);
            correct_coef2.SetCorrectCoef(correct_base_normal1);
            correct_coef3.SetCorrectCoef(correct_base_normal1);
            correct_coef4.SetCorrectCoef(correct_base_normal1);
            correct_coef5.SetCorrectCoef(correct_base_normal1);
            correct_coef6.SetCorrectCoef(correct_base_normal1);            


            
            normal1.SetCorrectCoef(correct_base_normal1);

            All_normals.Add(normal1);

            foreach (БазовыйНормативЗатратСтоимостнойГруппы item in All_normals[0].GetNormativ())
            {
                decimal val = item.Бакалавриат_Специалитет * ;
            }
        }

        private void buttonShowReport_Click (object sender, EventArgs e)
        {
            Fill();              
        }
    }
}
