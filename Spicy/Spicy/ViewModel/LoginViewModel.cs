using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.ViewModel
{
    using BaseClass;
    using System.Windows.Input;
    using Services;
    using Model;
    using DAL.Entities;
    using System.Windows;

    class LoginViewModel : BaseViewModel
    {
        private readonly Navigation NavigationVM;
        private readonly Model model;
        private readonly AccountManager accountManager;
        public LoginViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;
        }

        #region PUBLIC PROPERTIES
        //Login
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                if (value.Length <= Constants.MAX_USER_NICKNAME_LENGTH)
                    login = value;
                onPropertyChanged(nameof(Login));
            }
        }
        private string password { get; set; }
        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length <= Constants.MAX_USER_PASSWORD_LENGTH)
                    password = value;
                onPropertyChanged(nameof(Password));
            }
        }
        //Register
        private string registerLogin;
        public string RegisterLogin
        {
            get { return registerLogin; }
            set
            {
                if (value.Length <= Constants.MAX_USER_NICKNAME_LENGTH)
                    registerLogin = value;
                onPropertyChanged(nameof(RegisterLogin));
            }
        }
        private string registerPassword;
        public string RegisterPassword
        {
            get { return registerPassword; }
            set
            {
                if (value.Length <= Constants.MAX_USER_PASSWORD_LENGTH)
                    registerPassword = value;
                onPropertyChanged(nameof(RegisterPassword));
            }
        }
        private string registerPasswordRepeat;
        public string RegisterPasswordRepeat
        {
            get { return registerPasswordRepeat; }
            set
            {
                if (value.Length <= Constants.MAX_USER_PASSWORD_LENGTH)
                    registerPasswordRepeat = value;
                onPropertyChanged(nameof(RegisterPasswordRepeat));
            }
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
                            if (accountManager.LoginUser(new User(Login, Password)))
                            {
                                UserSession.NewSession();
                                NavigationVM.CurrentViewModel = new HomeViewModel(model);
                            }
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

                            bool result = false;
                            try
                            {
                                result = accountManager.RegisterUser(new User(RegisterLogin, RegisterPassword));
                            }
                            catch(Exception e)
                            {
                                MessageBox.Show(e.Message, "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }

                            if (result)
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
