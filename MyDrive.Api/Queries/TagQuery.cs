using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api.Queries
{
    public class TagQuery
    {
        public TagQuery(IEnumerable<string> tagNames, TagQueryType type)
        {
            TagNames = tagNames;
            Type = type;
        }

        public IEnumerable<string> TagNames { get; }

        public TagQueryType Type { get; }
    }
}
