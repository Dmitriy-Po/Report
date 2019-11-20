using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;



namespace Report.Forms
{
    public partial class FormСозданиеОтчёта : Form
    {
        public FormСозданиеОтчёта ()
        {
            InitializeComponent();
        }
        


        Dictionary<int, string[]> Fill (string year, decimal K_equal)
        {
            SQliteDB database = new SQliteDB();
            SQLiteConnection connection = new SQLiteConnection(database.ConnectionDB);
            string query =
                            // Table 0
                            "SELECT БазовыйНормативЗатрат.код as 'id', БазовыйНормативЗатрат.Наименование as 'наименование' FROM БазовыйНормативЗатрат;" +

                            // Table 1
                            "SELECT * FROM БНЗСтоимостнойГруппы ORDER BY БНЗ_ВК ASC; " +

                            // Table 2
                            "SELECT БазовыйНормативЗатрат.код, ЗначениеКоэффицента.Значение " +
                            "FROM КоррКоэффицентБазовогоНорматива " +
                            "JOIN ЗначениеКоэффицента ON " +
                                 "КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК = ЗначениеКоэффицента.Корректирующие_ВК " +
                            "JOIN БазовыйНормативЗатрат ON БазовыйНормативЗатрат.код = КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК; " +

                            // Table 3
                            "SELECT Filial.id as 'Филиал_код', " +                                
                                "Filial.full_desc as 'Филиал', " +
                                "Специальности.наименование as 'Специальность', " +
                                "Квалификации.наименование as 'Квалификация',  " +
                                "ЧисленностьОбучающихся.очное as 'Очное', "+
                                "ЧисленностьОбучающихся.очно_заочное as 'Очно-заочное', "+
                                "ЧисленностьОбучающихся.заочное as 'Заочное', "+
                                "ЧисленностьОбучающихся.год " +
                                "FROM ЧисленностьОбучающихся " +
                                "JOIN Filial ON ЧисленностьОбучающихся.стуктурное_подразделение_ВК = Filial.id " +
                                "JOIN Специальности ON ЧисленностьОбучающихся.специальность_ВК = Специальности.код " +
                                "JOIN Квалификации  ON ЧисленностьОбучающихся.квалификация_ВК = Квалификации.код " +
                                $"WHERE ЧисленностьОбучающихся.год = {year}; " + /*Выбор года подставить аргументом функции*/

                            // Table 4
                            "SELECT id, full_desc FROM Filial; ";




            connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);

            DataSet ds = new DataSet();
            adapter.Fill(ds);



            // Заполнение списка нормативами за этот год.
            List<БазовыеНормативыЗатрат> All_normals = new List<БазовыеНормативыЗатрат>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                All_normals.Add(new БазовыеНормативыЗатрат( Convert.ToInt32( row["id"] ), row["наименование"].ToString()));
            }

            // Выборка из таблицы БНЗСтоимостнойГруппы, значений норматива по полю БНЗ_ВК
            foreach (var normativ in All_normals)
            {
                var norm = from id in ds.Tables[1].AsEnumerable() where Convert.ToInt32( id["БНЗ_ВК"] ) == normativ.id select id;

                foreach (DataRow row in norm)
                {
                    normativ.SetNormativ(new БазовыйНормативЗатратСтоимостнойГруппы(new decimal[] {
                        Convert.ToDecimal(row[1]),
                        Convert.ToDecimal(row[2]),
                        Convert.ToDecimal(row[3]),
                        Convert.ToDecimal(row[4])},
                        row[5].ToString()));
                }

            }

            // Выборка из таблицы ЗначениеКоэффицента. Для сопоставления корректирующих коэффицентов Нормативам.
            foreach (var normativ in All_normals)
            {
                var norm = from coef in ds.Tables[2].AsEnumerable() where Convert.ToInt32(coef["код"]) == normativ.id select coef;


                List<ЗначениеКоэффицента> coef_values = new List<ЗначениеКоэффицента>();
                List<КорректирующиеКоэффиценты> correct_coef = new List<КорректирующиеКоэффиценты>();
                List<КорректирующийКоэффицентБазовогоНорматива> correct_base_normal = new List<КорректирующийКоэффицентБазовогоНорматива>();

                foreach (DataRow row in norm)
                {
                    coef_values.Add(new ЗначениеКоэффицента( Convert.ToDecimal( row[1] ), ""));
                }
                for (int i = 0; i < coef_values.Count; i++)
                {
                    correct_coef.Add(new КорректирующиеКоэффиценты("", "", "", "", false));
                    correct_coef[i].SetValueCoef(coef_values[i]);

                    correct_base_normal.Add(new КорректирующийКоэффицентБазовогоНорматива(""));
                    correct_base_normal[i].SetCorrectCoef(correct_coef[i]);

                    normativ.SetCorrectCoef(correct_base_normal[i]);
                }
                
                
            }          
            

