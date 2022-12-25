using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Models
{
    internal interface IDriveItem<TTag> where TTag : IDriveTag
    {
        public string Name { get; }

        public string Description { get; }

        public DateTime CreationDate { get; }

        public IEnumerable<TTag> Tags { get; }

        public Task UpdateAsync(string name = null, string description = null, DateTime? creationDate = null);

        public void Update(string name = null, string description = null, DateTime? creationDate = null);

        public Task AddTagAsync(TTag tag);

        public void AddTag(TTag tag);

        public void RemoveTag(TTag tag);

        public Task RemoveTagAsync(TTag tag);
    }
}
