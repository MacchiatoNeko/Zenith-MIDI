using System.ComponentModel;

namespace TexturedRender
{
    public class ViewSettingsModel : INotifyPropertyChanged
    {
        public int FirstKey { get; set; } = 0;
        public int LastKey { get; set; } = 127;

        public double NoteScreenTime { get; set; } = 1000;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