            // Заполнение колекции Численности обучающихся.
            List<TableCountStudent> ListStudent = new List<TableCountStudent>();
            foreach (DataRow item in ds.Tables[3].AsEnumerable())
            {                                
                ListStudent.Add(new TableCountStudent(
                    Convert.ToInt32(item[0]),
                    Convert.ToString(item[1]), 
                    Convert.ToString(item[2]),
                    Convert.ToString(item[3]),

                    Convert.ToInt32(item[4]), 
                    Convert.ToInt32(item[5]), 
                    Convert.ToInt32(item[6]), 
                    Convert.ToInt32(item[7])));
            }


            //// Начало алгоритма.
            
            // Массив для суммирования по специальностям и группам.
            decimal[,] SummOnGroups_AndSpecial = new decimal[4,3];
            int g = 0;

            foreach (var normals in All_normals)
            {
                foreach (var item in normals.GetNormativ())
                {                    
                    foreach (var coef in normals.GetCorrectCoef())
                    {                        
                        foreach (var val in coef.GetCorrectCoef())
                        {                            
                            foreach (var z in val.GetValueCoef())
                            {
                                item.Бакалавриат_Специалитет    = Math.Round(item.Бакалавриат_Специалитет   *= z.Коэффицент, 2);
                                item.Магистратура               = Math.Round(item.Магистратура              *= z.Коэффицент, 2);
                                item.Аспирантура                = Math.Round(item.Аспирантура               *= z.Коэффицент, 2);
                                item.SPO                        = Math.Round(item.SPO                       *= z.Коэффицент, 2);
                            }                            
                        }
                    } 
                    SummOnGroups_AndSpecial[0, g] += item.Аспирантура;
                    SummOnGroups_AndSpecial[1, g] += item.Бакалавриат_Специалитет;
                    SummOnGroups_AndSpecial[2, g] += item.Магистратура;
                    SummOnGroups_AndSpecial[3, g] += item.SPO;
                    g++;
                }
                g = 0; 
            }

            Dictionary<int, string[]> DataReport = new Dictionary<int, string[]>();
            
            int f = 0;
            GridReport.Rows.Clear();

            // Цикл считает Базовые нормативы по каждому филиалу.
            foreach (DataRow filial in ds.Tables[4].AsEnumerable())
            {
                int[,] ArrayOfCountStudents = new int[4,3];
                int r = 0;

                // Коэффицент формы обучения.
                decimal[] coef_priv = { 1, 0.25m, 0.1m };


                decimal Bakalavr = 0m;
                decimal Magistr = 0m;
                decimal Aspirant = 0m;
                decimal SPO = 0m;

                decimal _Bakalavr = 0m;
                decimal _Magistr = 0m;
                decimal _Aspirant = 0m;
                decimal _SPO = 0m;

                decimal SummOnGroup = 0;       // Сумма по группе.
                decimal Summ = 0;              // Сумма по группе с учётом количества студентов и формы обучения.


                // Группировка от суммирование по специальности. Численность обучающихся.
                var table3 = from result in ListStudent
                             where Convert.ToInt32(result.id_filial) == Convert.ToInt32(filial[0])
                             orderby result.Skill ascending
                             group result by result.Skill into newGroup
                             select new
                             {
                                 //k = newGroup.Key, // key - пока не требуется.
                                 och = newGroup.Sum(k => k.ochnoe),
                                 och_zao = newGroup.Sum(y => y.ochno_zaocjnoe),
                                 evening = newGroup.Sum(z => z.zaochnoe)
                             };

                foreach (var item in table3.AsEnumerable())
                {
                    ArrayOfCountStudents[r, 0] = item.och;
                    ArrayOfCountStudents[r, 1] = item.och_zao;
                    ArrayOfCountStudents[r, 2] = item.evening;                    
                    r++;
                }




                // Цикл - по численности обучающихся.
                for (int i = 0; i < 3; i++) //форма
                {
                    for (int j = 0; j < 3; j++) //группа
                    {
                        Aspirant    = Math.Round(SummOnGroups_AndSpecial[0, j], 2) * ArrayOfCountStudents[0, i];
                        Bakalavr    = Math.Round(SummOnGroups_AndSpecial[1, j], 2) * ArrayOfCountStudents[1, i];
                        Magistr     = Math.Round(SummOnGroups_AndSpecial[2, j], 2) * ArrayOfCountStudents[2, i];
                        SPO         = Math.Round(SummOnGroups_AndSpecial[3, j], 2) * ArrayOfCountStudents[3, i];

                        _Bakalavr += Bakalavr;
                        _Magistr += Magistr;
                        _Aspirant += Aspirant;
                        _SPO += SPO;

                        SummOnGroup += Math.Round((Bakalavr + Magistr + Aspirant + SPO) * coef_priv[i], 2);
                    }
                }

                Summ = Math.Round(SummOnGroup * K_equal, 2);

                DataReport.Add(f, new string[] { filial[1].ToString(), Summ.ToString(), _Bakalavr.ToString(),
                                                    _Aspirant.ToString(), _Magistr.ToString(), _SPO.ToString()});
                f++;                
                GridReport.Rows.Add(filial[1], Summ);
                

                //GridReport.Rows.Add("Аспирантура", _Aspirant);
                //GridReport.Rows.Add("Бакалавриат", _Bakalavr);
                //GridReport.Rows.Add("Магистратура", _Magistr);
                //GridReport.Rows.Add("СПО", _SPO);
                
                //// Конец алгоритма.
            }

