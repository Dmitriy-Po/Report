namespace Report
{
    public class TableCountStudent
    {
        public TableCountStudent () { }
        public TableCountStudent (int id_filial, string filial, string special, string skill, int ochnaya, int ocjno_zaochnaya, int zaochnaya, int year, bool std_inv)
        {
            this.id_filial = id_filial;
            Filial = filial;
            Special = special;
            Skill = skill;
            ochnoe = ochnaya;
            ochno_zaocjnoe = ocjno_zaochnaya;
            zaochnoe = zaochnaya;
            this.year = year;
            this.student_inv = std_inv;
        }
        public TableCountStudent (string filial, int id_filial, int special, int skill, int ochnaya, int ocjno_zaochnaya, int zaochnaya, int year, bool std_inv)
        {
            this.Filial = filial;
            this.id_filial = id_filial;
            this.Special_id = special;
            this.Skill_id = skill;
            this.ochnoe = ochnaya;
            this.ochno_zaocjnoe = ocjno_zaochnaya;
            this.zaochnoe = zaochnaya;
            this.year = year;
            this.student_inv = std_inv;
        }

        public int id { get; set; }
        public int id_filial { get; set; }

        public int year { get; set; }

        public int ochnoe { get; set; }
        public int zaochnoe { get; set; }
        public int ochno_zaocjnoe { get; set; }

        public bool student_inv { get; set; }

        public string Filial { get; set; }
        public string Special { get; set; }
        public int Special_id { get; set; }
        public string Skill { get; set; }
        public int Skill_id { get; set; }     

    }
}
