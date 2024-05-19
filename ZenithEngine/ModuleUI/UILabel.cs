using System.Windows;
using System.Windows.Controls;

namespace ZenithEngine.ModuleUI
{
    public class UILabel : Label
    {
        object label = null;
        public object Label
        {
            get => label;
            set
            {
                label = value;
                if (label is DynamicResourceExtension)
                {
                    var l = (DynamicResourceExtension)label;
                    SetResourceReference(ContentProperty, l.ResourceKey);
                }
                else
                {
                    Content = label;
                }
            }
        }

        public UILabel(object label)
        {
            Margin = new Thickness(0, 0, 5, 5);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Label = label;
        }
    }
}
