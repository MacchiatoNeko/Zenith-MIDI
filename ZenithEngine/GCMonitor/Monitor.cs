using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace ZenithEngine.GCMonitor
{
    public class Monitor
    {
        //static int lastGCCollectionCount;
        readonly string prefixTitle = "Zenith " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " | ";

        public Monitor()
        {
            //lastGCCollectionCount = GC.CollectionCount(0);
            Thread monitoringThread = new(MonitorGC);
            monitoringThread.Start();
        }

        private void MonitorGC()
        {
            while (true)
            {
                Process process = Process.GetCurrentProcess();
                //if (GC.CollectionCount(0) > lastGCCollectionCount) // Check if collection count has increased since last check
                //{
                    long memoryUsage = process.PrivateMemorySize64;
                    double memoryUsageMB = memoryUsage / (1024.0 * 1024.0);
                    Console.Title = prefixTitle + "RAM+SWAP Usage: " + memoryUsageMB.ToString("0.00") + " MB";
                    //lastGCCollectionCount = GC.CollectionCount(0); // Update last collection count
                //}
                Thread.Sleep(10);
            }
        }
    }
}
