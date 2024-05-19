using System.Windows;

namespace ZenithEngine.UI
{
    public class LangText : DynamicResourceExtension
    {
        public LangText(string key) : base("lang." + key)
        { }
    }
}
