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
            // Assembly Resolver'ı ekle (Gömülü DLL'leri yüklemek için)
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new System.Reflection.AssemblyName(args.Name).Name + ".dll";
                string resource = "etabs_tahkik_uygulaması_v001.Resources." + resourceName;

                using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    if (stream == null) return null;
                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return System.Reflection.Assembly.Load(assemblyData);
                }
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}