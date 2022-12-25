using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Models.MongoDB
{
    internal class MongoDrive : IDrive<MongoDriveItem, MongoDriveTag>
    {
        private IMongoCollection<MongoDriveItem> Collection;

        public MongoDrive(IMongoCollection<MongoDriveItem> collection)
        {
            Collection = collection;
        }

        public IAsyncEnumerable<MongoDriveItem> GetAsyncEnumerator(Expression<Func<MongoDriveItem, bool>> expression, int skip, int take, CancellationToken cancellationToken = default)
        {
            var query = Collection.Find(expression).Skip(skip).Limit(take);
            return new FindAsyncEnumerable<MongoDriveItem>((FindFluentBase<MongoDriveItem, MongoDriveItem>)query);
        }

        public async Task InsertTagsAsync(Expression<Func<MongoDriveItem, bool>> filter, CancellationToken cancellationToken = default, params MongoDriveTag[] tags)
        {
            await Collection.UpdateManyAsync(filter, Builders<MongoDriveItem>.Update.AddToSetEach(p => p.MongoTags, tags.Select(p => p.Name)), cancellationToken: cancellationToken);
        }

        public Task InsertTagsAsync(Expression<Func<MongoDriveItem, bool>> filter, params MongoDriveTag[] tags)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveTagsAsync(Expression<Func<MongoDriveItem, bool>> filter, CancellationToken cancellationToken = default, params MongoDriveTag[] tags)
        {
            await Collection.UpdateManyAsync(filter, Builders<MongoDriveItem>.Update.PullAll(p => p.MongoTags, tags.Select(p => p.Name)), cancellationToken: cancellationToken);
        }
    }
}
