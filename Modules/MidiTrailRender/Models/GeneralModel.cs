using Newtonsoft.Json;
using System.ComponentModel;
using ZenithEngine;

namespace MIDITrailRender.Models
{
    public class GeneralModel : INotifyPropertyChanged
    {
        public int FirstKey { get; set; } = 0;
        public int LastKey { get; set; } = 127;
        public bool SameWidthNotes { get; set; } = true;

        public double NoteScale { get; set; } = 5000;

        [JsonIgnore]
        public NoteColorPalettePick PalettePicker { get; set; } = new NoteColorPalettePick();

        public GeneralModel()
        {
            PalettePicker.SetPath("Plugins\\Assets\\Palettes");
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
