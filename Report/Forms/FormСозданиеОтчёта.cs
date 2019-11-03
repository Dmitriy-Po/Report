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
            КорректирующийКоэффицентБазовогоНорматива correct_base_normal2 = new КорректирующийКоэффицентБазовогоНорматива("2019");
            КорректирующийКоэффицентБазовогоНорматива correct_base_normal3 = new КорректирующийКоэффицентБазовогоНорматива("2019");
            КорректирующийКоэффицентБазовогоНорматива correct_base_normal4 = new КорректирующийКоэффицентБазовогоНорматива("2019");
            КорректирующийКоэффицентБазовогоНорматива correct_base_normal5 = new КорректирующийКоэффицентБазовогоНорматива("2019");
            КорректирующийКоэффицентБазовогоНорматива correct_base_normal6 = new КорректирующийКоэффицентБазовогоНорматива("2019");
            КорректирующийКоэффицентБазовогоНорматива correct_base_normal7 = new КорректирующийКоэффицентБазовогоНорматива("2019");



            КорректирующиеКоэффиценты correct_coef1 = new КорректирующиеКоэффиценты("Образовательные стандарты", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef2 = new КорректирующиеКоэффиценты("2", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef3 = new КорректирующиеКоэффиценты("3", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef4 = new КорректирующиеКоэффиценты("4", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef5 = new КорректирующиеКоэффиценты("5", "ПолноеОписание", "Уточнение", "Комментарий", false);
            КорректирующиеКоэффиценты correct_coef6 = new КорректирующиеКоэффиценты("6", "ПолноеОписание", "Уточнение", "Комментарий", false);
            
            // Добавлен, но пока не используется.
            КорректирующиеКоэффиценты ochnaya = new КорректирующиеКоэффиценты("", "", "", "", false);


            ЗначениеКоэффицента val_coef1 = new ЗначениеКоэффицента(1.15m, "2019");
            ЗначениеКоэффицента val_coef2 = new ЗначениеКоэффицента(2, "2019");
            ЗначениеКоэффицента val_coef3 = new ЗначениеКоэффицента(1.2m, "2019");
            ЗначениеКоэффицента val_coef4 = new ЗначениеКоэффицента(1, "2019");
            ЗначениеКоэффицента val_coef5 = new ЗначениеКоэффицента(1.1m, "2019");
            ЗначениеКоэффицента val_coef6 = new ЗначениеКоэффицента(1.25m, "2019");

            // Добавлен, но пока не используется.
            ЗначениеКоэффицента val_ochnaya = new ЗначениеКоэффицента(0.25m, "2019");


            // Коэффицент может иметь несколько значений.
            correct_coef1.SetValueCoef(val_coef1);
            correct_coef2.SetValueCoef(val_coef2);
            correct_coef3.SetValueCoef(val_coef3);
            correct_coef4.SetValueCoef(val_coef4);
            correct_coef5.SetValueCoef(val_coef5);
            correct_coef6.SetValueCoef(val_coef6);

            ochnaya.SetValueCoef(val_ochnaya);
            
           

            correct_base_normal1.SetCorrectCoef(correct_coef1);
            correct_base_normal2.SetCorrectCoef(correct_coef2);
            correct_base_normal3.SetCorrectCoef(correct_coef3);
            correct_base_normal4.SetCorrectCoef(correct_coef4);
            correct_base_normal5.SetCorrectCoef(correct_coef5);
            correct_base_normal6.SetCorrectCoef(correct_coef6);

            correct_base_normal7.SetCorrectCoef(ochnaya);




            normal1.SetCorrectCoef(correct_base_normal1);
            normal1.SetCorrectCoef(correct_base_normal2);
            normal1.SetCorrectCoef(correct_base_normal3);
            normal1.SetCorrectCoef(correct_base_normal4);
            normal1.SetCorrectCoef(correct_base_normal5);
            normal1.SetCorrectCoef(correct_base_normal6);

            //normal1.SetCorrectCoef(correct_base_normal7);


            All_normals.Add(normal1);
            /*
             * 
             * Искуственно введу массив - содержащий количество студентов Конкретно формы обучения
             * Очная, очно-заочная, заочная соответственно. Так же к ним коэффиценты приведения.
             * */
            int[] form_education = { 24, 14, 7 };
            decimal[] coef_priv = { 1, 0.25m, 0.1m };


            decimal Bakalavr = 1;
            decimal Magistr = 1;
            decimal Aspirant = 1;
            decimal SPO = 1;

            decimal SummOnGroup = 0;       // Сумма по группе.
            decimal Summ = 0;              // Сумма по группе с учётом количества студентов и формы обучения.
            decimal K_equal = 1.256m;      // Коэффицент выравнивания.

            // Цикл считает по одному нормативу. Пока сделано так, чтобы проверить ход расчётов
            foreach (БазовыйНормативЗатратСтоимостнойГруппы item in All_normals[0].GetNormativ())
            {                
                Bakalavr    = item.Бакалавриат_Специалитет;
                Magistr     = item.Магистратура;
                Aspirant    = item.Аспирантура;
                SPO         = item.SPO;


                foreach (КорректирующийКоэффицентБазовогоНорматива coef in All_normals[0].GetCorrectCoef())
                {
                    foreach (КорректирующиеКоэффиценты kk in coef.GetCorrectCoef())
                    {
                        foreach (ЗначениеКоэффицента val in kk.GetValueCoef())
                        {
                            Bakalavr *= val.Коэффицент;
                            Magistr *= val.Коэффицент;
                            Aspirant *= val.Коэффицент;
                            SPO      *= val.Коэффицент;
                        }
                    }                    
                }
                // Умножить на кол-во человек в группе.
                for (int i = 0; i < form_education.Length; i++)
                {
                    SummOnGroup += (Bakalavr + Magistr + Aspirant + SPO) * form_education[i];
                    SummOnGroup *= coef_priv[i];
                }
                Summ += SummOnGroup;
            }
            Summ *= K_equal;
        }

        private void buttonShowReport_Click (object sender, EventArgs e)
        {
            Fill();              
        }
    }
}
