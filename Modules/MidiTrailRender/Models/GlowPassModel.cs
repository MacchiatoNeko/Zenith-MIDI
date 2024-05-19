using System.ComponentModel;

namespace MIDITrailRender.Models
{
    public class GlowPassModel : INotifyPropertyChanged
    {
        public bool UseGlow { get; set; } = false;
        public double GlowSigma { get; set; } = 20;
        public double GlowStrength { get; set; } = 8;
        public double GlowBrightness { get; set; } = 1;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
