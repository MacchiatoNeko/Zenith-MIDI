using System;

namespace ZenithEngine.MIDI.Audio
{
    public interface IMidiOutput : IDisposable
    {
        void SendEvent(uint e);
        void Reset();
    }
}
