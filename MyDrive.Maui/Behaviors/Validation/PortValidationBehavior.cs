using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Maui.Behaviors.Validation
{
    internal class PortValidationBehavior : ValidationBehavior<string>
    {
        protected override Task<bool> ValidateGenericAsync(string value)
        {
            return Task.FromResult(ushort.TryParse(value, out _));
        }
    }
}