            return DataReport;

        }

        private void buttonShowReport_Click (object sender, EventArgs e)
        {
            try
            {
                decimal coef = Convert.ToDecimal(textBoxCoef.Text);
                Fill(comboBoxYear.SelectedItem.ToString(), coef);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
                          
        }

        private void buttonExportTo_Click (object sender, EventArgs e)
        {
            try
            {
                decimal coef = Convert.ToDecimal(textBoxCoef.Text);
                Dictionary<int, string[]> table = Fill(comboBoxYear.SelectedItem.ToString(), coef);


                object oMissing = System.Reflection.Missing.Value;
                object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
                string[] spec = new string[] { "Бакалавриат", "Магистратура", "Аспирантура", "Специалитет" };

                _Application WORD = new Word.Application();
                _Document MyDoc = WORD.Documents.Add(Visible: true);
                

                // Заголовок.
                Paragraph p0;
                p0 = MyDoc.Content.Paragraphs.Add(ref oMissing);
                p0.Range.Text = MyDoc.Name;
                p0.Range.InsertParagraphAfter();                

                Paragraph p1;
                p1 = MyDoc.Content.Paragraphs.Add(ref oMissing);
                p1.Range.Text = $"ОБЪЕМ СУБСИДИЙ НА ФИНАНСОВОЕ ОБЕСПЕЧЕНИЕ ВЫПОЛНЕНИЯ ГОСУДАРСТВЕННОГО ЗАДАНИЯ НА {comboBoxYear.SelectedItem.ToString()} ГОД";
                p1.Range.ParagraphFormat.SpaceAfter = 12;
                p1.Range.Font.Bold = 1;
                p1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                p1.Range.Font.Size = 12;
                p1.Range.Font.Name = "Times New Roman";
                p1.Range.InsertParagraphAfter();


                // Таблица.
                Range wrdRng = MyDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                Table wtable = MyDoc.Tables.Add(wrdRng, table.Count + 6, 2);

                wtable.Borders.Enable = 1;

                wtable.Rows.Add();
                wtable.Rows[1].Cells[1].Range.Text = "Расчет нормативных затрат на оказание государственных услуг " +
                                                        "из средств субсидии федерального бюджета с учетом филиалов";
                wtable.Rows[1].Cells[2].Range.Text = "Сумма, руб.";
                wtable.Rows[1].Range.Font.Bold = 0;
                wtable.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;

                int rr = 2;
                foreach (var item in table)
                {
                    //wtable.Rows[rr].Alignment = WdRowAlignment.wdAlignRowLeft;
                    wtable.Rows[rr].Cells[1].Range.Text = item.Value[0].ToString();
                    wtable.Rows[rr].Cells[2].Range.Text = item.Value[1].ToString();
                    rr++;
                    if (rr == 3)
                    {
                        for (int i = 2; i < 6; i++)
                        {
                            wtable.Rows[rr].Cells[1].Range.Text = spec[i - 2];
                            wtable.Rows[rr].Cells[2].Range.Text = item.Value[i].ToString();
                            wtable.Rows[rr].Range.Font.Bold = 0;

                            rr++;
                        }
                    }

                }
                rr++;
                wtable.Rows[rr].Cells[1].Range.Text = "ИТОГО по вузу:";
                wtable.Rows[rr].Cells[2].Range.Text = Convert.ToString(table.Sum(x => Convert.ToDecimal(x.Value[1])));

                try
                {
                    //MyDoc.SaveAs2($"Затраты на {comboBoxYear.SelectedItem.ToString()} год");
                    MyDoc.Save();
                    MyDoc.Close();

                }
                catch (Exception)
                {
                    WORD.Quit();
                }
                finally
                {
                    WORD.Quit();
                    //MessageBox.Show("Файл сохранён");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }

           this.Close();

        }

        private void buttonPrintReport_Click (object sender, EventArgs e)
        {
        }

        private void FormСозданиеОтчёта_Load (object sender, EventArgs e)
        {
            int y = DateTime.Today.Year - 3;
            do
            {
                comboBoxYear.Items.Add(++y);
                
            } while (y != DateTime.Today.Year);
            comboBoxYear.SelectedItem = y;
        }
    }
}
