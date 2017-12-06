using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Services.Dialog
{
    public class DialogService
    {
        public virtual async void ShowAlert(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title ?? string.Empty, message, cancel);
        }

        public virtual async Task<bool> ShowToDo(string title, string message, string accept, string cancel)
        {
            return await App.Current.MainPage.DisplayAlert(title ?? string.Empty, message, accept, cancel);
        }
    }
}
