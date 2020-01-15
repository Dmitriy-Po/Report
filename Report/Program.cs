using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                    FileStream file_inf = new FileStream(Properties.Settings.Default.PuthDB, FileMode.Open, FileAccess.Read);
                    return true;
                }
                catch (FileNotFoundException)
                {
                    return NewPuth();
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
            

        }
    }
}
