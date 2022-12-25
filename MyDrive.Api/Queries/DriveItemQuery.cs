using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api.Queries
{
    public class DriveItemQuery
    {
        public DriveItemQuery(StringQuery? nameQuery, StringQuery? descriptionQuery, DateQuery? creationDateQuery, TagQuery? tagQuery)
        {
            NameQuery = nameQuery;
            DescriptionQuery = descriptionQuery;
            CreationDateQuery = creationDateQuery;
            TagQuery = tagQuery;
        }

        public StringQuery? NameQuery { get; }

        public StringQuery? DescriptionQuery { get; }

        public DateQuery? CreationDateQuery { get; }

        public TagQuery? TagQuery { get; }
    }
}
