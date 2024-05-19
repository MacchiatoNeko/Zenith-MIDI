using System.Windows;
using System.Windows.Controls;

namespace MIDITrailRender.Views
{
    /// <summary>
    /// Interaction logic for FullColorView.xaml
    /// </summary>
    public partial class FullColorView : UserControl
    {
        public bool ShowWater
        {
            get { return (bool)GetValue(ShowWaterProperty); }
            set { SetValue(ShowWaterProperty, value); }
        }

        public static readonly DependencyProperty ShowWaterProperty =
            DependencyProperty.Register("ShowWater", typeof(bool), typeof(FullColorView), new PropertyMetadata(true));


        public FullColorView()
        {
            InitializeComponent();
        }
    }
}
