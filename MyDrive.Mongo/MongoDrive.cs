using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MyDrive.Api;
using MyDrive.Api.Queries;
using MyDrive.Api.Updates;
using MyDrive.Mongo.Queries;
using MyDrive.Mongo.Updates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Mongo
{
    internal class MongoDrive : IDrive
    {
        public IMongoCollection<MongoDriveItem> Collection { get; }

        public IGridFSBucket Bucket { get; }

        public MongoDrive(IMongoCollection<MongoDriveItem> collection, IGridFSBucket bucket)
        {
            Collection = collection;
            Bucket = bucket;
        }

        public IAsyncEnumerable<IDriveItem> GetAsyncEnumerator(DriveItemQuery? driveItemQuery, int skip, int take, CancellationToken cancellationToken = default)
        {
            var query = Collection.Find(driveItemQuery.Translate()).Skip(skip).Limit(take);
            return new FindAsyncEnumerable<MongoDriveItem>((FindFluentBase<MongoDriveItem, MongoDriveItem>)query, p => p.MongoDrive = this);
        }

        public async Task<IDriveItem> GetOneAsync(DriveItemQuery? driveItemQuery, CancellationToken cancellationToken = default)
        {
            var result = await Collection.FindAsync(driveItemQuery.Translate(), cancellationToken: cancellationToken);
            var item = await result.FirstAsync(cancellationToken);
            item.MongoDrive = this;
            return item;
        }

        public async Task InsertOneAsync(string name, string description, IEnumerable<string> tags, Stream contentStream, DateTime? creationDate = null, CancellationToken cancellationToken = default)
        {
            var fileId = await Bucket.UploadFromStreamAsync(name, contentStream, cancellationToken: cancellationToken);
            await Collection.InsertOneAsync(
                new MongoDriveItem(name, description, creationDate ?? DateTime.Now, tags, fileId, ObjectId.Empty),
                null,
                cancellationToken);
        }

        public async Task UpdateAsync(DriveItemQuery? driveItemQuery, DriveItemUpdate driveItemUpdate, CancellationToken cancellationToken = default)
        {
            await Collection.UpdateManyAsync(driveItemQuery.Translate(), driveItemUpdate.Translate(), cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(DriveItemQuery? driveItemQuery, CancellationToken cancellationToken = default)
        {
            var result = await Collection.FindAsync(driveItemQuery.Translate(), cancellationToken: cancellationToken);
            
            await result.ForEachAsync(async (p) =>
            {
                await Bucket.DeleteAsync(p.FileId, cancellationToken: cancellationToken);
            }, cancellationToken: cancellationToken);

            await Collection.DeleteManyAsync(driveItemQuery.Translate(), cancellationToken);
        }

        public void Dispose()
        {
            //nothing to dispose
        }
    }
}
