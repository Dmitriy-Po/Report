using System;
using Report.Classes;
using System.Windows.Forms;
using System.Linq;
using System.Data.SQLite;

namespace Report
{
    public partial class Form_КК_Добавление : Form
    {
        public Form_КК_Добавление ()
        {
            InitializeComponent();
        }
        public DataGridViewRow DataRow { get; set; }




        void InsertRecord ()
        {
            //Функция добавляет запись в базу данных
            SQliteDB db = new SQliteDB();
            int[] id = new int[2];

            string[] colums = { "Наименование", "ПолноеНаименование", "Комментарий", "Уточнение", "СтудентИнвалид" };
            object[] values = { textBoxDesc.Text, textBoxFullDesc.Text, textBoxComment.Text, textBoxDetail.Text, Convert.ToInt16(checkBoxStdInv.Checked) };

            db.Insert_New("КорректирующиеКоэффиценты",
                string.Join(", ", colums),
                string.Join("', '", values));

            #region получение внешних ключей
            SQLiteDataReader reader = db.Select_any_query("SELECT MAX(ФормаОбучения.код) as f, MAX(КорректирующиеКоэффиценты.код) as k" +
                    " FROM ФормаОбучения, КорректирующиеКоэффиценты");

            while (reader.Read())
            {
                id[0] = reader.GetInt32(0);
                id[1] = reader.GetInt32(1);
            }
            reader.Close();
            db.ConnectionDB.Close();

            #endregion
            string[] val = { Math.Round( Convert.ToDecimal(textBoxCoeff.Text), 2 ).ToString(),
                Convert.ToString(comboBoxYear.SelectedItem)+"-01-01", id[1].ToString(), id[0].ToString() };

            val[0] = val[0].Replace(",", "."); //замена символа запятой на точку, по формату

            string[] col = { "Значение", "КаледндарныйГод", "Корректирующие_ВК", "ФормаОбучения_ВК" };

            db.Insert_New("ЗначениеКоэффицента",
                string.Join(", ", col),
                string.Join("', '", val));
        }
        void IsDuplicate ()
        {
            FormКоэффиценты fk = new FormКоэффиценты();

            var duplicat_id = fk.ListCoef.Where(
                x => x.Наименование.Equals(textBoxDesc.Text, StringComparison.OrdinalIgnoreCase) &&
                     x.GetForm().Equals(comboBoxFormEducation.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase) &&
                     x.СтудентИнвалид.Equals(checkBoxStdInv.Checked) &&
                     x.GetYear().Equals(comboBoxYear.SelectedItem) &&
                     x.GetCoef() == Math.Round(Convert.ToDecimal(textBoxCoeff.Text), 2))
                     .Select(x => x.id);          
            //сделать проверку на дубликаты
            duplicat_id.ElementAt(0);
        }

        private void buttonSaveAndClose_Click (object sender, EventArgs e)
        {
            IsDuplicate();
        }

        private void Form_КК_Добавление_Load(object sender, EventArgs e)
        {
            // заполнение combobox данными из табилцы Форма обучения, Даты.
            FormКоэффиценты c = new FormКоэффиценты();
            comboBoxFormEducation.Items.AddRange
                (c.ListEducation.Select(x => x.Desc).ToArray());

            comboBoxYear.Items.AddRange(c.ListCoef
                .Select(y => y.GetYear().Substring(6, 4))
                .Distinct().ToArray());
        }

        private void buttonSave_Click (object sender, EventArgs e)
        {
            IsDuplicate();
        }
    }
}
