using MyDrive.Api.Updates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api
{
    public interface IDriveItem
    {
        public string Name { get; }

        public string Description { get; }

        public DateTime CreationDate { get; }

        public IEnumerable<string> Tags { get; }

        public Task RefreshAsync(CancellationToken cancellationToken = default);

        public Task UpdateAsync(DriveItemUpdate driveItemUpdate, CancellationToken cancellationToken = default);
    
        public Task<Stream> GetContentStreamAsync(CancellationToken cancellationToken = default);

        public Task<Stream> SetContentStreamAsync(CancellationToken cancellationToken = default);
    }
}
