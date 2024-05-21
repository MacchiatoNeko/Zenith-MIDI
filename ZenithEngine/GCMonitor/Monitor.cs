using System;
using System.Diagnostics;
using System.Threading;

namespace ZenithEngine.GCMonitor
{
    public class Monitor
    {
        static int lastGCCollectionCount;

        public Monitor()
        {
            lastGCCollectionCount = GC.CollectionCount(0);
            Thread monitoringThread = new(MonitorGC);
            monitoringThread.Start();
        }

        private static void MonitorGC()
        {
            Process process = Process.GetCurrentProcess();
            while (true)
            {
                if (GC.CollectionCount(0) > lastGCCollectionCount) // Check if collection count has increased since last check
                {
                    long memoryUsage = process.PrivateMemorySize64;
                    double memoryUsageMB = memoryUsage / (1024.0 * 1024.0);
                    Console.WriteLine("Garbage collection occurred. Memory Usage: " + memoryUsageMB.ToString("0.00") + " MB");
                    lastGCCollectionCount = GC.CollectionCount(0); // Update last collection count
                }
                Thread.Sleep(10);
            }
        }
    }
}
