namespace Zenith.Models
{
    public class SettingsModel : SaveableModel
    {
        public string SelectedLanguage { get; set; }

        public static SettingsModel Instance { get; } = new SettingsModel();
        public SettingsModel() : base(".cfg/settings.json")
        { }
    }
}
