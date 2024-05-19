using System;

namespace ZenithEngine.MIDI.Audio
{
    public class KDMAPIOutput : IDisposable, IMidiOutput
    {
        static bool initialized = false;
        public static bool Initialized => initialized;

        public static void Init()
        {
            if (initialized || !KDMAPI.CanImportDll()) return;
            initialized = true;
            KDMAPI.ExecuteWithDllProtection(() => KDMAPI.InitializeKDMAPIStream());
        }

        public static void Terminate()
        {
            if (!initialized || !KDMAPI.CanImportDll()) return;
            initialized = false;
            KDMAPI.ExecuteWithDllProtection(() => KDMAPI.TerminateKDMAPIStream());
        }

        public KDMAPIOutput() { }

        public void Dispose()
        {
            byte cc = 120;
            byte vv = 0;
            for (int i = 0; i < 16; i++)
            {
                var command = 0xB0 | i;
                SendEvent((uint)(command | (cc << 8) | (vv << 16)));
            }
            Reset();
        }

        public void SendEvent(uint e)
        {
            if (initialized)
            {
                KDMAPI.ExecuteWithDllProtection(() => KDMAPI.SendDirectData(e));
            }
        }

        public void Reset()
        {
            if (initialized)
            {
                KDMAPI.ExecuteWithDllProtection(() => KDMAPI.ResetKDMAPIStream());
            }
        }
    }
}
