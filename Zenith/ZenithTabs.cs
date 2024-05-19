using System.Windows;
using System.Windows.Controls;

namespace Zenith
{
    class ZenithTabs : TabControl
    {
        public UpdateProgress UpdaterProgress
        {
            get { return (UpdateProgress)GetValue(UpdaterProgressProperty); }
            set { SetValue(UpdaterProgressProperty, value); }
        }

        public static readonly DependencyProperty UpdaterProgressProperty =
            DependencyProperty.Register("UpdaterProgress", typeof(UpdateProgress), typeof(CustomTabs), new PropertyMetadata(UpdateProgress.NotDownloading));


        public string VersionName
        {
            get { return (string)GetValue(VersionNameProperty); }
            set { SetValue(VersionNameProperty, value); }
        }

        public static readonly DependencyProperty VersionNameProperty =
            DependencyProperty.Register("VersionName", typeof(string), typeof(CustomTabs), new PropertyMetadata(""));
    }
}
