using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Models.MongoDB
{
    internal class MongoDriveTag : IDriveTag
    {
        public MongoDriveTag(IMongoCollection<MongoDriveItem> collection, string name)
        {
            Collection = collection;
            Name = name;
        }

        public IMongoCollection<MongoDriveItem> Collection { get; }

        public string Name { get; }

        public async Task<long> GetCountAsync(CancellationToken cancellationToken)
        {
            return await Collection.CountDocumentsAsync(p => p.MongoTags.Contains(Name), cancellationToken: cancellationToken);
        }
    }
}
