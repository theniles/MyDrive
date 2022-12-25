using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Maui.Behaviors.Validation
{
    internal abstract class ValidationBehavior<TValue> : ValidationBehavior
    {
        protected override Task<bool> ValidateAsync(object value)
        {
            return ValidateGenericAsync((TValue)value);
        }

        protected abstract Task<bool> ValidateGenericAsync(TValue value);
    }
}
