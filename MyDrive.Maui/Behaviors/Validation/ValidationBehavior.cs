using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Maui.Behaviors.Validation
{
    internal abstract class ValidationBehavior : SingleBehavior<VisualElement>
    {
        /*
         * Binding context of the view is not auto set to the behavior by maui
         * The properties are initialised and the changed events are fired
         * before the behavior is attached
         */

        public const string ValidStateName = "Valid";
        public const string InvalidStateName = "Invalid";
        public const string ValidatingStateName = "Vaidating";
        
        public static readonly BindableProperty ValidStyleProperty =
        BindableProperty.Create(nameof(ValidStyle), typeof(Style), typeof(ValidationBehavior));
        public Style ValidStyle { get => (Style)GetValue(ValidStyleProperty); set => SetValue(ValidStyleProperty, value); }

        public static readonly BindableProperty InvalidStyleProperty =
        BindableProperty.Create(nameof(InvalidStyle), typeof(Style), typeof(ValidationBehavior));
        public Style InvalidStyle { get => (Style)GetValue(InvalidStyleProperty); set => SetValue(InvalidStyleProperty, value); }

        public static readonly BindableProperty ValidatingStyleProperty =
        BindableProperty.Create(nameof(ValidatingStyle), typeof(Style), typeof(ValidationBehavior));
        public Style ValidatingStyle { get => (Style)GetValue(ValidatingStyleProperty); set => SetValue(ValidatingStyleProperty, value); }

        public static readonly BindableProperty ValidityProperty =
        BindableProperty.Create(nameof(Validity), typeof(Validity), typeof(ValidationBehavior), defaultValue: Validity.Validating , propertyChanged: OnValidityPropertyChanged);
        public Validity Validity { get => (Validity)GetValue(ValidityProperty); set => SetValue(ValidityProperty, value); }

        public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(object), typeof(ValidationBehavior), propertyChanged: OnValuePropertyChanged);
        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        public static readonly BindableProperty ValuePropertyPathProperty =
        BindableProperty.Create(nameof(ValuePropertyPath), typeof(string), typeof(ValidationBehavior), propertyChanged: OnValuePropertyPathPropertyChanged);
        public string ValuePropertyPath { get => (string)GetValue(ValuePropertyPathProperty); set => SetValue(ValuePropertyPathProperty, value); }

        private static void OnValuePropertyPathPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(oldValue != newValue)
                ((ValidationBehavior)(bindable)).OnValuePropertyPathChanged();
        }

        private void OnValuePropertyPathChanged()
        {
            if(Owner != null && ValuePropertyPath != null)
            {
                SetBinding(ValueProperty, new Binding(ValuePropertyPath, BindingMode.OneWay, source: Owner));
                boundValueFromOwner = true;
            }
        }

        private Task<bool> ValidationTask;

        private bool boundValueFromOwner;

        protected abstract Task<bool> ValidateAsync(object value);

        private static void OnValidityPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(oldValue != newValue)
                ((ValidationBehavior)bindable).OnValidityChanged();
        }

        private void OnValidityChanged()
        {
            if(Owner != null)
            {
                switch (Validity)
                {
                    case Validity.Valid:
                        Owner.Style = ValidStyle;
                        VisualStateManager.GoToState(Owner, ValidStateName);
                        break;
                    case Validity.Invalid:
                        Owner.Style = InvalidStyle;
                        VisualStateManager.GoToState(Owner, InvalidStateName);
                        break;
                    case Validity.Validating:
                        Owner.Style = ValidatingStyle;
                        VisualStateManager.GoToState(Owner, ValidatingStateName);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private static async void OnValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            await ((ValidationBehavior)bindable).OnValueChanged(newValue);
        }

        private async Task OnValueChanged(object newValue)
        {
            if(Validity != Validity.Validating)
                Validity = Validity.Validating;

            using (var validationTask = ValidateAsync(newValue))
            {
                ValidationTask = validationTask;

                var isValid = await validationTask;

                if(validationTask == ValidationTask)
                {
                    Validity = isValid ? Validity.Valid : Validity.Invalid;
                }
            }
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            OnValuePropertyPathChanged();
            OnValidityChanged();
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            base.OnDetachingFrom(bindable);
            if(boundValueFromOwner)
            {
                RemoveBinding(ValueProperty);
                boundValueFromOwner = false;
            }
        }
    }
}
