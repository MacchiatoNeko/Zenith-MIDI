using System;
using System.Windows.Controls;

namespace MIDITrailRender.Views
{
    /// <summary>
    /// Interaction logic for GeneralView.xaml
    /// </summary>
    public partial class GeneralView : UserControl
    {
        public GeneralView()
        {
            InitializeComponent();

            scaleSlider.NudToSlider = v => Math.Log(v, 2);
            scaleSlider.SliderToNud = v => Math.Pow(2, v);
        }
    }
}
