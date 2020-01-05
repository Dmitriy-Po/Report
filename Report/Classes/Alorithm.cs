using Report.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Classes

{
    
    class Algorithm
    {
        // Значения констант отвечают за правильное расперделение квалификаций и форм обучения.
        // Значения соответсвутют колючевым значениям в базе данных.
        const int ID_BAKALAVR = 12;
        const int ID_MAGISTR = 14;
        const int ID_ASPIRANT = 13;
        const int ID_SPO = 10;

        const int ID_FORM_OCH = 11;
        const int ID_FORM_O_Z = 9;
        const int ID_FORM_Z = 12;

        decimal[,] ssmm;
        decimal[,] SummOnNormals;

        decimal[,] ar_sum_inv;
        decimal[,] ar_inv;

        SQLiteDataAdapter Adapter;
        DataSet Dataset;
        SQLiteConnection connection;


        public Algorithm () { }

                
        public Dictionary<string, decimal[]> Calculate (int year)
        {
            SQliteDB DB = new SQliteDB();
            connection = new SQLiteConnection(DB.ConnectionDB);
            //connection.Open();
            //{
                connection.Open();
            #region Заполнение коллекций
            string query = 
                                // Table 0.
                                "SELECT Filial.id as 'Филиал_код', " +                                
                                "Специальности.код as 'Специальность', " +
                                "Квалификации.код as 'Квалификация',  " +
                                "ЧисленностьОбучающихся.очное as 'Очное', " +
                                "ЧисленностьОбучающихся.очно_заочное as 'Очно-заочное', " +
                                "ЧисленностьОбучающихся.заочное as 'Заочное', " +
                                "SUBSTR(ЧисленностьОбучающихся.год, 1, 4) as 'год', " +
                                "ЧисленностьОбучающихся.студент_инвалид " +
                                "FROM ЧисленностьОбучающихся " +
                                "JOIN Filial ON ЧисленностьОбучающихся.стуктурное_подразделение_ВК = Filial.id " +
                                "JOIN Специальности ON ЧисленностьОбучающихся.специальность_ВК = Специальности.код " +
                                "JOIN Квалификации  ON ЧисленностьОбучающихся.квалификация_ВК = Квалификации.код " +
                                $"WHERE ЧисленностьОбучающихся.год LIKE '{year}%'; " +
                                
                                // Table 1.
                                "SELECT ЗначениеКоэффицента.Значение FROM ЗначениеКоэффицента "+
                                "JOIN КорректирующиеКоэффиценты ON КорректирующиеКоэффиценты.код = ЗначениеКоэффицента.Корректирующие_ВК "+
                                "JOIN КоррКоэффицентБазовогоНорматива ON КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК = ЗначениеКоэффицента.Корректирующие_ВК "+
                                $"WHERE КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК = 75; " +

                                // Table 2.
                                $"SELECT * FROM БНЗСтоимостнойГруппы WHERE БНЗСтоимостнойГруппы.КалендарныйГод LIKE '{year}%'; " +

                                // Table 3.
                                $"SELECT СтоимостнаяГруппаКалГода.код, Наименование FROM СтоимостнаяГруппаКалГода WHERE СтоимостнаяГруппаКалГода.КалендарныйГод LIKE '{year}%'; " +
                                
                                // Table 4.
                                $"SELECT * FROM СпециальностьСтоимостнойГруппы; " +
                                
                                // Table 5.
                                "SELECT id, full_desc FROM Filial; " +

                                // Table 6.
                                "SELECT Квалификации.код FROM Квалификации; " +

                                // Table 7.
                                "SELECT БазовыйНормативЗатрат.код, ЗначениеКоэффицента.Значение, ФормаОбучения_ВК, КорректирующиеКоэффиценты.СтудентИнвалид " +
                                "FROM КоррКоэффицентБазовогоНорматива "+
                                "JOIN ЗначениеКоэффицента ON "+
                                "КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК = ЗначениеКоэффицента.Корректирующие_ВК "+
                                "JOIN БазовыйНормативЗатрат ON БазовыйНормативЗатрат.код = КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК "+
                                "JOIN КорректирующиеКоэффиценты ON КорректирующиеКоэффиценты.код = КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК; ";

                Adapter = new SQLiteDataAdapter(query, connection);
                Dataset = new DataSet();
                Adapter.Fill(Dataset);


                // Заполнение колекции Численности обучающихся.
                List<TableCountStudent> ListStudent = new List<TableCountStudent>();
                foreach (DataRow item in Dataset.Tables[0].AsEnumerable())
                {
                    ListStudent.Add(new TableCountStudent(
                                        Convert.ToInt32(item[0]),                                        
                                        Convert.ToInt32(item[1]),
                                        Convert.ToInt32(item[2]),

                                        Convert.ToInt32(item[3]),
                                        Convert.ToInt32(item[4]),
                                        Convert.ToInt32(item[5]),
                                        Convert.ToInt32(item[6]), 
                                        Convert.ToBoolean(item[7])
                                   ));
                }
                // Конец заполнения коллекции Численности обучающихся.

                // Заполнение коллекции Базовых нормативвов затрат стоимостной группы.
                List<БазовыйНормативЗатратСтоимостнойГруппы> bnzsg = new List<БазовыйНормативЗатратСтоимостнойГруппы>();                

                foreach (DataRow row in Dataset.Tables[2].AsEnumerable())
                {
                    bnzsg.Add(new БазовыйНормативЗатратСтоимостнойГруппы(
                        new decimal[] {
                            Convert.ToInt32(row["Бакалавриат"]),
                            Convert.ToInt32(row["Магистратура"]),
                            Convert.ToInt32(row["Аспирантура"]),
                            Convert.ToInt32(row["СПО"])
                        },
                        Convert.ToInt32(row["БНЗ_ВК"]),
                        Convert.ToInt32(row["СтоимостнаяГруппаКалГода_ВК"])
                        ));
                }            
                // Конец заполнения коллекции Базовых нормативвов затрат стоимостной группы.

                // Заполнение списка групп за текущий год.
                List<СтоимостнаяГруппаКалендарногоГода> groups = new List<СтоимостнаяГруппаКалендарногоГода>();
                foreach (DataRow row in Dataset.Tables[3].AsEnumerable())
                {
                    groups.Add(new СтоимостнаяГруппаКалендарногоГода(
                        Convert.ToInt32(row[0]),
                        Convert.ToString(row[1])
                        ));
                }
                // конец заполнения списка групп на текущий год.

                // Заполнение коллекции стоимостных групп.
                List<СпециальностьСтоимостнойГруппы> spec_group = new List<СпециальностьСтоимостнойГруппы>();
                foreach (DataRow row in Dataset.Tables[4].AsEnumerable())
                {
                    spec_group.Add(new СпециальностьСтоимостнойГруппы(
                        Convert.ToInt32(row[0]), Convert.ToInt32(row[1])));
                }
            // Конец заполнения коллекции стоимостных групп.
            
            /**/
            List<КорректирующийКоэффицентБазовогоНорматива> kkbn = new List<КорректирующийКоэффицентБазовогоНорматива>();
            foreach (DataRow row in Dataset.Tables[7].AsEnumerable())
            {
                kkbn.Add(new КорректирующийКоэффицентБазовогоНорматива(
                    Convert.ToInt32(row[0]), Convert.ToDecimal(row[1]), Convert.ToInt32(row[2]), Convert.ToBoolean(row[3])));
            }
            #endregion


            // 3 - Стоимостные группы, 4 - Квалификации, 3 - Формы обучения. 
            Dictionary<int, decimal[,]> SON_g = new Dictionary<int, decimal[,]>();
            Dictionary<int, decimal[,]> SUM_inv = new Dictionary<int, decimal[,]>();

            int z = 0; // группа.
            foreach (var group in groups)
            {
                SummOnNormals = new decimal[4, 3];                

                foreach (var norm in bnzsg.Where(x => x.id_group == group.id_group))
                {
                    ssmm = new decimal[4, 3];
                    ar_inv = new decimal[4, 3];
                    ar_sum_inv = new decimal[4, 3];

                    

                    ar_inv[0, z] = norm.Бакалавриат_Специалитет;
                    ar_inv[1, z] = norm.Магистратура;
                    ar_inv[2, z] = norm.Аспирантура;
                    ar_inv[3, z] = norm.SPO;


                    var kk_bnz = kkbn.Where(k => k.id_bnz == norm.id_normativ).Select(k => k);


                    // Применение корректирующих коэфицентов, без формы обучения.
                    z = 0;
                    foreach (var item in kk_bnz.Where(f => f.id_form_education == 0).Select(f => f))
                    {
                        if (item.std_inv == false)
                        {
                            norm.Бакалавриат_Специалитет    *= item.value;
                            norm.Магистратура               *= item.value;
                            norm.Аспирантура                *= item.value;
                            norm.SPO                        *= item.value;
                        }
                        else
                        {
                            ar_inv[0, z] = item.value;
                            ar_inv[1, z] = item.value;
                            ar_inv[2, z] = item.value;
                            ar_inv[3, z] = item.value;                            
                        }
                        
                    }

                    // Заполнение массива значениями базового норматива.
                    for (int i = 0; i < ssmm.GetLength(1); i++)
                    {
                        ssmm[0, i] = norm.Бакалавриат_Специалитет;
                        ssmm[1, i] = norm.Магистратура;
                        ssmm[2, i] = norm.Аспирантура;
                        ssmm[3, i] = norm.SPO;

                        ar_sum_inv[0, i] = ar_inv[0, i];
                        ar_sum_inv[1, i] = ar_inv[1, i];
                        ar_sum_inv[2, i] = ar_inv[2, i];
                        ar_sum_inv[3, i] = ar_inv[3, i];
                    }

                    // Применение корректирующих коэфицентов, относительно формы обучения.
                    foreach (var item in kk_bnz)
                    {
                        if (item.std_inv == false)
                        {
                            switch (item.id_form_education)
                            {
                                case ID_FORM_OCH:
                                    {
                                        ssmm[0, 0] *= item.value;
                                        ssmm[1, 0] *= item.value;
                                        ssmm[2, 0] *= item.value;
                                        ssmm[3, 0] *= item.value;
                                    }
                                    break;
                                case ID_FORM_O_Z:
                                    {
                                        ssmm[0, 1] *= item.value;
                                        ssmm[1, 1] *= item.value;
                                        ssmm[2, 1] *= item.value;
                                        ssmm[3, 1] *= item.value;
                                    }
                                    break;
                                case ID_FORM_Z:
                                    {
                                        ssmm[0, 2] *= item.value;
                                        ssmm[1, 2] *= item.value;
                                        ssmm[2, 2] *= item.value;
                                        ssmm[3, 2] *= item.value;
                                    }
                                    break;
                                default:
                                    break;
                            } 
                        }
                        else
                        {
                            switch (item.id_form_education)
                            {
                                case ID_FORM_OCH:
                                    {
                                        ar_sum_inv[0, 0] *= item.value;
                                        ar_sum_inv[1, 0] *= item.value;
                                        ar_sum_inv[2, 0] *= item.value;
                                        ar_sum_inv[3, 0] *= item.value;
                                    }
                                    break;
                                case ID_FORM_O_Z:
                                    {
                                        ar_sum_inv[0, 1] *= item.value;
                                        ar_sum_inv[1, 1] *= item.value;
                                        ar_sum_inv[2, 1] *= item.value;
                                        ar_sum_inv[3, 1] *= item.value;
                                    }
                                    break;
                                case ID_FORM_Z:
                                    {
                                        ar_sum_inv[0, 2] *= item.value;
                                        ar_sum_inv[1, 2] *= item.value;
                                        ar_sum_inv[2, 2] *= item.value;
                                        ar_sum_inv[3, 2] *= item.value;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    // Суммирование нормативов по всем квалификациям.
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            SummOnNormals[i, j] += ssmm[i, j];
                        }
                    }                    
                    ssmm = null;
                    z++;
                }
                SUM_inv.Add(group.id_group, ar_sum_inv);
                SON_g.Add(group.id_group, SummOnNormals);
                SummOnNormals = null;
                
            }
            /**/
            
            Dictionary<string, decimal[]> SummOfFilial = new Dictionary<string, decimal[]>();
            Dictionary<string, decimal[]> SUM_fil_inv = new Dictionary<string, decimal[]>();
            
            // Цикл по каждому филиалу и группам.
            foreach (var filial in Dataset.Tables[5].AsEnumerable())
            {
                decimal[] summ = new decimal[4];
                decimal[] summ_inv = new decimal[4];

                foreach (var group in groups)
                {
                    List<TableCountStudent> spec_list = new List<TableCountStudent>();
                    //List<БазовыйНормативЗатратСтоимостнойГруппы> snormallist = new List<БазовыйНормативЗатратСтоимостнойГруппы>();
                    

                    foreach (var item in spec_group.Where(g => g.id_group == group.id_group))
                    {
                        // Отбор специальностей, которые относятся в стоимостную группу.
                        foreach (var list in ListStudent.Where(x => x.id_filial == Convert.ToInt32(filial["id"])).Select(x => x))
                        {
                            if (item.id_spec == list.Special_id)
                            {
                                spec_list.Add(list);
                            }
                        }
                    }                    

                    /*Magic*/
                    foreach (var list in spec_list)
                    {
                        foreach (var normal in SON_g.Where(x => x.Key == group.id_group).Select(x => x.Value))
                        {
                            if (list.student_inv == false)
                            {
                                switch (list.Skill_id)
                                {
                                    case ID_BAKALAVR:
                                        summ[0] += (list.ochnoe * normal[0, 0]) + (list.ochno_zaocjnoe * normal[0, 1]) + (list.zaochnoe * normal[0, 2]);
                                        break;
                                    case ID_ASPIRANT:
                                        summ[1] += (list.ochnoe * normal[2, 0]) + (list.ochno_zaocjnoe * normal[2, 1]) + (list.zaochnoe * normal[2, 2]);
                                        break;
                                    case ID_MAGISTR:
                                        summ[2] += (list.ochnoe * normal[1, 0]) + (list.ochno_zaocjnoe * normal[1, 1]) + (list.zaochnoe * normal[1, 2]); ;
                                        break;
                                    case ID_SPO:
                                        summ[3] += (list.ochnoe * normal[3, 0]) + (list.ochno_zaocjnoe * normal[3, 1]) + (list.zaochnoe * normal[3, 2]); ;
                                        break;
                                    default:
                                        break;
                                } 
                            }
                        }
                    }
                    foreach (var list in spec_list)
                    {
                        foreach (var normal in SUM_inv.Where(x => x.Key == group.id_group).Select(x => x.Value))
                        {
                            if (list.student_inv == true)
                            {
                                switch (list.Skill_id)
                                {
                                    case ID_BAKALAVR:
                                        summ_inv[0] += (list.ochnoe * normal[0, 0]) + (list.ochno_zaocjnoe * normal[0, 1]) + (list.zaochnoe * normal[0, 2]);
                                        break;
                                    case ID_ASPIRANT:
                                        summ_inv[1] += (list.ochnoe * normal[2, 0]) + (list.ochno_zaocjnoe * normal[2, 1]) + (list.zaochnoe * normal[2, 2]);
                                        break;
                                    case ID_MAGISTR:
                                        summ_inv[2] += (list.ochnoe * normal[1, 0]) + (list.ochno_zaocjnoe * normal[1, 1]) + (list.zaochnoe * normal[1, 2]); ;
                                        break;
                                    case ID_SPO:
                                        summ_inv[3] += (list.ochnoe * normal[3, 0]) + (list.ochno_zaocjnoe * normal[3, 1]) + (list.zaochnoe * normal[3, 2]); ;
                                        break;
                                    default:
                                        break;
                                } 
                            }
                        }
                    }
                }
                //SUM_fil_inv.Add(filial["full_desc"].ToString(), summ_inv);
                for (int i = 0; i < summ.GetLength(0); i++)
                {
                    summ[i] += summ_inv[i];
                }
                SummOfFilial.Add(filial["full_desc"].ToString(), summ);
                summ = null;
            }
                       

            connection.Close();
            return SummOfFilial;            
        }
    }
}
