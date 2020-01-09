using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Classes
{
    class Algorithm2
    {
        // Значения констант отвечают за правильное расперделение квалификаций и форм обучения.
        // Значения констант соответсвутют колючевым значениям в базе данных.
        const int ID_BAKALAVR = 12;
        const int ID_MAGISTR = 14;
        const int ID_ASPIRANT = 13;
        const int ID_SPO = 10;

        const int ID_FORM_OCH = 11;
        const int ID_FORM_O_Z = 9;
        const int ID_FORM_Z = 12;
        
        /* Для запросов к базе данных. */
        DataSet Dataset;
        SQliteDB DataBase;
        SQLiteDataAdapter Adapter;

        /* Коллекции */
        List<TableCountStudent> ListStudent;
        List<СтоимостнаяГруппаКалендарногоГода> groups;
        List<СпециальностьСтоимостнойГруппы> spec_group;
        List<БазовыйНормативЗатратСтоимостнойГруппы> bnzsg;
        List<КорректирующийКоэффицентБазовогоНорматива> kkbn;

        int CurrentYear = 2020;

        /*
         * ListGroupsAndNormals - список стоимостных групп с подсчитанными норамтивами, 
         * относительно форм обучения и без форм обучения, без студентов - инвалидов.
         * 
         * ListGroupsAndNormals_inv - список стоимостных групп с подсчитанными норамтивами,
         * относительно форм обучения и с учётом студентов - инвалидов.
         * */
        Dictionary<int, Dictionary<int, decimal[,]>> ListGroupsAndNormals;
        Dictionary<int, Dictionary<int, decimal[,]>> ListGroupsAndNormals_inv;


        public Algorithm2 () { }

        DataTable GetTable (string select_query)
        {
            DataBase = new SQliteDB();
            Dataset = new DataSet();
            using (SQLiteConnection connection = new SQLiteConnection(DataBase.ConnectionDB))
            {
                connection.Open();
                Adapter = new SQLiteDataAdapter(select_query, connection);
                Adapter.Fill(Dataset);
            }
            return Dataset.Tables[0];
        }
        void Fill_ListStudent ()
        {
            ListStudent = new List<TableCountStudent>();

            string query = "SELECT Filial.id as 'Филиал_код', " +
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
                                $"WHERE ЧисленностьОбучающихся.год LIKE '{CurrentYear}%'; ";

            DataTable table = GetTable(query);
            foreach (DataRow item in table.Rows)
            {
                ListStudent.Add(new TableCountStudent(
                                    Convert.ToInt32(item[0]),
                                    Convert.ToInt32(item[1]),
                                    Convert.ToInt32(item[2]),

                                    Convert.ToInt32(item[3]),
                                    Convert.ToInt32(item[4]),
                                    Convert.ToInt32(item[5]),
                                    Convert.ToInt32(item[6]),
                                    Convert.ToBoolean(item[7])));
            }
        }
        void Fill_ListBNZ ()
        {
            bnzsg = new List<БазовыйНормативЗатратСтоимостнойГруппы>();
            string q = $"SELECT * FROM БНЗСтоимостнойГруппы WHERE БНЗСтоимостнойГруппы.КалендарныйГод LIKE '{CurrentYear}%'; ";
            DataTable table = GetTable(q);
            foreach (DataRow row in table.Rows)
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
        }
        void Fill_Groups ()
        {
            groups = new List<СтоимостнаяГруппаКалендарногоГода>();
            string q = $"SELECT СтоимостнаяГруппаКалГода.код, Наименование FROM СтоимостнаяГруппаКалГода "+
                $"WHERE СтоимостнаяГруппаКалГода.КалендарныйГод LIKE '{CurrentYear}%'; ";
            DataTable table = GetTable(q);
            foreach (DataRow row in table.Rows)
            {
                groups.Add(new СтоимостнаяГруппаКалендарногоГода(
                    Convert.ToInt32(row[0]),
                    Convert.ToString(row[1])
                    ));
            }
        }
        void Fill_SpecGroups ()
        {
            spec_group = new List<СпециальностьСтоимостнойГруппы>();
            string q = $"SELECT * FROM СпециальностьСтоимостнойГруппы; ";
            DataTable table = GetTable(q);
            foreach (DataRow row in table.Rows)
            {
                spec_group.Add(new СпециальностьСтоимостнойГруппы(
                    Convert.ToInt32(row[0]), Convert.ToInt32(row[1])));
            }
        }
        void Fill_KKBN ()
        {
            kkbn = new List<КорректирующийКоэффицентБазовогоНорматива>();
            string q =
                "SELECT БазовыйНормативЗатрат.код, ЗначениеКоэффицента.Значение, ФормаОбучения_ВК, КорректирующиеКоэффиценты.СтудентИнвалид " +
                "FROM КоррКоэффицентБазовогоНорматива " +
                "JOIN ЗначениеКоэффицента ON " +
                "КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК = ЗначениеКоэффицента.Корректирующие_ВК " +
                "JOIN БазовыйНормативЗатрат ON БазовыйНормативЗатрат.код = КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК " +
                "JOIN КорректирующиеКоэффиценты ON КорректирующиеКоэффиценты.код = КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК; ";

            DataTable table = GetTable(q);
            foreach (DataRow row in table.Rows)
            {
                kkbn.Add(new КорректирующийКоэффицентБазовогоНорматива(
                    Convert.ToInt32(row[0]), Convert.ToDecimal(row[1]), Convert.ToInt32(row[2]), Convert.ToBoolean(row[3])));
            }
        }
        void Fill_All_Lists ()
        {
            Fill_Groups();
            Fill_KKBN();
            Fill_ListBNZ();
            Fill_ListStudent();
            Fill_SpecGroups();
        }
        Dictionary<int, Dictionary<int, decimal[]>> AddListGroups ()
        {
            Fill_All_Lists();            
            Dictionary<int, Dictionary<int, decimal[]>> ListGroup = new Dictionary<int, Dictionary<int, decimal[]>>();            

            foreach (var group in groups)
            {
                Dictionary<int, decimal[]> g_norm = new Dictionary<int, decimal[]>();
                foreach (var normativ in bnzsg.Where(x => x.id_group == group.id_group))
                {
                    g_norm.Add(normativ.id_normativ, new decimal[] 
                    
                            // Без формы обучения.
                            { normativ.Бакалавриат_Специалитет,
                            normativ.Магистратура,
                            normativ.Аспирантура,
                            normativ.SPO }                            
                    
                    );
                }
                ListGroup.Add(group.id_group, g_norm);
                g_norm = null; 
            }
            return ListGroup;
        }
        public void MultiplayKK ()
        {
            Dictionary<int, Dictionary<int, decimal[]>> ListGroups = AddListGroups();

            Dictionary<int, decimal[,]> Normal_without_inv;
            Dictionary<int, decimal[,]> Normal_with_inv;

            ListGroupsAndNormals = new Dictionary<int, Dictionary<int, decimal[,]>>();
            ListGroupsAndNormals_inv = new Dictionary<int, Dictionary<int, decimal[,]>>();
            int g = 0;
            #region Расчёт нормативов.
            foreach (var group in ListGroups.Values)
            {
                Normal_without_inv = new Dictionary<int, decimal[,]>();
                Normal_with_inv = new Dictionary<int, decimal[,]>();
                foreach (KeyValuePair<int, decimal[]> norm in group)
                {
                    // Массив нормативов без формы и без инвалидов.
                    decimal[] NormalsWithoutStdInv = new decimal[norm.Value.Count()];
                    norm.Value.CopyTo(NormalsWithoutStdInv, 0);

                    // Массив нормативов без формы c инвалидами.
                    decimal[] NormalsWithStdInv = new decimal[norm.Value.Count()];
                    norm.Value.CopyTo(NormalsWithStdInv, 0);

                    // Отбор КК без формы обучения.
                    foreach (var koef in kkbn.Where(x => x.id_form_education == 0 && x.id_bnz == norm.Key))
                    {
                        // Отбор КК без студентов-инвалидов.
                        if (koef.std_inv == false)
                        {
                            for (int i = 0; i < NormalsWithoutStdInv.Count(); i++)
                            {
                                NormalsWithoutStdInv[i] *= koef.value;
                            }
                        }
                        // Отбор КК студентов-инвалидов.
                        else if (koef.std_inv == true)
                        {
                            for (int i = 0; i < NormalsWithStdInv.Count(); i++)
                            {
                                NormalsWithStdInv[i] *= koef.value;
                            }
                        }
                    }

                    decimal[,] bnz_without_inv = new decimal[3, 4];
                    decimal[,] bnz_with_inv = new decimal[3, 4];

                    for (int i = 0; i < bnz_without_inv.GetLength(1); i++)
                    {
                        for (int j = 0; j < bnz_without_inv.GetLength(0); j++)
                        {
                            bnz_without_inv[j, i] = NormalsWithoutStdInv[i];
                            bnz_with_inv[j, i] = NormalsWithStdInv[i];
                        }
                    }

                    //Отбор КК относительно формы обучения.
                    foreach (var item in kkbn.Where(x => x.id_form_education != 0 && x.id_bnz == norm.Key))
                    {
                        // Отбор КК без студентов-инвалидов относительно формы обучения.
                        if (item.std_inv == false)
                        {
                            switch (item.id_form_education)
                            {
                                case ID_FORM_OCH:
                                    {
                                        for (int i = 0; i < bnz_without_inv.GetLength(1); i++)
                                        {
                                            bnz_without_inv[0, i] = item.value * NormalsWithoutStdInv[i]; 
                                        }
                                    }
                                    break;
                                case ID_FORM_O_Z:
                                    {
                                        for (int i = 0; i < bnz_without_inv.GetLength(1); i++)
                                        {
                                            bnz_without_inv[1, i] = item.value * NormalsWithoutStdInv[i];
                                        }
                                    }
                                    break;
                                case ID_FORM_Z:
                                    {
                                        for (int i = 0; i < bnz_without_inv.GetLength(1); i++)
                                        {
                                            bnz_without_inv[2, i] = item.value * NormalsWithoutStdInv[i];
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        // Отбор КК студентов-инвалидов относительно формы обучения.
                        else if (item.std_inv == true)
                        {
                            switch (item.id_form_education)
                            {
                                case ID_FORM_OCH:
                                    {
                                        for (int i = 0; i < bnz_with_inv.GetLength(1); i++)
                                        {
                                            bnz_with_inv[0, i] = item.value * NormalsWithStdInv[i];
                                        }
                                    }
                                    break;
                                case ID_FORM_O_Z:
                                    {
                                        for (int i = 0; i < bnz_with_inv.GetLength(1); i++)
                                        {
                                            bnz_with_inv[1, i] = item.value * NormalsWithStdInv[i];
                                        }
                                    }
                                    break;
                                case ID_FORM_Z:
                                    {
                                        for (int i = 0; i < bnz_with_inv.GetLength(1); i++)
                                        {
                                            bnz_with_inv[2, i] = item.value * NormalsWithStdInv[i];
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    Normal_without_inv.Add(norm.Key, bnz_without_inv);
                    Normal_with_inv.Add(norm.Key, bnz_with_inv);
                }
                ListGroupsAndNormals.Add(ListGroups.Keys.ElementAt(g), Normal_without_inv);
                ListGroupsAndNormals_inv.Add(ListGroups.Keys.ElementAt(g), Normal_with_inv);
                Normal_without_inv = null;
                Normal_with_inv = null;
                g++;
            }
#endregion
        }
    }
}
