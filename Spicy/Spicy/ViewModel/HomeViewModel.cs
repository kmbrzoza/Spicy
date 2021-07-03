using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.ViewModel
{
    using BaseClass;
    using Services;
    using System.Windows.Input;
    using Model;

    class HomeViewModel: BaseViewModel
    {
        private readonly Navigation NavigationVM;
        private readonly Model model;
        private readonly AccountManager accountManager;
        public HomeViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;
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

        private ICommand logOut = null;
        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(
                        arg =>
                        {
                            if (accountManager.LogOut())
                                NavigationVM.CurrentViewModel = new LoginViewModel(model);
                        },
                        arg => true
                        );
                }
                return logOut;
            }
        }
        #endregion
    }
}
