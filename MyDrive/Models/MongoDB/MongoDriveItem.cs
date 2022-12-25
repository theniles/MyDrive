using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Models.MongoDB
{
    internal class MongoDriveItem : IDriveItem<MongoDriveTag>
    {
        [BsonConstructor]
        public MongoDriveItem(string name, string description, DateTime creationDate, string[] tags)
        {
            Name = name;
            Description = description;
            CreationDate = creationDate;

        }

        public MongoDriveItem(IMongoCollection<MongoDriveItem> collection, IGridFSBucket bucket)
        {
            Collection = collection;
            Bucket = bucket;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<IDriveTag> Tags { get; set; }
        [BsonIgnore]
        public IMongoCollection<MongoDriveItem> Collection { get; }
        [BsonIgnore]
        public IGridFSBucket Bucket { get; }
        [BsonId]
        public ObjectId ObjectId { get; set; }

        public DateTime CreationDate => throw new NotImplementedException();

        [BsonIgnore]
        IReadOnlyList<MongoDriveTag> IDriveItem<MongoDriveTag>.Tags => throw new NotImplementedException();

        public IEnumerable<string> MongoTags => Tags.Select(p => p.Name);

        public void AddTag(MongoDriveTag tag)
        {
            throw new NotImplementedException();
        }

        public void RemoveTag(MongoDriveTag tag)
        {
            throw new NotImplementedException();
        }

        public void Update(string name = null, string description = null, DateTime? creationDate = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string name = null, string description = null, DateTime? creationDate = null)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSelfAsync(CancellationToken cancellationToken)
        {
            var result = await Collection.ReplaceOneAsync(Builders<MongoDriveItem>.Filter.Eq(p => p.ObjectId, ObjectId), this, cancellationToken: cancellationToken);
        }
    }
}
