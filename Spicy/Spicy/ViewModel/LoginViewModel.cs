using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.ViewModel
{
    using BaseClass;
    using System.Windows.Input;
    using Navigation;

    class LoginViewModel : BaseViewModel
    {
        private readonly Navigation NavigationVM;
        public LoginViewModel()
        {
            NavigationVM = Navigation.Instance;
        }

        #region COMMANDS
        private ICommand login = null;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(
                        arg =>
                        {
                            //NavigationVM.CurrentViewModel = new HomeViewModel(); // TESTING
                        },
                        arg => true
                        );
                }
                return login;
            }
        }
        #endregion
    }
}
