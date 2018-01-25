using EveryCent.Base;
using EveryCent.Model;
using EveryCent.Services;
using EveryCent.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EveryCent.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ViewPageBase
    {
        public ListPage()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// NOT GOOD BUT ..
        /// Command="{Binding Path=BindingContext.DeleteMovementCommand, Source={x:Reference listPage}}" -> page x:Name=listPage
        /// NOT WORKING = Can't resolve name on Element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Clicked(object sender, System.EventArgs e)
        {            
            try
            {
                var movementToDelete = (Movement)((MenuItem)sender).CommandParameter;
                ((ListViewModel)this.BindingContext).DeleteMovementCommand.Execute(movementToDelete);
            }
            catch { }
        }
    }
}