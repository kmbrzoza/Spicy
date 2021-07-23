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

            LoadShopsFilter();
            LoadCategoriesFilter();

            LoadCurrentDiscountsInfoOverall();
        }

        #region PRIVATE Components
        private ObservableCollection<Discount> currentDiscounts;
        #endregion

        #region PROPS FOR VIEW
        public ObservableCollection<DiscountInfo> CurrentDiscountsInfo { get; set; }
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
                LoadCurrentDiscountsInfoOverall();
            }
        }
        public bool Filters
        {
            get { return userSession.Home_Filters; }
            set
            {
                userSession.Home_Filters = value;
                onPropertyChanged(nameof(Filters));
            }
        }

        public ObservableCollection<Category> CategoriesFilter { get; set; }
        public int IndexOfSelectedCategoriesFilter
        {
            get { return userSession.Home_CategoriesFilter; }
            set
            {
                userSession.Home_CategoriesFilter = value;
                onPropertyChanged(nameof(IndexOfSelectedCategoriesFilter));
                LoadCurrentDiscountsInfoOverall();
            }
        }

        public ObservableCollection<Shop> ShopsFilter { get; set; }
        public int IndexOfSelectedShopsFilter
        {
            get { return userSession.Home_ShopsFilter; }
            set
            {
                userSession.Home_ShopsFilter = value;
                onPropertyChanged(nameof(IndexOfSelectedShopsFilter));
                LoadCurrentDiscountsInfoOverall();
            }
        }

        public bool ShowEnded
        {
            get { return userSession.Home_ShowEnded; }
            set
            {
                userSession.Home_ShowEnded = value;
                LoadCurrentDiscountsInfoOverall();
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
                            if (IndexOfSelectedDiscount > -1)
                            {
                                var discount = model.GetDiscountById(CurrentDiscountsInfo.ElementAt(IndexOfSelectedDiscount).DiscountId);
                                if (discount != null)
                                    NavigationVM.CurrentViewModel = new DiscountViewModel(model, discount);
                            }
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

        public void LoadShopsFilter()
        {
            ShopsFilter = new ObservableCollection<Shop>();
            ShopsFilter.Add(null);
            foreach (var shop in model.Shops)
                ShopsFilter.Add(shop);
        }
        public void LoadCategoriesFilter()
        {
            CategoriesFilter = new ObservableCollection<Category>();
            CategoriesFilter.Add(null);
            foreach (var category in model.Categories)
                CategoriesFilter.Add(category);
        }

        public void SetCurrentDiscountsByEndDate()
        {
            if (ShowEnded)
            {
                currentDiscounts = new ObservableCollection<Discount>();
                foreach (var disc in model.Discounts)
                    currentDiscounts.Add(disc);
            }
            else
                currentDiscounts = model.GetActualDiscounts();
        }
        public void SetCurrentDiscountsBySearch()
        {
            if (string.IsNullOrEmpty(Search)) return;

            var searchedDiscounts = currentDiscounts.Where(d => d.Name.ToLower().Contains(Search.ToLower()));
            currentDiscounts = new ObservableCollection<Discount>();
            foreach (var disc in searchedDiscounts)
                currentDiscounts.Add(disc);
        }
        public void SetCurrentDiscountsByCategoriesFilter()
        {
            var selectedCategory = CategoriesFilter.ElementAt(IndexOfSelectedCategoriesFilter);
            if (selectedCategory == null) return;

            var filteredDiscounts = currentDiscounts.Where(d => d.Id_category == selectedCategory.Id);
            currentDiscounts = new ObservableCollection<Discount>();
            foreach (var disc in filteredDiscounts)
                currentDiscounts.Add(disc);
        }

        public void SetCurrentDiscountsByShopsFilter()
        {
            var selectedShop = ShopsFilter.ElementAt(IndexOfSelectedShopsFilter);
            if (selectedShop == null) return;

            var filteredDiscounts = currentDiscounts.Where(d => d.Id_shop == selectedShop.Id);
            currentDiscounts = new ObservableCollection<Discount>();
            foreach (var disc in filteredDiscounts)
                currentDiscounts.Add(disc);
        }

        public void LoadCurrentDiscountsInfo()
        {
            CurrentDiscountsInfo = new ObservableCollection<DiscountInfo>();
            foreach (var currDisc in currentDiscounts)
                CurrentDiscountsInfo.Add(new DiscountInfo(currDisc));
            onPropertyChanged(nameof(CurrentDiscountsInfo));
        }

        public void LoadCurrentDiscountsInfoOverall()
        {
            SetCurrentDiscountsByEndDate();
            SetCurrentDiscountsBySearch();
            SetCurrentDiscountsByCategoriesFilter();
            SetCurrentDiscountsByShopsFilter();
            LoadCurrentDiscountsInfo();
        }
    }
}
