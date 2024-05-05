namespace Zenith 
{
    public class DefaultValues
    {
        public static readonly int Width = 1280;
        public static readonly int Height = 720;
        public static readonly int FPS = 60;
        public static readonly bool TimeBased = false;
        public static readonly bool IgnoreColorEvents = false;
        public static readonly long LastBGChangeTime = -1;
        public static readonly string BGImage = "none";
        public static readonly bool IncludeAudio = false;
        public static readonly string AudioInputPath = "none";
        public static readonly bool UseBitrate = true;
        public static readonly bool CustomFFmpeg = false;
        public static readonly int Bitrate = 20000;
        public static readonly int RenderCRF = 17;
        public static readonly string RenderCRFPreset = "medium";
        public static readonly bool FFmpegDebug = false;
        public static readonly string FFmpegCustomArgs = "-metadata Title=\"Zenith-MIDI\"";
        public static readonly bool IsRendering = false;
        public static readonly bool IsRenderingMask = false;
        public static readonly string RenderMaskOutput = "none";
    }
}


