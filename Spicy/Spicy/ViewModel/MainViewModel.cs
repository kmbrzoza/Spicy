using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.ViewModel
{
    using BaseClass;
    using Services;
    using Model;
    class MainViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel => NavigationVM.CurrentViewModel;
        private readonly Navigation NavigationVM;
        private readonly Model model = new Model();
        public MainViewModel()
        {
            NavigationVM = Navigation.Instance;
            NavigationVM.CurrentVMChanged += OnCurrentViewModelChanged;
            NavigationVM.CurrentViewModel = new LoginViewModel(model);
        }

        private void OnCurrentViewModelChanged()
        {
            onPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
