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
    using DAL.Entities;
    using System.Windows;

    class LoginViewModel : BaseViewModel
    {
        private readonly Navigation NavigationVM;
        private Model model;
        public LoginViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
        }

        #region PUBLIC PROPERTIES
        //Login
        public string Login { get; set; }
        public string Password { get; set; }
        //Register
        private string registerLogin;
        public string RegisterLogin
        {
            get { return registerLogin; }
            set { registerLogin = value; onPropertyChanged(nameof(RegisterLogin)); }
        }
        private string registerPassword;
        public string RegisterPassword
        {
            get { return registerPassword; }
            set { registerPassword = value; onPropertyChanged(nameof(RegisterPassword)); }
        }
        private string registerPasswordRepeat;
        public string RegisterPasswordRepeat
        {
            get { return registerPasswordRepeat; }
            set { registerPasswordRepeat = value; onPropertyChanged(nameof(RegisterPasswordRepeat)); }
        }

        #endregion

        #region COMMANDS
        private ICommand signIn = null;
        public ICommand SignIn
        {
            get
            {
                if (signIn == null)
                {
                    signIn = new RelayCommand(
                        arg =>
                        {
                            if (model.LoginUser(new User(Login, Password)))
                                NavigationVM.CurrentViewModel = new HomeViewModel();
                            else
                                MessageBox.Show("Nieprawidłowe dane logowania!", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        },
                        arg => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password)
                        );
                }
                return signIn;
            }
        }

        private ICommand register = null;
        public ICommand Register
        {
            get
            {
                if (register == null)
                {
                    register = new RelayCommand(
                        arg =>
                        {
                            if (RegisterPassword.CompareTo(RegisterPasswordRepeat) != 0)
                            {
                                MessageBox.Show("Hasła muszą być takie same!", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                            if (model.RegisterUser(new User(RegisterLogin, RegisterPassword)))
                            {
                                MessageBox.Show("Rejestracja udana!\nMożesz się teraz zalogować.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                                ClearRegisterInputs();
                            }
                            else
                                MessageBox.Show("Użytkownik o takim Loginie już istnieje!", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        },
                        arg => !string.IsNullOrEmpty(RegisterLogin) && !string.IsNullOrEmpty(RegisterPassword) && !string.IsNullOrEmpty(RegisterPasswordRepeat)
                        );
                }
                return register;
            }
        }
        #endregion

        #region METHODS
        private void ClearRegisterInputs()
        {
            RegisterLogin = "";
            RegisterPassword = "";
            RegisterPasswordRepeat = "";
        }

        #endregion
    }
}
