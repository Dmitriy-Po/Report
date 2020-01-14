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

        const int RoundedNumber = 2;

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

        public int CurrentYear = 2020;

        /*
         * ListGroupsAndNormals - список стоимостных групп с подсчитанными норамтивами, 
         * относительно форм обучения и без форм обучения, без студентов - инвалидов.
         * 
         * ListGroupsAndNormals_inv - список стоимостных групп с подсчитанными норамтивами,
         * относительно форм обучения и с учётом студентов - инвалидов.
         * */
        Dictionary<int, Dictionary<int, decimal[,]>> ListGroupsAndNormals;
        Dictionary<int, Dictionary<int, decimal[,]>> ListGroupsAndNormals_inv;


        public Algorithm2 (int y) {
            CurrentYear = y;
        }

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
                                "ЧисленностьОбучающихся.студент_инвалид, " +
                                "Filial.full_desc "+
                                "FROM ЧисленностьОбучающихся " +
                                "JOIN Filial ON ЧисленностьОбучающихся.стуктурное_подразделение_ВК = Filial.id " +
                                "JOIN Специальности ON ЧисленностьОбучающихся.специальность_ВК = Специальности.код " +
                                "JOIN Квалификации  ON ЧисленностьОбучающихся.квалификация_ВК = Квалификации.код " +
                                $"WHERE ЧисленностьОбучающихся.год LIKE '{CurrentYear}%'; ";

            DataTable table = GetTable(query);
            foreach (DataRow item in table.Rows)
            {
                ListStudent.Add(new TableCountStudent(
                                    item["full_desc"].ToString(),
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
                            Convert.ToDecimal(row["Бакалавриат"]),
                            Convert.ToDecimal(row["Магистратура"]),
                            Convert.ToDecimal(row["Аспирантура"]),
                            Convert.ToDecimal(row["СПО"])
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
                                NormalsWithoutStdInv[i] = Math.Round(NormalsWithoutStdInv[i], RoundedNumber, MidpointRounding.AwayFromZero);
                            }
                        }
                        // Отбор КК студентов-инвалидов.
                        else if (koef.std_inv == true)
                        {
                            for (int i = 0; i < NormalsWithStdInv.Count(); i++)
                            {
                                NormalsWithStdInv[i] *= koef.value;
                                NormalsWithStdInv[i] = Math.Round(NormalsWithStdInv[i], RoundedNumber, MidpointRounding.AwayFromZero);
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
                                            bnz_without_inv[0, i]  = Math.Round(bnz_without_inv[0, i], RoundedNumber, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                    break;
                                case ID_FORM_O_Z:
                                    {
                                        for (int i = 0; i < bnz_without_inv.GetLength(1); i++)
                                        {
                                            bnz_without_inv[1, i] = item.value * NormalsWithoutStdInv[i];
                                            bnz_without_inv[1, i] = Math.Round(bnz_without_inv[1, i], RoundedNumber, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                    break;
                                case ID_FORM_Z:
                                    {
                                        for (int i = 0; i < bnz_without_inv.GetLength(1); i++)
                                        {
                                            bnz_without_inv[2, i] = item.value * NormalsWithoutStdInv[i];
                                            bnz_without_inv[2, i] = Math.Round(bnz_without_inv[2, i], RoundedNumber, MidpointRounding.AwayFromZero);
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
                                            bnz_with_inv[0, i] = Math.Round(bnz_with_inv[0, i], RoundedNumber, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                    break;
                                case ID_FORM_O_Z:
                                    {
                                        for (int i = 0; i < bnz_with_inv.GetLength(1); i++)
                                        {
                                            bnz_with_inv[1, i] = item.value * NormalsWithStdInv[i];
                                            bnz_with_inv[1, i] = Math.Round(bnz_with_inv[1, i], RoundedNumber, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                    break;
                                case ID_FORM_Z:
                                    {
                                        for (int i = 0; i < bnz_with_inv.GetLength(1); i++)
                                        {
                                            bnz_with_inv[2, i] = item.value * NormalsWithStdInv[i];
                                            bnz_with_inv[2, i] = Math.Round(bnz_with_inv[2, i], RoundedNumber, MidpointRounding.AwayFromZero);
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
        public Dictionary<string, decimal[]> MultiplayCountStudent ()
        {            
            // Словарь подразделений со списком затрат по квалификациям.
            Dictionary<string, decimal[]> SummOnFilial = new Dictionary<string, decimal[]>();
            // Массив для суммирования по квалификациям.
            decimal[,] SummOnSkill_buff;
            decimal[] SummOnSkill = new decimal[4];

            List<TableCountStudent> ListStudent_sel = new List<TableCountStudent>();

            // Сначала, отберём специальности, которые входят в стоимостные группы.
            foreach (var spec in spec_group.Select(x => x.id_spec))
            {
                foreach (var list in ListStudent)
                {
                    // Если специальность входяит в стоимостную группу, тогда её добавляем в список.
                    if (spec == list.Special_id)
                    {
                        ListStudent_sel.Add(list);
                    }
                }
            }

            // Затем, выберем подразделения, которые присутствуют в списке численности.
            var filials = ListStudent_sel.GroupBy(x => x.id_filial).Select(x => x);

            
            

            // Цикл по найденным подразделениям.
            foreach (var filial in filials)
            {
                int skill = 0;
                int skill_inv = 0;
                SummOnSkill_buff = new decimal[3, 4];
                SummOnSkill = new decimal[4];
                // Цикл по специальностям в подразделении.
                foreach (var special in filial)/*отобрать специальности, которые входят в стоимостную группу*/
                {
                    

                    // отбор специальностей без студентов - инвалидов.
                    if (special.student_inv == false)
                    {
                        foreach (var gr in spec_group.Where(x => x.id_spec == special.Special_id))
                        {
                            // Умножение нормативов на численность учащихся.
                            foreach (var group in ListGroupsAndNormals.Where(x => x.Key == gr.id_group))
                            {
                                foreach (KeyValuePair<int, decimal[,]> normal in group.Value)
                                {
                                    switch (special.Skill_id)
                                    {
                                        case ID_BAKALAVR:
                                            {
                                                CalculateNormsAndCounts(SummOnSkill_buff, 0, new int[] {
                                                    special.ochnoe,
                                                    special.ochno_zaocjnoe,
                                                    special.zaochnoe }, normal.Value, 0);
                                            }
                                            break;
                                        case ID_MAGISTR:
                                            {
                                                CalculateNormsAndCounts(SummOnSkill_buff, 1, new int[] {
                                                    special.ochnoe,
                                                    special.ochno_zaocjnoe,
                                                    special.zaochnoe }, normal.Value, 1);
                                            }
                                            break;
                                        case ID_ASPIRANT:
                                            {
                                                CalculateNormsAndCounts(SummOnSkill_buff, 2, new int[] {
                                                    special.ochnoe,
                                                    special.ochno_zaocjnoe,
                                                    special.zaochnoe }, normal.Value, 2);
                                            }
                                            break;
                                        case ID_SPO:
                                            {
                                                CalculateNormsAndCounts(SummOnSkill_buff, 3, new int[] {
                                                    special.ochnoe,
                                                    special.ochno_zaocjnoe,
                                                    special.zaochnoe }, normal.Value, 3);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    
                                }
                            } 
                        }
                    }
                    
                    // отбор специальностей студентов - инвалидов.
                    else if (special.student_inv == true)
                    {
                        foreach (var gr in spec_group.Where(x => x.id_spec == special.Special_id))
                        {
                            // Умножение нормативов на численность учащихся.
                            foreach (var group in ListGroupsAndNormals_inv.Where(x => x.Key == gr.id_group))
                            {
                                foreach (KeyValuePair<int, decimal[,]> normal in group.Value)
                                {
                                    switch (special.Skill_id)
                                    {
                                        case ID_BAKALAVR:
                                            {
                                                CalculateNormsAndCounts(SummOnSkill_buff, 0, new int[] {
                                                    special.ochnoe,
                                                    special.ochno_zaocjnoe,
                                                    special.zaochnoe }, normal.Value, 0);
                                            }
                                            break;
                                        case ID_MAGISTR:
                                            {
                                                CalculateNormsAndCounts(SummOnSkill_buff, 1, new int[] {
                                                    special.ochnoe,
                                                    special.ochno_zaocjnoe,
                                                    special.zaochnoe }, normal.Value, 1);
                                            }
                                            break;
                                        case ID_ASPIRANT:
                                            {
                                                CalculateNormsAndCounts(SummOnSkill_buff, 2, new int[] {
                                                    special.ochnoe,
                                                    special.ochno_zaocjnoe,
                                                    special.zaochnoe }, normal.Value, 2);
                                            }
                                            break;
                                        case ID_SPO:
                                            {
                                                CalculateNormsAndCounts(SummOnSkill_buff, 3, new int[] {
                                                    special.ochnoe,
                                                    special.ochno_zaocjnoe,
                                                    special.zaochnoe }, normal.Value, 3);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    
                }
                                
                for (int i = 0; i < SummOnSkill.GetLength(0); i++)
                {
                    SummOnSkill[i] += SummOnSkill_buff[0, i] + SummOnSkill_buff[1, i] + SummOnSkill_buff[2, i];
                }
                // Добавление в коллекцию подразделений.
                SummOnFilial.Add(filial.FirstOrDefault().Filial, SummOnSkill);                
                SummOnSkill_buff = null;
                SummOnSkill = null;
            }
            return SummOnFilial;
        }
        void CalculateNormsAndCounts (decimal[,] summ, int skill, int[] count, decimal[,] mass, int sk)
        {            
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                summ[i, sk] += (mass[i, sk] * count[i]);
                summ[i, sk] = Math.Round(summ[i, sk], RoundedNumber, MidpointRounding.AwayFromZero);
            }
            mass = null;
            count = null;
            //summ = null;
        }
    }
}
