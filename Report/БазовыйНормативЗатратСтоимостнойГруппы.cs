namespace Report
{
    //Базовый Норматив Затрат Стоимостной Группы
    public class БазовыйНормативЗатратСтоимостнойГруппы
    {
        public СтоимостнаяГруппаКалендарногоГода СтоимостнаяГруппа { get; set; }

        public БазовыйНормативЗатратСтоимостнойГруппы() { }

        public БазовыйНормативЗатратСтоимостнойГруппы(decimal[] values, string year)
        {
            //СтоимостнаяГруппа = group;
            Бакалавриат_Специалитет = values[0];
            Магистратура            = values[1];
            Аспирантура             = values[2];
            SPO                     = values[3];
            Year                    = year;         
            
        }
        public БазовыйНормативЗатратСтоимостнойГруппы(decimal[] values, int id_normativ, int id_group)
        {
            this.id_normativ            = id_normativ;
            this.id_group               = id_group;
            Бакалавриат_Специалитет     = values[0];
            Магистратура                = values[1];
            Аспирантура                 = values[2];
            SPO                         = values[3];
        }

        public СтоимостнаяГруппаКалендарногоГода GetGroup () => СтоимостнаяГруппа;
        
        public decimal Бакалавриат_Специалитет { get; set; }
        public decimal Магистратура { get; set; }
        public decimal Аспирантура { get; set; }
        public decimal SPO { get; set; }
        string Year { get; set; }
        public int id_normativ { get; set; }
        public int id_group { get; set; }
    }
}
