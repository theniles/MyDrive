using MongoDB.Driver;
using MyDrive.Api.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Mongo.Queries
{
    internal static class DateQueryExtensions
    {
        public static FilterDefinition<TDocument> GetFilter<TDocument>(this DateQuery? dateQuery, Expression<Func<TDocument, DateTime>> property)
        {
            if(dateQuery == null)
            {
                return Builders<TDocument>.Filter.Empty;
            }
            else
            {
                switch (dateQuery.Type)
                {
                    case DateTimeQueryType.SameDay:
                        return
                            Builders<TDocument>.Filter.And(
                                Builders<TDocument>.Filter.Gte(property, dateQuery.Date.Date),
                                Builders<TDocument>.Filter.Lt(property, dateQuery.Date.Date.AddDays(1)));
                    case DateTimeQueryType.BeforeDay:
                        return Builders<TDocument>.Filter.Lt(property, dateQuery.Date.Date);
                    case DateTimeQueryType.AfterDay:
                        return Builders<TDocument>.Filter.Gt(property, dateQuery.Date.Date);
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
