using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Spicy.ViewModel
{
    using BaseClass;
    using Services;
    using System.Windows.Input;
    using Model;
    using DAL.Entities;

    class HomeViewModel : BaseViewModel
    {
        private readonly Navigation NavigationVM;
        private readonly Model model;
        private readonly AccountManager accountManager;
        public HomeViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;

            ActualDiscounts = model.GetActualDiscounts();
        }

        #region PRIVATE Components
        private ObservableCollection<Discount> actualDiscounts;
        private int indexOfSelectedDiscount = -1;
        #endregion

        #region PROPS FOR VIEW
        public ObservableCollection<Discount> ActualDiscounts
        {
            get { return actualDiscounts; }
            set { actualDiscounts = value; onPropertyChanged(nameof(ActualDiscounts)); }
        }
        public Discount SelectedDiscount { get; set; }
        public int IndexOfSelectedDiscount
        {
            get { return indexOfSelectedDiscount; }
            set { indexOfSelectedDiscount = value; onPropertyChanged(nameof(IndexOfSelectedDiscount)); }
        }
        #endregion

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
                            NavigationVM.CurrentViewModel = new AddDiscountViewModel(model);
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
