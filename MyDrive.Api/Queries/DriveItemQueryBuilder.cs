using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api.Queries
{
    //TODO update and query builders
    public class DriveItemQueryBuilder
    {
        public string? Name { get; set; }

        public StringQueryType NameQueryType { get; set; }

        public string? Description { get; set; }

        public StringQueryType DescriptionQueryType { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTimeQueryType CreationDateQueryType { get; set; }

        public IEnumerable<string>? Tags { get; set; }

        public TagQueryType TagQueryType { get; set; }

        public DriveItemQueryBuilder InName(string name)
        {
            Name = name;
            NameQueryType = StringQueryType.Include;
            return this;
        }

        public DriveItemQueryBuilder InDescription(string name)
        {
            Description = name;
            DescriptionQueryType = StringQueryType.Include;
            return this;
        }

        public DriveItemQueryBuilder Before(DateTime dateTime)
        {
            CreationDate = dateTime;
            CreationDateQueryType = DateTimeQueryType.BeforeDay;
            return this;
        }

        public DriveItemQueryBuilder After(DateTime dateTime)
        {
            CreationDate = dateTime;
            CreationDateQueryType = DateTimeQueryType.AfterDay;
            return this;
        }

        public DriveItemQueryBuilder SameDay(DateTime dateTime)
        {
            CreationDate = dateTime;
            CreationDateQueryType = DateTimeQueryType.SameDay;
            return this;
        }

        public DriveItemQueryBuilder WithTags(IEnumerable<string> tags)
        {
            Tags = tags;
            TagQueryType = TagQueryType.Contains;
            return this;
        }

        public DriveItemQueryBuilder WithoutTags(IEnumerable<string> tags)
        {
            Tags = tags;
            TagQueryType = TagQueryType.Excludes;
            return this;
        }

        public DriveItemQuery Build()
        {
            return new DriveItemQuery(
                Name == null ? null : new StringQuery(Name, NameQueryType),
                Description == null ? null : new StringQuery(Description, DescriptionQueryType),
                CreationDate == null ? null : new DateQuery(CreationDate.Value, CreationDateQueryType),
                Tags == null ? null : new TagQuery(Tags, TagQueryType));
        }
    }
}
