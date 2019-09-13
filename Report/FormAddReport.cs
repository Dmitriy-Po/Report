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
        public bool IsMatch() //функция проверяет наличие дубликатов
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
        public void save()
        {
            /* 
             * Кнопка для сохранения записи в таблицу ЧисленностьОбучающихся.
               Метод передаёт на форму FormListCountStudent 
                индексы выбранных полей, в выпадающих списках.
               По индексам выбирается элемент в коллекции. 
             */
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
            }
            catch (FormatException)
            {
                MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Заполните все обязательные поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void update(TableCountStudent info)
        {
            //update set
            SQliteDB db = new SQliteDB();
            FormListCountStudent fa = new FormListCountStudent();

            //в момент обновления, берутся текущие данные из выпадающий списков
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
            
            if (ID_current_row[0].id > 0)
            {
                /*здесь нет проверки на дубликаты*/
                update(ID_current_row[0]);
                Close();
            }
            else
            {
                /*здест есть проверка на дубликаты*/
                if (IsMatch())
                {
                    save();
                    Close();
                }
            }
        }
    }
}
