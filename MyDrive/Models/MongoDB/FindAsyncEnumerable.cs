using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Models.MongoDB
{
    internal class FindAsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        public FindAsyncEnumerable(FindFluentBase<MongoDriveItem, T> findFluent)
        {
            FindFluent = findFluent;
        }

        public FindFluentBase<MongoDriveItem, T> FindFluent { get; }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncCursorAsyncEnumerator<T>(FindFluent.ToCursor(cancellationToken), cancellationToken);
        }
    }
}
