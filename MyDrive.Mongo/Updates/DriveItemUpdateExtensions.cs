using MongoDB.Driver;
using MyDrive.Api.Updates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Mongo.Updates
{
    internal static class DriveItemUpdateExtensions
    {
        public static UpdateDefinition<MongoDriveItem> Translate(this DriveItemUpdate driveItemUpdate)
        {
            var builder = Builders<MongoDriveItem>.Update;

            UpdateDefinition<MongoDriveItem>? finalUpdate = null;

            if(driveItemUpdate != null)
            {
                if(driveItemUpdate.NameUpdate != null)
                {
                    finalUpdate = 
                        finalUpdate == null ?
                        builder.Set(p => p.Name, driveItemUpdate.NameUpdate.NewValue) :
                        finalUpdate.Set(p => p.Name, driveItemUpdate.NameUpdate.NewValue);
                }
                if (driveItemUpdate.DescriptionUpdate != null)
                {
                    finalUpdate =
                        finalUpdate == null ?
                        builder.Set(p => p.Description, driveItemUpdate.DescriptionUpdate.NewValue) :
                        finalUpdate.Set(p => p.Description, driveItemUpdate.DescriptionUpdate.NewValue);
                }
                if (driveItemUpdate.TagUpdate != null)
                {
                    finalUpdate = finalUpdate == null ?
                        builder
                        .PullAll(p => p.Tags, driveItemUpdate.TagUpdate.ToRemoveTags)
                        .AddToSetEach(p => p.Tags, driveItemUpdate.TagUpdate.ToAddTags) :
                        finalUpdate
                        .PullAll(p => p.Tags, driveItemUpdate.TagUpdate.ToRemoveTags)
                        .AddToSetEach(p => p.Tags, driveItemUpdate.TagUpdate.ToAddTags);
                }
            }

            return finalUpdate ?? throw new ArgumentException();
        }
    }
}
