using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.ViewModel
{
    using BaseClass;
    using Navigation;
    public class MainViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel => NavigationVM.CurrentViewModel;
        private readonly Navigation NavigationVM;
        public MainViewModel()
        {
            NavigationVM = Navigation.Instance;
            NavigationVM.CurrentVMChanged += OnCurrentViewModelChanged;
            NavigationVM.CurrentViewModel = new LoginViewModel();
        }

        private void OnCurrentViewModelChanged()
        {
            onPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
