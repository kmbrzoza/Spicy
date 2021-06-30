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
    using Model;

    class LoginViewModel : BaseViewModel
    {
        private readonly Navigation NavigationVM;
        private Model model = Model.Instance;
        public LoginViewModel()
        {
            NavigationVM = Navigation.Instance;
        }

        #region PUBLIC PROPERTIES
        public string Login { get; set; }
        public string Password { get; set; }

        public string RegisterLogin { get; set; }
        public string RegisterPassword { get; set; }
        public string RegisterPasswordRepeat { get; set; }

        #endregion

        #region COMMANDS
        private ICommand signIn= null;
        public ICommand SignIn
        {
            get
            {
                if (signIn == null)
                {
                    signIn = new RelayCommand(
                        arg =>
                        {
                            //Console.WriteLine(Login + " " + Password);
                        },
                        arg => true
                        );
                }
                return signIn;
            }
        }
        #endregion
    }
}
