using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api.Queries
{
    public class StringQuery
    {
        public StringQuery(string text, StringQueryType type)
        {
            Text = text;
            Type = type;
        }

        public string Text { get; }

        public StringQueryType Type { get; }
    }
}
