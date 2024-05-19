using System;

namespace ZenithEngine.DXHelper
{
    public class Applier<T> : IDisposable
    {
        T previous;
        Action<T> set;

        public Applier(T apply, Func<T> get, Action<T> set)
        {
            previous = get();
            set(apply);
            this.set = set;
        }

        public void Dispose()
        {
            set(previous);
        }
    }
}
