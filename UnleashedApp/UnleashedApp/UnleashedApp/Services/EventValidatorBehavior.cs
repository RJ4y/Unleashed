﻿using UnleashedApp.ViewModels;
using Xamarin.Forms;

namespace UnleashedApp.Services
{
    public class EventValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += HandleTextChanged;
            base.OnAttachedTo(entry);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool validEvent = e.NewTextValue.Length > 0;
            ((Entry)sender).TextColor = validEvent ? Color.DarkGray : Color.Red;

            var vm = ((Entry)sender).BindingContext;
            ((TrainingViewModel)vm).IsEventValid = validEvent;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
