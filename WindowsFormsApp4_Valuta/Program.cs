using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4_Valuta
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ShowSplash();
            Form1 mainForm = new Form1();
            Application.Run(mainForm);

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());



        }

        private static void ShowSplash()
        {
            Splash sp = new Splash();
            sp.Show();
            Application.DoEvents();

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler((sender, ea) =>
            {
                sp.BeginInvoke(new Action(() =>
                {
                    if (sp != null && Application.OpenForms.Count > 1)
                    {
                        sp.Close();
                        sp.Dispose();
                        sp = null;
                        t.Stop();
                        t.Dispose();
                        t = null;
                    }
                }));
            });
            t.Start();
        }
    }
}
