using MongoDB.Driver;
using MyDrive.Api.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Mongo.Queries
{
    internal static class DriveItemQueryExtensions
    {
        public static FilterDefinition<MongoDriveItem> Translate(this DriveItemQuery? driveItemQuery)
        {
            var builder = Builders<MongoDriveItem>.Filter;

            FilterDefinition<MongoDriveItem>? finalFilter = null;

            //the way to implement fully customisable queries is to have a driveItemQuery[] and to have
            //them be connected by conditional operators
            if (driveItemQuery != null)
            {
                if (driveItemQuery.NameQuery != null)
                {
                    var filter = driveItemQuery.NameQuery.GetFilter<MongoDriveItem>(p => p.Name);
                    finalFilter = finalFilter == null ? filter : builder.And(filter, finalFilter);
                }
                if (driveItemQuery.DescriptionQuery != null)
                {
                    var filter = driveItemQuery.DescriptionQuery.GetFilter<MongoDriveItem>(p => p.Description);
                    finalFilter = finalFilter == null ? filter : builder.And(filter, finalFilter);
                }
                if (driveItemQuery.CreationDateQuery != null)
                {
                    var filter = driveItemQuery.CreationDateQuery.GetFilter<MongoDriveItem>(p => p.CreationDate);
                    finalFilter = finalFilter == null ? filter : builder.And(filter, finalFilter);
                }
                if (driveItemQuery.TagQuery != null)
                {
                    //TODO tag query
                }
            }


            if (finalFilter == null)
                finalFilter = builder.Empty;

            return finalFilter;
        }
    }
}
