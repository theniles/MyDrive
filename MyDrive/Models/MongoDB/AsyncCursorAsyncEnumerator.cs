using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Models.MongoDB
{
    internal class AsyncCursorAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private IEnumerator<T> CurrentBatch;

        public IAsyncCursor<T> Cursor { get; }

        public CancellationToken CancellationToken { get; }

        public bool Started { get; private set; }

        public bool Finished { get; private set; }

        public bool Disposed { get; private set; }

        public AsyncCursorAsyncEnumerator(IAsyncCursor<T> cursor, CancellationToken cancellationToken = default)
        {
            Cursor = cursor;
            CancellationToken = cancellationToken;
        }

        public T Current { get { ThrowIfDisposed(); if (!Started) throw new InvalidOperationException("Enumeration not started"); if (Finished) throw new InvalidOperationException("Enumeration finished"); return CurrentBatch.Current; } }

        public ValueTask DisposeAsync()
        {
            if(!Disposed)
            {
                Disposed = true;
                CurrentBatch?.Dispose();
                Cursor.Dispose();
            }
            return ValueTask.CompletedTask;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            ThrowIfDisposed();
            Started = true;

            if (CurrentBatch != null && CurrentBatch.MoveNext())
                return true;

            while (true)
            {
                if(await Cursor.MoveNextAsync())
                {
                    CurrentBatch?.Dispose();
                    CurrentBatch = Cursor.Current.GetEnumerator();
                    if (CurrentBatch.MoveNext())
                    {
                        return true;
                    }
                }
                else
                {
                    CurrentBatch = null;
                    Finished = true;
                    return false;
                }
            }
        }

        public void Reset()
        {
            ThrowIfDisposed();
            throw new NotSupportedException();
        }

        private void ThrowIfDisposed()
        {
            if (Disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}
