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

            actualDiscounts = model.GetActualDiscounts();
            ActualDiscounts = new ObservableCollection<DiscountItem>();
            foreach (var adiscount in actualDiscounts)
                ActualDiscounts.Add(new DiscountItem(adiscount.Id, adiscount.Name, adiscount.Image, adiscount.CurrentPrice,
                    adiscount.PreviousPrice, adiscount.End_Date));
        }

        #region PRIVATE Components
        private ObservableCollection<Discount> actualDiscounts;
        private int indexOfSelectedDiscount = -1;
        #endregion

        #region PROPS FOR VIEW
        public ObservableCollection<DiscountItem> ActualDiscounts { get; set; }
        public DiscountItem SelectedDiscount { get; set; }
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

        private ICommand goToDiscount = null;
        public ICommand GoToDiscount
        {
            get
            {
                if (goToDiscount == null)
                {
                    goToDiscount = new RelayCommand(
                        arg =>
                        {
                            //if(IndexOfSelectedDiscount > -1)
                            var discount = model.GetDiscountById(ActualDiscounts.ElementAt(IndexOfSelectedDiscount).DiscountId);
                            if (discount != null)
                                NavigationVM.CurrentViewModel = new DiscountViewModel(model, discount);
                            Console.WriteLine("test2");
                        },
                        arg => true
                        );
                }
                return goToDiscount;
            }
        }

        private ICommand addShop = null;
        public ICommand AddShop
        {
            get
            {
                if (addShop == null)
                {
                    addShop = new RelayCommand(
                        arg =>
                        {
                            NavigationVM.CurrentViewModel = new AddShopViewModel(model);
                        },
                        arg => true
                        );
                }
                return addShop;
            }
        }
        #endregion
    }
}
