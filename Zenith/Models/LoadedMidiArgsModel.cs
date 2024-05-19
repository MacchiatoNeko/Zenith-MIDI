using System;
using System.IO;
using ZenithEngine.MIDI;

namespace Zenith.Models
{
    public class LoadedMidiArgsModel : IDisposable
    {
        public MidiFile MidiFile { get; }

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public long NoteCount => MidiFile.NoteCount;

        public LoadedMidiArgsModel(MidiFile midi, string filepath)
        {
            MidiFile = midi;
            FilePath = filepath;
            FileName = Path.GetFileName(filepath);
        }

        public void Dispose()
        {
            MidiFile.Dispose();
        }
    }
}
