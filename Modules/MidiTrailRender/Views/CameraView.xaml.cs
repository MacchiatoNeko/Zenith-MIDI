using System;
using System.Windows.Controls;

namespace MIDITrailRender.Views
{
    /// <summary>
    /// Interaction logic for CameraView.xaml
    /// </summary>
    public partial class CameraView : UserControl
    {
        double StoN(double v)
        {
            return Math.Sign(v) * Math.Pow(Math.Abs(v), 2);
        }

        double NtoS(double v)
        {
            return Math.Sign(v) * Math.Pow(Math.Abs(v), 1.0 / 2);
        }

        public CameraView()
        {
            InitializeComponent();

            xPos.NudToSlider = NtoS;
            xPos.SliderToNud = StoN;

            yPos.NudToSlider = NtoS;
            yPos.SliderToNud = StoN;

            zPos.NudToSlider = NtoS;
            zPos.SliderToNud = StoN;

            //zOffset.NudToSlider = NtoS;
            //zOffset.SliderToNud = StoN;
        }
    }
}
