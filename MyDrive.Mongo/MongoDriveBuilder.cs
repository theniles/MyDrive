using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MyDrive.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Mongo
{
    public static class MongoDriveBuilder
    {
        public static IDrive CreateMongoDrive(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            
            return new MongoDrive(db.GetCollection<MongoDriveItem>(collectionName), new GridFSBucket(db));
        }
    }
}
