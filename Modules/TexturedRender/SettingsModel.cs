using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using ZenithEngine;
using ZenithEngine.IO;
using ZenithEngine.ModuleUI;

namespace TexturedRender
{
    public class SettingsModel : INotifyPropertyChanged, ISerializableContainer
    {
        public DirectoryFolderLocation[] PackLocations { get; set; } = new DirectoryFolderLocation[0];
        public DirectoryFolderLocation SelectedPack { get; set; } = null;
        public LoadedPack LoadedPack { get; set; } = null;

        public ViewSettingsModel View { get; set; } = new ViewSettingsModel();

        public NoteColorPalettePick PalettePicker { get; } = new NoteColorPalettePick();

        public event PropertyChangedEventHandler PropertyChanged;

        public void Parse(JObject data)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
