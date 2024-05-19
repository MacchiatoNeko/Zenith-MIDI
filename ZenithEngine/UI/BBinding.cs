using System.Windows;
using System.Windows.Data;

namespace ZenithEngine.UI
{
    public class BBinding : Binding
    {
        public BBinding(DependencyProperty dp, object source) : base()
        {
            Path = new PropertyPath(dp);
            Source = source;
        }
    }
}
