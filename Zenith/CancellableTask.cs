﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zenith
{
    public class CancellableTask
    {
        public static CancellableTask Run(Action<CancellationToken> start)
        {
            var source = new CancellationTokenSource();
            return new CancellableTask(Task.Run(() => start(source.Token)), source);
        }

        private CancellableTask(Task task, CancellationTokenSource token)
        {
            Task = task;
            TokenSource = token;
        }

        public Task Task { get; }
        public CancellationTokenSource TokenSource { get; }
        public CancellationToken Token => TokenSource.Token;
        public bool IsCancelling => Token.IsCancellationRequested;

        public void Cancel()
        {
            if (IsCancelling) return;
            TokenSource.Cancel();
        }

        public void Wait()
        {
            Task.Wait();
        }
    }
}
