using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api.Updates
{
    public class StringUpdate
    {
        public StringUpdate(string newValue)
        {
            NewValue = newValue;
        }

        public string NewValue { get; }
    }
}
