using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Zenith
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = ("Zenith " + InstallSettings.VersionName + " Alpha-state");
            Console.WriteLine("Current runtime -> " + RuntimeInformation.FrameworkDescription);
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
        }
    }
}
