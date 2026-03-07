using System;
using System.Windows.Forms;
using EtabsTools; // Form1'in olduğu namespace'i ekliyoruz

namespace EtabsTools
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Eğer Form1.cs içindeki namespace 'EtabsTools' ise burası çalışacaktır.
            Application.Run(new Form1());
        }
    }
}