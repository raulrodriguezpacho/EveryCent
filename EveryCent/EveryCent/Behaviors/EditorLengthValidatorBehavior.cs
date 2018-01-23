using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EveryCent.Behaviors
{
    public class EditorLengthValidatorBehavior : Behavior<Editor>
    {
        public int MaxLength { get; set; }

        protected override void OnAttachedTo(Editor bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Editor bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var editor = (Editor)sender;

            if (editor.Text.Length > this.MaxLength)
            {
                string editorText = editor.Text;
                editor.TextChanged -= OnEntryTextChanged;
                editor.Text = e.OldTextValue;
                editor.TextChanged += OnEntryTextChanged;
            }
        }
    }
}
