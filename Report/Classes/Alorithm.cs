﻿using Report.Forms;
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
        const int ID_BAKALAVR = 12;
        const int ID_MAGISTR = 14;
        const int ID_ASPIRANT = 13;
        const int ID_SPO = 10;

        const int ID_FORM_OCH = 11;
        const int ID_FORM_O_Z = 9;
        const int ID_FORM_Z = 12;

        decimal[,] ssmm;
        decimal[,] SummOnNormals;
        SQLiteDataAdapter Adapter;
        DataSet Dataset;
        SQLiteConnection connection;


        public Algorithm () { }



        bool CheckSkill (int skill, DataTable table)
        {
            return false;
        }
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
                                "SELECT БазовыйНормативЗатрат.код, ЗначениеКоэффицента.Значение, ФормаОбучения_ВК " +
                                "FROM КоррКоэффицентБазовогоНорматива "+
                                "JOIN ЗначениеКоэффицента ON "+
                                "КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК = ЗначениеКоэффицента.Корректирующие_ВК "+
                                "JOIN БазовыйНормативЗатрат ON БазовыйНормативЗатрат.код = КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК; ";

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
                    Convert.ToInt32(row[0]), Convert.ToDecimal(row[1]), Convert.ToInt32(row[2])));
            }
            #endregion
            // 3 - Стоимостные группы, 4 - Квалификации, 3 - Формы обучения.            
            
            Dictionary<int, decimal[,]> SON_g = new Dictionary<int, decimal[,]>();


            foreach (var group in groups)
            {
                SummOnNormals = new decimal[4, 3];
                foreach (var norm in bnzsg.Where(x => x.id_group == group.id_group))
                {
                    ssmm = new decimal[4, 3];
                    var kk_bnz = kkbn.Where(k => k.id_bnz == norm.id_normativ).Select(k => k);

                    // Применение корректирующих коэфицентов, без формы обучения.
                    foreach (var item in kk_bnz.Where(f => f.id_form_education == 0).Select(f => f))
                    {
                        norm.Бакалавриат_Специалитет    *= item.value;
                        norm.Магистратура               *= item.value;
                        norm.Аспирантура                *= item.value;
                        norm.SPO                        *= item.value;
                    }

                    // Заполнение массива значениями базового норматива.
                    for (int i = 0; i < ssmm.GetLength(1); i++)
                    {
                        ssmm[0, i] = norm.Бакалавриат_Специалитет;
                        ssmm[1, i] = norm.Магистратура;
                        ssmm[2, i] = norm.Аспирантура;
                        ssmm[3, i] = norm.SPO;
                    }

                    // Применение корректирующих коэфицентов, относительно формы обучения.
                    foreach (var item in kk_bnz)
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

                    // Суммирование нормативов по всем квалификациям.
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            SummOnNormals[i, j] += ssmm[i, j];
                        }
                    }                    
                    ssmm = null;
                }
                SON_g.Add(group.id_group, SummOnNormals);
                SummOnNormals = null;
            }
            /**/
            
            Dictionary<string, decimal[]> SummOfFilial = new Dictionary<string, decimal[]>();

            // Цикл по каждому филиалу и группам.
            foreach (var filial in Dataset.Tables[5].AsEnumerable())
            {
                decimal[] summ = new decimal[4];
                

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
                    // Отбор БНЗ, которые относятся в стоимостную группу.
                    //foreach (var normal in SON_g.Where(x => x.Key == group.id_group).Select(x => x.Value))
                    //{
                    //    normal;
                    //}

                    /*Magic*/
                    foreach (var list in spec_list)
                    {
                        foreach (var normal in SON_g.Where(x => x.Key == group.id_group).Select(x => x.Value))
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
                SummOfFilial.Add(filial["full_desc"].ToString(), summ);
                summ = null;
            }

            

            //foreach (var filial in Dataset.Tables[5].AsEnumerable())
            //{
            //    // Отбор по филиалам среди всего списка численности (id).
            //    var list_student = from l in ListStudent
            //                       where l.id_filial == Convert.ToInt32(filial[0])
            //                       select l;

            //    // Отбор квалификации среди всего списка текущего филиала.
            //    foreach (var skill in Dataset.Tables[6].AsEnumerable())
            //    {
            //        var spec_list = from s in list_student
            //                        where s.Skill_id == Convert.ToInt32(skill[0])
            //                        select s;

            //        foreach (var item in spec_list)
            //        {
            //            //item.
            //        }
                    
            //    }
                
            //}

            //var List = from l in ListStudent
            //           group l by l.id_filial into newgroup
            //           select new
            //           {
            //               ochnoe = newgroup.Sum(s => s.ochnoe),
            //               och_zao = newgroup.Sum(s => s.ochno_zaocjnoe),
            //               zao = newgroup.Sum(s => s.zaochnoe)
            //           };   
            
            //    foreach (int filial in ListStudent.Select(x => x.id_filial))
            //    {                
            //        foreach (var group in spec_group)
            //        {
            //            var bnz = bnzsg.Where(x => x.id_group == group.id_group);
            //            var spc = ListStudent.FindAll(x => x.Special_id == group.id_spec);

            //            foreach (var item in bnz)
            //            {
            //                foreach (var s in spc)
            //                {
            //                    item.Бакалавриат_Специалитет *= (s.ochnoe + s.ochno_zaocjnoe + s.zaochnoe);
            //                    item.Аспирантура *= (s.ochnoe + s.ochno_zaocjnoe + s.zaochnoe);
            //                    item.Магистратура *= (s.ochnoe + s.ochno_zaocjnoe + s.zaochnoe);
            //                    item.SPO *= (s.ochnoe + s.ochno_zaocjnoe + s.zaochnoe);
            //                }
            //            }                       
            //        }
            //    }
                
            
            connection.Close();
            return SummOfFilial;
            //    FormСозданиеОтчёта form = new FormСозданиеОтчёта();
            //form.GridReport.DataSource = bnzsg;
                //foreach (var item in bnzsg)
                //{
                //    form.GridReport.Rows.Add("Магистарутра", item.Магистратура);
                //    form.GridReport.Rows.Add(item.Бакалавриат_Специалитет);
                //    form.GridReport.Rows.Add(item.Аспирантура);
                //    form.GridReport.Rows.Add(item.SPO);
                //}
                /*здесь применить group by ListStudent*/

                //foreach (var item in ListStudent)
                //{
                    //item.ochnoe * 
                //}
                //foreach (var item in spec_group)
                //{
                //    var listcount = ListStudent.FindAll(x => x.Special_id == item.id_spec);

                //    var bnz = bnzsg.Where(g => g.id_group == group.id_group).Select(x => x);
                //    foreach (var normativ in bnz)
                //    {
                //        // Значение базового норматива умножается на 
                //        // количество человек относительно квалификации.
                //        foreach (var fil in listcount)
                //        {
                //            normativ.Бакалавриат_Специалитет *= (fil.ochnoe + fil.ochno_zaocjnoe + fil.zaochnoe);
                //            normativ.Магистратура *= (fil.ochnoe + fil.ochno_zaocjnoe + fil.zaochnoe);
                //            normativ.Аспирантура *= (fil.ochnoe + fil.ochno_zaocjnoe + fil.zaochnoe);
                //            normativ.SPO *= (fil.ochnoe + fil.ochno_zaocjnoe + fil.zaochnoe);


                //        }
                //    }
                //}

            //}
        }
    }
}
