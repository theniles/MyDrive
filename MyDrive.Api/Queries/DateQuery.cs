using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api.Queries
{
    public class DateQuery
    {
        public DateQuery(DateTime date, DateTimeQueryType type)
        {
            Date = date;
            Type = type;
        }

        public DateTime Date { get; }

        public DateTimeQueryType Type { get; }
    }
}
