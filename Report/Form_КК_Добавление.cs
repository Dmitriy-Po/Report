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
        public DataGridViewRow CurrentDataRow { get; set; }
        string[] ValuesOfCoef()
        {
            SQliteDB db = new SQliteDB();
            FormКоэффиценты form_coef = new FormКоэффиценты();
            int[] id = new int[2];            

            #region получение id - последней записи в бд.
            SQLiteDataReader reader = db.Select_any_query("SELECT MAX(КорректирующиеКоэффиценты.код) as last_id" +
                    " FROM КорректирующиеКоэффиценты");

            while (reader.Read())
            {
                id[0] = reader.GetInt32(0);
            }
            reader.Close();
            db.ConnectionDB.Close();

            #endregion
            //запись из коллекции - формы обучения.
            id[1] = form_coef.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.Text)).Select(x => x.id).ElementAt(0);

            string[] val = { Math.Round( Convert.ToDecimal(textBoxCoeff.Text), 2 ).ToString(),
                comboBoxYear.Text +"-01-01", id[0].ToString(), id[1].ToString() };

            val[0] = val[0].Replace(",", "."); //замена символа запятой на точку

            return val;

        }

        void UpdateRecord (int id)//обновление
        {
            SQliteDB db = new SQliteDB();
            FormКоэффиценты form_coef = new FormКоэффиценты();

            string[] FieldsCorrCoef = { "Наименование", "ПолноеНаименование", "Комментарий", "Уточнение", "СтудентИнвалид" };
            object[] ValuesOfCorrCoef = { textBoxDesc.Text, textBoxFullDesc.Text, textBoxComment.Text, textBoxDetail.Text, Convert.ToInt16(checkBoxStdInv.Checked) };

            db.Update_new("КорректирующиеКоэффиценты", FieldsCorrCoef, ValuesOfCorrCoef, id.ToString());

            string[] FieldsCoef = { "Значение", "КаледндарныйГод", "Корректирующие_ВК", "ФормаОбучения_ВК" };

            int id_formEducation = form_coef.ListEducation.Where(x => x.Desc.Contains(comboBoxFormEducation.Text)).Select(x => x.id).ElementAt(0);
            string[] ValuesOfCoeff = { textBoxCoeff.Text, comboBoxYear.Text, id.ToString(),  id_formEducation.ToString()};

            db.Update_new("ЗначениеКоэффицента",
                    FieldsCoef, ValuesOfCoeff, id.ToString());

        }
        void InsertRecord ()//сохранение
        {
            //Функция добавляет запись в базу данных
            SQliteDB db = new SQliteDB();

            string[] ColumnsCorrCoef = { "Наименование", "ПолноеНаименование", "Комментарий", "Уточнение", "СтудентИнвалид" };
            object[] ValuesOfCorrCoef = { textBoxDesc.Text, textBoxFullDesc.Text, textBoxComment.Text, textBoxDetail.Text, Convert.ToInt16(checkBoxStdInv.Checked) };

            db.Insert_New("КорректирующиеКоэффиценты",
                string.Join(", ", ColumnsCorrCoef),
                string.Join("', '", ValuesOfCorrCoef));

            string[] ColumnsValueCoef = { "Значение", "КаледндарныйГод", "Корректирующие_ВК", "ФормаОбучения_ВК" };
            string[] ValueCoef = ValuesOfCoef();

            db.Insert_New("ЗначениеКоэффицента",
                string.Join(", ", ColumnsValueCoef),
                string.Join("', '", ValueCoef));
        }
        bool IsEditingMode (out int id)
        {
            FormКоэффиценты fk = new FormКоэффиценты();

            //Редактируемая строка, взятая из коллекции ListCoef.
            var DuplicateId = fk.ListCoef.Where(
                x => x.Наименование.Equals(textBoxDesc.Text, StringComparison.OrdinalIgnoreCase) &&
                     x.GetForm().Equals(comboBoxFormEducation.Text, StringComparison.OrdinalIgnoreCase) &&
                     x.СтудентИнвалид.Equals(checkBoxStdInv.Checked) &&
                     x.GetYear().Substring(6, 4).Equals(comboBoxYear.Text) &&
                     x.GetCoef() == Math.Round(Convert.ToDecimal(textBoxCoeff.Text), 2))
                     .Select(x => x.id);

            //будликат - исходя из выбранных полей на форме.
            bool Duplicate = fk.ListCoef.Select(
                x => x.Наименование.Equals(textBoxDesc.Text, StringComparison.OrdinalIgnoreCase) &&
                     x.GetForm().Equals(comboBoxFormEducation.Text, StringComparison.OrdinalIgnoreCase) &&
                     x.СтудентИнвалид.Equals(checkBoxStdInv.Checked) &&
                     x.GetYear().Substring(6, 4).Equals(comboBoxYear.Text) &&
                     x.GetCoef() == Math.Round(Convert.ToDecimal(textBoxCoeff.Text), 2))
                    .Any(x => x.Equals(true));

            /*Если выбранная (из DataGrid) редактируемая строка совпадает, по параметрам 
             * <указанным в поях формы>, с строкой в коллекции  ListCoef, тогда 
                    - это текущая редактиремая строка и сохраненеи возможно.
                Строки в DataGrid и ListCoef сравниваются по полю id.    

             * Фокус в том, что при нажатии кнопки Редактировать, переменная DataGrid получает, из столбца 'id'
             * идентификатор строки (записи в бд). А при нажати кнопки Создать по шаблону, переменная DataGrid
             * идентификатор не получает. Вот и вся магия :) 
             * */

            if (Duplicate)
            {
                if (Convert.ToInt32(CurrentDataRow.Cells["id"].Value) == DuplicateId.ElementAt(0))
                {
                    id = DuplicateId.ElementAt(0);
                    return true;
                }
                else { id = 0; return false; };
            }
            else { id = 0; return true; };
        }

        private void buttonSaveAndClose_Click (object sender, EventArgs e)
        {
            SQliteDB db = new SQliteDB();
            int id = 0;
            if (IsEditingMode(out id))
            {
                //db.Insert_New()
                //InsertRecord();
                UpdateRecord(id);
            }
            else MessageBox.Show("Такая строка уже существует");
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
            //IsEditingMode();
        }
    }
}
