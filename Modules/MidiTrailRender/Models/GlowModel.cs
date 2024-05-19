using System.ComponentModel;

namespace MIDITrailRender.Models
{
    public class GlowModel : INotifyPropertyChanged
    {
        public GlowPassModel Pass { get; set; } = new GlowPassModel();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
