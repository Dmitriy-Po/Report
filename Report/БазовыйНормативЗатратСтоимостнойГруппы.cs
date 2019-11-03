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
        public СтоимостнаяГруппаКалендарногоГода GetGroup () => СтоимостнаяГруппа;
        
        public decimal Бакалавриат_Специалитет { get; set; }
        public decimal Магистратура { get; set; }
        public decimal Аспирантура { get; set; }
        public decimal SPO { get; set; }
        string Year { get; set; }
        
    }
}
