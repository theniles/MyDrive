using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api.Updates
{
    public class TagUpdate
    {
        public IEnumerable<string>? ToAddTags { get; }

        public IEnumerable<string>? ToRemoveTags { get; }
    }
}
