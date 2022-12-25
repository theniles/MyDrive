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
    internal static class StringQueryExtensions
    {
        public static FilterDefinition<TDocument> GetFilter<TDocument>(this StringQuery? stringQuery, Expression<Func<TDocument, string>> property)
        {
            if(stringQuery == null)
            {
                return Builders<TDocument>.Filter.Empty;
            }
            else
            {
                switch (stringQuery.Type)
                {
                    case StringQueryType.Equal:
                        return Builders<TDocument>.Filter.Eq(property, stringQuery.Text);
                    case StringQueryType.Include:
                        return Builders<TDocument>.Filter.StringIn(property, stringQuery.Text);
                    case StringQueryType.Exclude:
                        return Builders<TDocument>.Filter.StringNin(property, stringQuery.Text);
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
