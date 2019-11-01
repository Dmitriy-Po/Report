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
        decimal Магистратура { get; set; }
        decimal Аспирантура { get; set; }
        decimal SPO { get; set; }
        string Year { get; set; }
        
    }
}
