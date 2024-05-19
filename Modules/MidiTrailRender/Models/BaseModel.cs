using System.ComponentModel;

namespace MIDITrailRender.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public CameraModel Camera { get; set; } = new CameraModel();
        public GeneralModel General { get; set; } = new GeneralModel();
        public GlowModel Glow { get; set; } = new GlowModel();
        public KeysModel Keys { get; set; } = new KeysModel();
        public NotesModel Notes { get; set; } = new NotesModel();
        public LightModel Light { get; set; } = new LightModel();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
