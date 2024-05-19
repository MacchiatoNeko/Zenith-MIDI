using System;
using System.Windows;
using System.Windows.Controls;
using Zenith.Models;

namespace Zenith
{
    /// <summary>
    /// Interaction logic for RenderTab.xaml
    /// </summary>
    public partial class RenderTab : UserControl
    {
        public RenderTab()
        {
            InitializeComponent();
        }

        public BaseModel Data
        {
            get
            {
                var data = DataContext as BaseModel;
                if (data == null) throw new Exception("Data context must be set correctly");
                return data;
            }
        }

        private void browseOutputButton_Click(object sender, RoutedEventArgs e)
        {
            var path = Data.Cache.SaveFileDialog("RenderOutput", "H.264 video (*.mp4)|*.mp4|All types|*.*");
            if (path != null) Data.OutputOptions.OutputLocation = path;
        }

        private void browseMaskButton_Click(object sender, RoutedEventArgs e)
        {
            var path = Data.Cache.SaveFileDialog("RenderMask", "H.264 video (*.mp4)|*.mp4|All types|*.*");
            if (path != null) Data.OutputOptions.MaskOutputLocation = path;
        }

        private void browseAudioButton_Click(object sender, RoutedEventArgs e)
        {
            var path = Data.Cache.OpenFileDialog("RenderAudio", "Common audio files (*.mp3;*.wav;*.ogg;*.flac)|*.mp3;*.wav;*.ogg;*.flac");
            if (path != null) Data.OutputOptions.AudioLocation = path;
        }

        private async void startRender_Click(object sender, RoutedEventArgs e)
        {
            await Data.StartRender();
        }
    }
}
