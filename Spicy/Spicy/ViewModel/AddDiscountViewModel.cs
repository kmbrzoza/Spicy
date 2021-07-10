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
    using DAL.Entities;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Text.RegularExpressions;

    class AddDiscountViewModel : BaseViewModel
    {
        private Model model;
        private Navigation NavigationVM;
        private AccountManager accountManager;

        public AddDiscountViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;
            Shops = model.Shops;
            Categories = model.Categories;
            Since = DateTime.Now;
        }

        #region PRIVATE Components
        private string title;
        private string link;
        private string currentPrice;
        private string previousPrice;
        private ObservableCollection<Shop> shops;
        private ObservableCollection<Category> categories;
        private DateTime since;
        private DateTime to;
        #endregion

        #region PROPS FOR VIEW
        public string Title
        {
            get { return title; }
            set
            {
                if (value.Length <= Consts.MAX_DISCOUNT_TITLE_LENGTH)
                    title = value;
                onPropertyChanged(nameof(Title));

            }
        }
        public string Link
        {
            get { return link; }
            set { link = value; onPropertyChanged(nameof(Link)); }
        }
        public string CurrentPrice
        {
            get { return currentPrice; }
            set
            {
                if (IsStringPrice(value) || string.IsNullOrEmpty(value))
                {
                    currentPrice = value;
                }
                CheckPrices();
                onPropertyChanged(nameof(CurrentPrice));
            }
        }
        public string PreviousPrice
        {
            get { return previousPrice; }
            set
            {
                if (IsStringPrice(value) || string.IsNullOrEmpty(value))
                {
                    previousPrice = value;
                }
                CheckPrices();
                onPropertyChanged(nameof(previousPrice));
            }
        }
        public ObservableCollection<Shop> Shops
        {
            get { return shops; }
            set { shops = value; onPropertyChanged(nameof(Shops)); }
        }
        //public ObservableCollection<Shop> SelectedShop { get; set; }
        public int IndexOfSelectedShop { get; set; }
        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set { categories = value; onPropertyChanged(nameof(Categories)); }
        }
        public int IndexOfSelectedCategory { get; set; }
        public DateTime Since
        {
            get { return since; }
            set
            {
                since = value;
                CheckDates();
                onPropertyChanged(nameof(Since));
            }
        }
        public DateTime To
        {
            get { return to; }
            set
            {
                to = value;
                CheckDates();
                onPropertyChanged(nameof(To));
            }
        }
        public string Code { get; set; }
        public string Description { get; set; }
        #endregion

        #region COMMANDS
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
                            float? currPrice = null;
                            if(!string.IsNullOrEmpty(CurrentPrice))
                                if (float.TryParse(CurrentPrice.Replace(",", "."), out float cp))
                                    currPrice = cp;

                            float? prevPrice = null;
                            if (!string.IsNullOrEmpty(PreviousPrice))
                                if (float.TryParse(PreviousPrice.Replace(",", "."), out float pp))
                                prevPrice = pp;

                            model.AddDiscount(new Discount(Title, Description, currPrice, prevPrice, Link, Code, Since, To),
                                Categories.ElementAt(IndexOfSelectedCategory), Shops.ElementAt(IndexOfSelectedShop));
                            NavigationVM.CurrentViewModel = new HomeViewModel(model);
                        },
                        arg => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Link) && !string.IsNullOrEmpty(Description)
                        );
                }
                return add;
            }
        }

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
        #endregion

        #region METHODS
        public void CheckDates()
        {
            if (To <= Since || To == null)
                To = Since.AddDays(1);
        }

        public bool IsStringPrice(string value)
        {
            Regex regex = new Regex(@"^\d{1,6}([.,]\d{1,2})?$");
            return regex.IsMatch(value);
        }

        public void CheckPrices()
        {
            if (string.IsNullOrEmpty(CurrentPrice) || string.IsNullOrEmpty(PreviousPrice))
                return;
            else
            {
                if (float.Parse(PreviousPrice.Replace(".", ",")) < float.Parse(CurrentPrice.Replace(".", ",")))
                    PreviousPrice = CurrentPrice;
            }
        }
        #endregion
    }
}
