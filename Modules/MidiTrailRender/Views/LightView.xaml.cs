using System;
using System.Windows.Controls;

namespace MIDITrailRender.Views
{
    /// <summary>
    /// Interaction logic for LightView.xaml
    /// </summary>
    public partial class LightView : UserControl
    {
        double StoN(double v)
        {
            return Math.Sign(v) * Math.Pow(Math.Abs(v), 2);
        }

        double NtoS(double v)
        {
            return Math.Sign(v) * Math.Pow(Math.Abs(v), 1.0 / 2);
        }

        public LightView()
        {
            InitializeComponent();

            xPos.NudToSlider = NtoS;
            xPos.SliderToNud = StoN;

            zPos.NudToSlider = NtoS;
            zPos.SliderToNud = StoN;
        }
    }
}
