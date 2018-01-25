using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryCent.ViewModels.Base;
using Xamarin.Forms;
using Autofac;

namespace EveryCent.Base
{
    public class ViewPageBase : ContentPage
    {
        readonly ViewModelBase _viewModel;
        public ViewModelBase ViewModel
        {
            get { return _viewModel; }            
        }

        public ViewPageBase()
        {                        
            var viewType = this.GetType();
            var viewModelName = viewType.FullName.Replace(".Views.", ".ViewModels.").Replace("Page", "ViewModel");
            var viewModelType = Type.GetType(viewModelName);
            _viewModel = (ViewModelBase)LocatorBase.Container.Resolve(viewModelType);
            BindingContext = _viewModel;                 
        }        
    }
}
