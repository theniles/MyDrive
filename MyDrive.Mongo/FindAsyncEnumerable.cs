using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Mongo
{
    internal class FindAsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        public FindAsyncEnumerable(FindFluentBase<MongoDriveItem, T> findFluent, Action<T>? transform = null)
        {
            FindFluent = findFluent;
            Transform = transform;
        }

        public FindFluentBase<MongoDriveItem, T> FindFluent { get; }
        public Action<T>? Transform { get; }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncCursorAsyncEnumerator<T>(FindFluent.ToCursor(cancellationToken), Transform, cancellationToken);
        }
    }
}
