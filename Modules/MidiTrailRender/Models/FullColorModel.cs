using System.ComponentModel;

namespace MIDITrailRender.Models
{
    public class FullColorModel : INotifyPropertyChanged
    {
        public double Diffuse { get; set; } = 1;
        public double Specular { get; set; } = 1;
        public double Emit { get; set; } = 1;
        public double Water { get; set; } = 1;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
