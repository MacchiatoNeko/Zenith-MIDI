using System;

namespace ZenithShared
{
    class InstallFailedException : Exception
    {
        public InstallFailedException(string message) : base(message) { }
    }
}
