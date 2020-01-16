using System;
using System.IO;
using System.Windows.Forms;

namespace Report
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main ()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool NewPuth()
            {
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.Filter = "Files DB | *.db";
                openfile.Title = "Укажите путь к файлу базы данных";

                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PuthDB = openfile.FileName;
                    Properties.Settings.Default.Save();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            bool GetPuth()
            {
                try
                {
                    FileStream file_inf = new FileStream(Properties.Settings.Default.PuthDB, FileMode.Open,
                        FileAccess.ReadWrite, FileShare.ReadWrite, 4096, FileOptions.Asynchronous);
                    return true;
                }
                catch (FileNotFoundException)
                {
                    return NewPuth();
                }
                catch (ArgumentNullException)
                {
                    return NewPuth();
                }
                catch (ArgumentException)
                {
                    return NewPuth();
                }
                catch (DirectoryNotFoundException)
                {
                    return NewPuth();
                }
                catch (PathTooLongException)
                {
                    return NewPuth();
                }
                catch (IOException)
                {
                    MessageBox.Show("Возможно, это связанно с " +
                        "файлом базы данных, который открытыт в другой программе. " +
                        "Закройте все программы, котороые могут использовать этот файл, и повторите попытку.", "Не удалось открыть приложение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
            }

            if (GetPuth())
            {
                Application.Run(new FormListCountStudent());
            }
            else
            {
                Application.Exit();
            }

            //Application.Run(new FormListCountStudent());
        }
    }
}
