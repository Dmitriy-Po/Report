namespace Report
{
    public class TableCountStudent
    {
        public TableCountStudent () { }
        public TableCountStudent (int filial, string special, string skill, int ochnaya, int ocjno_zaochnaya, int zaochnaya, int year)
        {
            Filial = filial.ToString();
            Special = special;
            Skill = skill;
            ochnoe = ochnaya;
            ochno_zaocjnoe = ocjno_zaochnaya;
            zaochnoe = zaochnaya;
            this.year = year;
        }


        public int id { get; set; }

        public int year { get; set; }

        public int ochnoe { get; set; }
        public int zaochnoe { get; set; }
        public int ochno_zaocjnoe { get; set; }

        public bool student_inv { get; set; }

        public string Filial { get; set; }
        public string Special { get; set; }
        public string Skill { get; set; }     

    }
}
