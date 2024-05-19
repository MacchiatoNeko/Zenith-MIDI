using System;
using System.Runtime.InteropServices;

namespace ZenithEngine.MIDI.Audio
{
    static partial class KDMAPI
    {
        public struct MIDIHDR
        {
            public string lpdata;
            public uint dwBufferLength;
            public uint dwBytesRecorded;
            public nint dwUser;
            public uint dwFlags;
            public nint lpNext;
            public nint reserved;
            public uint dwOffset;
            public nint dwReserved;
        }

        public enum OMSettingMode
        {
            OM_SET = 0x0,
            OM_GET = 0x1
        }

        public enum OMSetting
        {
            OM_CAPFRAMERATE = 0x10000,
            OM_DEBUGMMODE = 0x10001,
            OM_DISABLEFADEOUT = 0x10002,
            OM_DONTMISSNOTES = 0x10003,

            OM_ENABLESFX = 0x10004,
            OM_FULLVELOCITY = 0x10005,
            OM_IGNOREVELOCITYRANGE = 0x10006,
            OM_IGNOREALLEVENTS = 0x10007,
            OM_IGNORESYSEX = 0x10008,
            OM_IGNORESYSRESET = 0x10009,
            OM_LIMITRANGETO88 = 0x10010,
            OM_MT32MODE = 0x10011,
            OM_MONORENDERING = 0x10012,
            OM_NOTEOFF1 = 0x10013,
            OM_EVENTPROCWITHAUDIO = 0x10014,
            OM_SINCINTER = 0x10015,
            OM_SLEEPSTATES = 0x10016,

            OM_AUDIOBITDEPTH = 0x10017,
            OM_AUDIOFREQ = 0x10018,
            OM_CURRENTENGINE = 0x10019,
            OM_BUFFERLENGTH = 0x10020,
            OM_MAXRENDERINGTIME = 0x10021,
            OM_MINIGNOREVELRANGE = 0x10022,
            OM_MAXIGNOREVELRANGE = 0x10023,
            OM_OUTPUTVOLUME = 0x10024,
            OM_TRANSPOSE = 0x10025,
            OM_MAXVOICES = 0x10026,
            OM_SINCINTERCONV = 0x10027,

            OM_OVERRIDENOTELENGTH = 0x10028,
            OM_NOTELENGTH = 0x10029,
            OM_ENABLEDELAYNOTEOFF = 0x10030,
            OM_DELAYNOTEOFFVAL = 0x10031
        }

        public struct DebugInfo
        {
            public float RenderingTime;
            public int[] ActiveVoices;

            public double ASIOInputLatency;
            public double ASIOOutputLatency;
        }

        private const string DllName = "OmniMIDI\\OmniMIDI";

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern nint LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeLibrary(nint hModule);

        private static nint _dllHandle = nint.Zero;

        public static bool CanImportDll()
        {
            if (_dllHandle == nint.Zero)
            {
                _dllHandle = LoadLibrary(DllName);
            }

            if (_dllHandle == nint.Zero)
            {
                return false;
            }

            FreeLibrary(_dllHandle);
            _dllHandle = nint.Zero;
            return true;
        }

        [LibraryImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool ReturnKDMAPIVer(out int Major, out int Minor, out int Build, out int Revision);

        [LibraryImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool IsKDMAPIAvailable();

        [LibraryImport(DllName, SetLastError = true)]
        public static partial int InitializeKDMAPIStream();

        [LibraryImport(DllName, SetLastError = true)]
        public static partial int TerminateKDMAPIStream();

        [LibraryImport(DllName, SetLastError = true)]
        public static partial void ResetKDMAPIStream();

        [LibraryImport(DllName, SetLastError = true)]
        public static partial uint SendCustomEvent(uint eventtype, uint chan, uint param);

        [LibraryImport(DllName, SetLastError = true)]
        public static partial uint SendDirectData(uint dwMsg);

        [LibraryImport(DllName, SetLastError = true)]
        public static partial uint SendDirectDataNoBuf(uint dwMsg);

        [DllImport(DllName, SetLastError = true)]
        public static extern uint SendDirectLongData(ref MIDIHDR IIMidiHdr);

        [DllImport(DllName, SetLastError = true)]
        public static extern uint SendDirectLongDataNoBuf(ref MIDIHDR IIMidiHdr);

        [DllImport(DllName, SetLastError = true)]
        public static extern uint PrepareLongData(ref MIDIHDR IIMidiHdr);

        [DllImport(DllName, SetLastError = true)]
        public static extern uint UnprepareLongData(ref MIDIHDR IIMidiHdr);

        [DllImport(DllName, SetLastError = true)]
        public static extern bool DriverSettings(OMSetting Setting, OMSettingMode Mode, nint Value, int cbValue);

        [DllImport(DllName, SetLastError = true)]
        public static extern void LoadCustomSoundFontsList(ref string Directory);

        [DllImport(DllName, SetLastError = true)]
        public static extern DebugInfo GetDriverDebugInfo();

        public static T ExecuteWithDllProtection<T>(Func<T> func)
        {
            if (CanImportDll())
            {
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return default;
                }
            }
            else
            {
                Console.WriteLine("DLL not found. Please ensure the OmniMIDI DLL is present.");
                return default;
            }
        }

        public static void ExecuteWithDllProtection(Action action)
        {
            if (CanImportDll())
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("DLL not found. Please ensure the OmniMIDI DLL is present.");
            }
        }
    }
}