using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Models
{
    internal interface IDriveTag
    {
        public string Name { get; }

        public Task<long> GetCountAsync();
    }
}
