using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDrive.Api;
using MyDrive.Api.Updates;
using MyDrive.Mongo.Updates;

namespace MyDrive.Mongo
{ 
    internal class MongoDriveItem : IDriveItem
    {
        //[BsonConstructor]
        public MongoDriveItem(string name, string description, DateTime creationDate, IEnumerable<string> tagNames, ObjectId fileId, ObjectId id)
        {
            Name = name;
            Description = description;
            CreationDate = creationDate;
            Tags = tagNames;
            FileId = fileId;
            Id = id;
        }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public DateTime CreationDate { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public ObjectId FileId { get; set; }

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonIgnore]
        public MongoDrive? MongoDrive { get; set; }

        public async Task RefreshAsync(CancellationToken cancellationToken)
        {
            if(MongoDrive != null)
            {
                var cursor = await MongoDrive.Collection.FindAsync(Builders<MongoDriveItem>.Filter.Eq(p => p.Id, Id), cancellationToken: cancellationToken);
                var result = await cursor.FirstAsync(cancellationToken);
                Name = result.Name;
                Description = result.Description;
                CreationDate = result.CreationDate;
                Tags = result.Tags;
            }
        }

        public async Task UpdateAsync(DriveItemUpdate driveItemUpdate, CancellationToken cancellationToken = default)
        {
            if(MongoDrive != null)
            {
                await MongoDrive.Collection.UpdateOneAsync(
                    Builders<MongoDriveItem>.Filter.Eq(p => p.Id, Id),
                    driveItemUpdate.Translate(),
                    cancellationToken: cancellationToken);
            }
        }

        public async Task<Stream> GetContentStreamAsync(CancellationToken cancellationToken = default)
        {
            if(MongoDrive != null)
            {
                return await MongoDrive.Bucket.OpenDownloadStreamAsync(FileId, cancellationToken: cancellationToken);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<Stream> SetContentStreamAsync(CancellationToken cancellationToken = default)
        {
            if (MongoDrive != null)
            {
                return await MongoDrive.Bucket.OpenUploadStreamAsync(FileId, Name, cancellationToken: cancellationToken);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
