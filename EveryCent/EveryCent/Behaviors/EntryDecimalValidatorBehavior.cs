using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EveryCent.Behaviors
{
    public class EntryDecimalValidatorBehavior : Behavior<Entry>
    {
        public int MaxLength { get; set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            string entryText = entry.Text;
            if (entry.Text.Length > this.MaxLength)
            {                
                entry.TextChanged -= OnEntryTextChanged;
                entry.Text = e.OldTextValue;
                entry.TextChanged += OnEntryTextChanged;
            }
            else
            {
                decimal result = decimal.Zero;
                if (!decimal.TryParse(entryText, out result))
                {
                    entry.TextChanged -= OnEntryTextChanged;
                    if (!string.IsNullOrEmpty(entryText))
                        entry.Text = e.OldTextValue;
                    entry.TextChanged += OnEntryTextChanged;
                    return;
                }
                var decimalPoint = entryText.IndexOf('.');
                if (decimalPoint >= 0 && entryText.Length >= decimalPoint + 4)
                {
                    entry.TextChanged -= OnEntryTextChanged;
                    entry.Text = e.OldTextValue;
                    entry.TextChanged += OnEntryTextChanged;
                    return;
                }                
            }            
        }
    }
}
