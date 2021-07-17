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
        private readonly UserSession userSession;
        public HomeViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;
            userSession = UserSession.Instance;

            SetActualDiscounts();
            SetActualDiscountsBySearch();
        }

        #region PRIVATE Components
        private ObservableCollection<Discount> actualDiscounts;
        #endregion

        #region PROPS FOR VIEW
        public ObservableCollection<DiscountInfo> ActualDiscounts { get; set; }
        public DiscountInfo SelectedDiscount { get; set; }
        public int IndexOfSelectedDiscount
        {
            get { return userSession.Home_SelectedIndexOfDiscount; }
            set { userSession.Home_SelectedIndexOfDiscount = value; onPropertyChanged(nameof(IndexOfSelectedDiscount)); }
        }
        public string Search
        {
            get { return userSession.Home_SearchBy; }
            set
            {
                userSession.Home_SearchBy = value;
                SetActualDiscountsBySearch();
            }
        }
        public bool ShowEnded
        {
            get { return userSession.Home_ShowEnded; }
            set
            {
                userSession.Home_ShowEnded = value;
                SetActualDiscounts();
                SetActualDiscountsBySearch();
            }
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
                        },
                        arg => true
                        );
                }
                return goToDiscount;
            }
        }

        private ICommand shop = null;
        public ICommand Shop
        {
            get
            {
                if (shop == null)
                {
                    shop = new RelayCommand(
                        arg =>
                        {
                            NavigationVM.CurrentViewModel = new ShopViewModel(model);
                        },
                        arg => true
                        );
                }
                return shop;
            }
        }
        #endregion

        public void SetActualDiscounts()
        {
            if (ShowEnded)
            {
                actualDiscounts = new ObservableCollection<Discount>();
                foreach (var disc in model.Discounts)
                    actualDiscounts.Add(disc);
            }
            else
                actualDiscounts = model.GetActualDiscounts();
        }
        public void SetActualDiscountsBySearch()
        {
            ActualDiscounts = new ObservableCollection<DiscountInfo>();

            if (string.IsNullOrEmpty(Search))
                foreach (var adiscount in actualDiscounts)
                    ActualDiscounts.Add(new DiscountInfo(adiscount));
            else
                foreach (var adiscount in actualDiscounts.Where(d => d.Name.Contains(Search)))
                    ActualDiscounts.Add(new DiscountInfo(adiscount));

            onPropertyChanged(nameof(ActualDiscounts));
        }
    }
}
