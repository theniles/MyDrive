using MyDrive.Api.Queries;
using MyDrive.Api.Updates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api
{
    public interface IDrive : IDisposable
    {
        //will be hard to do
        //noit yet buddy but maybe someday
        //public IAsyncEnumerable<IDriveItem> GetAsyncEnumerator(Expression<Func<IDriveItem, bool>> filter, int skip, int take, CancellationToken cancellationToken = default);
        //hardcoded alternative not the worst thing ever
        //makes it easier to implement client (view) side too maybe...

        public IAsyncEnumerable<IDriveItem> GetAsyncEnumerator(DriveItemQuery? driveItemQuery, int skip, int take, CancellationToken cancellationToken = default);

        public Task<IDriveItem> GetOneAsync(DriveItemQuery? driveItemQuery, CancellationToken cancellationToken = default);

        public Task InsertOneAsync(string name, string description, IEnumerable<string> tags, Stream contentStream, DateTime? creationDate = null, CancellationToken cancellationToken = default);

        public Task UpdateAsync(DriveItemQuery? driveItemQuery, DriveItemUpdate driveItemUpdate, CancellationToken cancellationToken = default);
    
        public Task DeleteAsync(DriveItemQuery? driveItemQuery, CancellationToken cancellationToken = default);
    }
}
