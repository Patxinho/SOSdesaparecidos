using MvvmHelpers;
using SOSdesaparecidos.Services.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.ViewModels.Base
{
    public abstract class ViewModelBase : BaseViewModel, INotifyPropertyChanged
    {
        protected readonly INavigationService NavigationService;

        public ViewModelBase()
        {
            NavigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
