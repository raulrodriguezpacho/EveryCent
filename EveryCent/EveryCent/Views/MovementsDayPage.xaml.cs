using EveryCent.Base;
using EveryCent.Model;
using EveryCent.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EveryCent.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovementsDayPage : ViewPageBase
	{
		public MovementsDayPage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// NOT GOOD BUT ..
        /// Command="{Binding Path=BindingContext.DeleteMovementCommand, Source={x:Reference movmentsDayPage}}" -> page x:Name=movmentsDayPage
        /// NOT WORKING = Can't resolve name on Element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var movementToDelete = (Movement)((MenuItem)sender).CommandParameter;
                ((MovementsDayViewModel)this.BindingContext).DeleteMovementCommand.Execute(movementToDelete);
            }
            catch { }
        }
    }
}