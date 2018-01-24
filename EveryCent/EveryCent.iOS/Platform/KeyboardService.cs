using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EveryCent.Services;
using Foundation;
using UIKit;

namespace EveryCent.iOS.Platform
{
    public class KeyboardService: IKeyboardService
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}