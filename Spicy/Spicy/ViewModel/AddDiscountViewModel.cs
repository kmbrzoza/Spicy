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
    using System.Windows.Media.Imaging;
    using System.Windows;

    class AddDiscountViewModel : BaseViewModel
    {
        private readonly Model model;
        private readonly Navigation NavigationVM;
        private readonly AccountManager accountManager;

        public AddDiscountViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;
            Shops = model.Shops;
            Categories = model.Categories;
            Since = DateTime.Now;
        }

        public AddDiscountViewModel(Model model, Discount discount) : this(model)
        {
            editingMode = true;
            discountToEdit = discount;
            Title = discountToEdit.Name;
            Link = discountToEdit.Link;
            CurrentPrice = discountToEdit.CurrentPrice.ToString();
            PreviousPrice = discountToEdit.PreviousPrice.ToString();
            Since = discountToEdit.Start_Date;
            To = discountToEdit.End_Date;
            Code = discountToEdit.Code;
            Description = discountToEdit.Description;
            ImageInBytes = discountToEdit.Image;

            IndexOfSelectedShop = Shops.IndexOf(model.GetShopOfDiscount(discountToEdit));
            IndexOfSelectedCategory = Categories.IndexOf(model.GetCategoryOfDiscount(discountToEdit));
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
        private const string imageExtensions = Constants.IMAGE_EXTENSIONS;
        private string imagePath;
        private byte[] imageInBytes;
        private string code;

        private bool editingMode = false;
        private Discount discountToEdit = null;
        #endregion

        #region PROPS FOR VIEW
        public string Title
        {
            get { return title; }
            set
            {
                if (value.Length <= Constants.MAX_DISCOUNT_TITLE_LENGTH)
                    title = value;
                onPropertyChanged(nameof(Title));
            }
        }
        public string Link
        {
            get { return link; }
            set
            {
                if (value.Length <= Constants.MAX_LINK_LENGTH && (ValidationService.IsStringLink(value) || string.IsNullOrEmpty(value) ))
                    link = value;
                onPropertyChanged(nameof(Link));
            }
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
        public string Code
        {
            get { return code; }
            set
            {
                if (value.Length <= Constants.MAX_CODE_LENGTH)
                    code = value;
                onPropertyChanged(nameof(Code));
            }
        }
        public string Description { get; set; }
        public string ImageExtensions { get => imageExtensions; }
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (ImageManager.ImageExist(value))
                {
                    imagePath = value;
                    ImageInBytes = ImageManager.GetBytesFromImagePath(value);
                }
                else
                    imagePath = Constants.IMAGE_NOT_SELECTED;
                onPropertyChanged(nameof(ImagePath));
            }
        }
        public byte[] ImageInBytes
        {
            get { return imageInBytes; }
            set { imageInBytes = value; onPropertyChanged(nameof(ImageInBytes)); }
        }
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
                            double? currPrice = null;
                            if (!string.IsNullOrEmpty(CurrentPrice))
                                if (double.TryParse(CurrentPrice.Replace(".", ","), out double cp))
                                    currPrice = Math.Round(cp, 2);

                            double? prevPrice = null;
                            if (!string.IsNullOrEmpty(PreviousPrice))
                                if (double.TryParse(PreviousPrice.Replace(".", ","), out double pp))
                                    prevPrice = Math.Round(pp, 2);

                            var discount = new Discount(Title, Description, currPrice, prevPrice, Link, Code, Since, To, ImageInBytes);
                            var category = Categories.ElementAt(IndexOfSelectedCategory);
                            var shop = Shops.ElementAt(IndexOfSelectedShop);
                            if (!editingMode)
                            {
                                if (!model.AddDiscount(discount, category, shop, accountManager.CurrentUser))
                                {
                                    MessageBox.Show(Constants.DISCOUNT_EXISTS, Constants.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                discount.Id = discountToEdit.Id;
                                model.UpdateDiscount(discount, category, shop);
                            }
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
