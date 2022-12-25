using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Models
{
    internal interface IDrive<TItem, TTag> where TTag : IDriveTag where TItem : IDriveItem<TTag>
    {
        public IAsyncEnumerable<TItem> GetAsyncEnumerator(Expression<Func<TItem, bool>> filter, int skip, int take, CancellationToken cancellationToken = default);
    
        public Task InsertTagsAsync(Expression<Func<TItem, bool>> filter, CancellationToken cancellationToken = default, params TTag[] tags);

        public Task RemoveTagsAsync(Expression<Func<TItem, bool>> filter, CancellationToken cancellationToken = default, params TTag[] tags);
    }
}
