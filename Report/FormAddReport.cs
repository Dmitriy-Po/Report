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
                        //добавить функции очистки полей ввода                                               
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
        public void update()
        {
            //update set
            SQliteDB db = new SQliteDB();
            FormListCountStudent fa = new FormListCountStudent();

            db.Update(new int[] {
                comboBoxFilial.SelectedIndex,
                comboBoxSpecial.SelectedIndex,
                comboBoxSkill.SelectedIndex,
                Convert.ToInt32(textBoxОчное.Text),
                Convert.ToInt32(textBoxОчно_заочное.Text),
                Convert.ToInt32(textBoxЗаочное.Text),
                Convert.ToInt32(textBoxYear.Text),
                Convert.ToInt32(checkBoxStdInv.Checked),               
            });

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
            FormListCountStudent fa = new FormListCountStudent();
            // -1 это добавление новой строки
            
            //if (fa.SelectedRowID == -1)
            //{
            //    if (IsCorrect())
            //    {
            //        if (IsMatch())
            //        {
            //            save();
            //        }
            //    }
            //}
            //else
            //{
            //    if (IsCorrect())
            //    {
            //        update();
            //    }
            //}
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            //сохранить и закрыть форму            
            SQliteDB db = new SQliteDB();
            FormListCountStudent FormStudent = new FormListCountStudent();

            
            //если строка не существует, тогда сохранение, иначе обновление
            if (db.IfExists(new int[] 
                { Convert.ToInt32(textBoxYear.Text),  FormStudent.ListFilial[comboBoxFilial.SelectedIndex].id,
                    FormStudent.ListSpecial[comboBoxSpecial.SelectedIndex].id,  Convert.ToInt32(checkBoxStdInv.ThreeState)}))
            {
                if (IsCorrect())
                {
                    update();
                    Close();                    
                }
            }
            else
            {
                if (IsCorrect())
                {
                    if (IsMatch())
                    {
                        save();
                        Close();
                    }
                }
            }
        }
    }
}
