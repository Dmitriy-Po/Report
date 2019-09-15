using Report;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Linq;

namespace Report
{
    public partial class FormAddReport : Form
    {
        public int GetCurrentrow_ID
        {
            get;
            set;
        }
        public FormAddReport()
        {
            InitializeComponent();
        }
        #region Проверка ввода данных
        public bool NO_Dublicate_ForUpdateRecord(List<TableCountStudent> editable_row)
        {
            //Функция для проверки на дубликаты при редактировании строки
            // - чтобы при редактировании не было возможности создать будликат записи.
            //Требуется сверять значения, каждый раз при сохранении редактиуемой строки,
            //в полях на форме с записями в коллекции ListCountStudent
            FormListCountStudent fa = new FormListCountStudent();
            
            //поиск дубликатов
            var duplicate = fa.ListCountStudent
                 .Where(x => x.Filial.Contains(comboBoxFilial.SelectedItem.ToString()) &&
                 x.Special.Contains(comboBoxSpecial.SelectedItem.ToString()) &&
                 x.year == Convert.ToInt32(textBoxYear.Text) &&
                 x.student_inv.Equals(checkBoxStdInv.Checked)).Select(x => x.id);

            //если переменная duplicate совпадает с редактируемой строкой, 
            //тогда это текущая редактируемая строка, и сохранение возможно.
            try
            {
                if (editable_row[0].id == duplicate.ElementAt(0))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Такая строка уже существует");
                    return false;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return true;
            }

        }
        public bool IsMatch() //функция проверяет наличие дубликатов при добавлении новой записи
        {
            //локальная переменная для определения корректного ввода ГОДА
            int local_year = 0;
            FormListCountStudent fa = new FormListCountStudent();
            /*
             * проверяем условие на совпадение строк со datagridview
             * если есть совпадение - значит строка уже существует и текущая запись дубликат
             * иначе выполнить сохранение. 
             * */
            try
            {
                if (int.TryParse(textBoxYear.Text, out local_year))
                {
                    var is_match = fa.ListCountStudent
                 .Select(
                         x => x.Filial.Contains(fa.ListFilial[comboBoxFilial.SelectedIndex].full_desc) &&
                         x.Special.Contains(fa.ListSpecial[comboBoxSpecial.SelectedIndex].desc) &&
                         x.year == local_year &&
                         x.student_inv.Equals(checkBoxStdInv.Checked)

                  ).Any(x => x.Equals(true));
                                        
                    if (!is_match)//если нет совпадений
                    {
                        //тогда сохранение                                               
                        return true;
                    }
                    //иначе совпадения есть
                    else
                    {
                        MessageBox.Show("Такая строка уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Заполните поле Календарный год", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                return false;
            }            
        } 
        public bool IsCorrect()
        {
            try
            {
                Convert.ToInt32(textBoxОчное.Text);
                Convert.ToInt32(textBoxЗаочное.Text);
                Convert.ToInt32(textBoxОчно_заочное.Text);                
                return true;                
            }
            catch (FormatException)
            {
                MessageBox.Show("В полях количество, должно быть указанно положительное число или ноль");
                return false;
            }

        }
        #endregion
        #region Функции сохранения и удаления
        public bool save()
        {
            /* 
             * Кнопка для сохранения записи в таблицу ЧисленностьОбучающихся.
               Метод передаёт на форму FormListCountStudent 
                индексы выбранных полей, в выпадающих списках.
               По индексам выбирается элемент в коллекции. 
             */
            if (comboBoxSkill.SelectedIndex >= 0)
            {
                try
                {
                    FormListCountStudent FormStudent = new FormListCountStudent();
                    FormStudent.SaveStudent(new int[] {
                    comboBoxFilial.SelectedIndex,
                    comboBoxSpecial.SelectedIndex,
                    comboBoxSkill.SelectedIndex,
                    Convert.ToInt32(textBoxОчное.Text),
                    Convert.ToInt32(textBoxОчно_заочное.Text),
                    Convert.ToInt32(textBoxЗаочное.Text),
                    Convert.ToInt32(textBoxYear.Text),
                    Convert.ToInt32(checkBoxStdInv.Checked)
                });
                    return true;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        public void update(TableCountStudent info)
        {
            //update set
            SQliteDB db = new SQliteDB();
            FormListCountStudent fa = new FormListCountStudent();

            //в момент обновления (сохранения), берутся текущие данные из выпадающий списков
            info.Filial     = fa.ListFilial[comboBoxFilial.SelectedIndex].full_desc;
            info.Special    = fa.ListSpecial[comboBoxSpecial.SelectedIndex].desc;
            info.Skill      = fa.ListSkill[comboBoxSkill.SelectedIndex].desc;
            info.ochnoe     = Convert.ToInt32(textBoxОчное.Text);
            info.zaochnoe   = Convert.ToInt32(textBoxЗаочное.Text);
            info.ochno_zaocjnoe = Convert.ToInt32(textBoxОчно_заочное.Text);
            info.year           = Convert.ToInt32(textBoxYear.Text);
            info.student_inv    = checkBoxStdInv.Checked;

            db.Update(info);

            //db.Update(new int[] {
            //    comboBoxFilial.SelectedIndex,
            //    comboBoxSpecial.SelectedIndex,
            //    comboBoxSkill.SelectedIndex,
            //    Convert.ToInt32(textBoxОчное.Text),
            //    Convert.ToInt32(textBoxОчно_заочное.Text),
            //    Convert.ToInt32(textBoxЗаочное.Text),
            //    Convert.ToInt32(textBoxYear.Text),
            //    Convert.ToInt32(checkBoxStdInv.Checked),
            //    id               
            //});

        }
        #endregion
        #region clearcomboboxes
        private void button2_Click(object sender, EventArgs e)
        {
            comboBoxFilial.ResetText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBoxSpecial.ResetText();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBoxSkill.ResetText();
        }
        #endregion
        
        public void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            //сохранить и закрыть форму        
            FormListCountStudent FormStudent = new FormListCountStudent();

            /*Существует проблема: при редактировании есть возможность создать дубликат
             * программа никак не отреагирет, а просто обновит запись*/

            //поиск в коллекции, посик поля с идентификатором записи
            var ID_current_row = FormStudent.ListCountStudent
                    .Where(x => x.id == GetCurrentrow_ID)
                    .Select(x => x).ToList();

            try
            {
                if (ID_current_row.Count > 0)
                {
                    if (NO_Dublicate_ForUpdateRecord(ID_current_row))
                    {
                        update(ID_current_row[0]);
                        Close();
                    }
                }
                else
                {
                    if (IsMatch())
                    {
                        if (save())
                        {
                            Close();
                        }  
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }
    }
}
