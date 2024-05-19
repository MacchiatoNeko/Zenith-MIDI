using System.Windows;

namespace ZenithEngine.UI
{
    [TemplatePart(Name = "PART_ripple", Type = typeof(RippleSource))]
    public class Checkbox : System.Windows.Controls.CheckBox
    {
        public static readonly RoutedEvent CheckToggledEvent = EventManager.RegisterRoutedEvent(
            "CheckToggled", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(Checkbox));

        public event RoutedEventHandler CheckToggled
        {
            add { AddHandler(CheckToggledEvent, value); }
            remove { RemoveHandler(CheckToggledEvent, value); }
        }

        public Checkbox()
        {
            Checked += Checkbox_Checked;
            Unchecked += Checkbox_Unchecked;
            CheckToggled += Checkbox_CheckToggled;
        }

        private void Checkbox_CheckToggled(object sender, RoutedEventArgs e)
        {
            rippleBox?.SendRipple();
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CheckToggledEvent));
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CheckToggledEvent));
        }

        RippleSource rippleBox = null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template != null)
            {
                rippleBox = Template.FindName("PART_ripple", this) as RippleSource;
            }
        }
    }
}
