using System;
using System.Windows;

namespace Zenith
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
#if !DEBUG
            try
            {
#endif
                Console.Title = ("Zenith " + InstallSettings.VersionName + " Alpha-state");
                Application app = new();
                var window = new ZenithWindow();
                //window.Loaded += (s, e) =>
                //{
                //    if (args.Length > 0)
                //    {
                //        window.LoadMidi(args[0]);
                //    }
                //    if (args.Length > 1)
                //    {
                //        window.SelectModule(args[1]);
                //        window.StartPipeline(false);
                //    }
                //};
                //ZenithEngine.GCMonitor.Monitor _ = new();
                app.Run(window);
#if !DEBUG
            }
            catch (Exception e)
            {
                string msg = e.Message + "\n" + e.Data + "\n";
                msg += e.StackTrace;
                MessageBox.Show(msg, "Zenith has crashed!");
            }
#endif
        }
    }
}
