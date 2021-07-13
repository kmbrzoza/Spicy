using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.ViewModel
{
    using BaseClass;
    using Model;
    using Services;
    using System.Windows.Input;
    using DAL.Entities;

    class AddShopViewModel : BaseViewModel
    {
        private readonly Navigation NavigationVM;
        private readonly Model model;
        private readonly AccountManager accountManager;
        public AddShopViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;
        }

        #region PRIVATE Components
        private string shopName;
        private string shopDescription;
        #endregion

        #region PROPS FOR VIEW
        public string ShopName
        {
            get { return shopName; }
            set
            {
                if (value.Length <= Consts.MAX_SHOP_NAME)
                    shopName = value;
                onPropertyChanged(nameof(ShopName));
            }
        }
        public string ShopDescription
        {
            get { return shopDescription; }
            set
            {
                if (value.Length <= Consts.MAX_SHOP_DESCRIPTION)
                    shopDescription = value;
                onPropertyChanged(nameof(ShopDescription));
            }
        }
        #endregion

        #region COMMANDS
        private ICommand cancel = null;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(
                        arg =>
                        {
                            NavigationVM.CurrentViewModel = new HomeViewModel(model);
                        },
                        arg => true
                        );
                }
                return cancel;
            }
        }

        private ICommand add = null;
        public ICommand Add
        {
            get
            {
                if (add == null)
                {
                    add = new RelayCommand(
                        arg =>
                        {
                            var shop = new Shop(ShopName, ShopDescription);
                            model.AddShop(shop);
                            NavigationVM.CurrentViewModel = new HomeViewModel(model);
                        },
                        arg => !string.IsNullOrEmpty(ShopName) && !string.IsNullOrEmpty(ShopDescription)
                        );
                }
                return add;
            }
        }
        #endregion

        #region METHODS

        #endregion
    }
}
