using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using ZenithEngine.ModuleUI;

namespace NoteCountRender
{
    /// <summary>
    /// Interaction logic for SettingsCtrl.xaml
    /// </summary>
    public partial class SettingsCtrl : UserControl, ISerializableContainer
    {
        public BaseModel Data { get; private set; } = new BaseModel();

        public SettingsCtrl()
        {
            DataContext = Data;

            InitializeComponent();
        }

        public void Parse(JObject data)
        {
            Dispatcher.Invoke(() =>
            {
                Data = data.ToObject<BaseModel>();
                DataContext = Data;
            });
        }

        public JObject Serialize()
        {
            return JObject.FromObject(Data);
        }
    }
}
