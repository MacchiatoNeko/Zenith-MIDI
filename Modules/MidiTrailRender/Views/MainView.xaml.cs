using MIDITrailRender.Models;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using ZenithEngine.ModuleUI;

namespace MIDITrailRender.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl, ISerializableContainer
    {
        public BaseModel Data { get; private set; } = new BaseModel();

        public MainView()
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
