using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.ViewModel
{
    using BaseClass;
    using Navigation;
    using System.Windows.Input;
    using Model;

    class HomeViewModel: BaseViewModel
    {
        private readonly Navigation NavigationVM;
        private readonly Model model;
        public HomeViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
        }

        #region COMMANDS
        private ICommand addDiscount = null;
        public ICommand AddDiscount
        {
            get
            {
                if (addDiscount == null)
                {
                    addDiscount = new RelayCommand(
                        arg =>
                        {
                            //NavigationVM.CurrentViewModel = new LoginViewModel(); // TESTING
                        },
                        arg => true
                        );
                }
                return addDiscount;
            }
        }
        #endregion
    }
}
