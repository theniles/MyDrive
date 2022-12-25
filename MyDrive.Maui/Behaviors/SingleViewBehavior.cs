using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Maui.Behaviors
{
    //https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/behaviors?view=net-maui-7.0
    internal abstract class SingleBehavior<T> : Behavior<T> where T : BindableObject
    {
        private static readonly BindablePropertyKey OwnerPropertyKey =
        BindableProperty.CreateReadOnly(nameof(Owner), typeof(T), typeof(SingleBehavior<T>), null);
        public static readonly BindableProperty OwnerProperty = OwnerPropertyKey.BindableProperty;
        public T Owner { get => (T)GetValue(OwnerProperty); private set => SetValue(OwnerPropertyKey, value); }

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            if (Owner == null)
                Owner = bindable;
            else
                throw new InvalidOperationException();
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
            Owner = null;
        }
    }
}
