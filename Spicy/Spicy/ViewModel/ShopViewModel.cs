using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.ViewModel
{
    using Spicy.Model;
    using Spicy.Services;
    using Spicy.DAL.Entities;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Spicy.ViewModel.BaseClass;

    class ShopViewModel : BaseViewModel
    {
        private readonly Navigation NavigationVM;
        private readonly Model model;
        private readonly AccountManager accountManager;
        public ShopViewModel(Model model)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;
            ShopList = new ObservableCollection<Shop>();
            var shops = model.Shops;
            foreach (var shop in shops)
                ShopList.Add(shop);
        }
        public ObservableCollection<Shop> ShopList { get; set; }
        private Shop selectedShop;
        public Shop SelectedShop
        {
            get { return selectedShop; }
            set
            {
                selectedShop = value;
                onPropertyChanged(nameof(SelectedShop));
            }
        }
        private int indexOfSelectedShop;
        public int IndexOfSelectedShop
        {
            get { return indexOfSelectedShop; }
            set
            {
                indexOfSelectedShop = value;
                SelectedShop = ShopList.ElementAt(indexOfSelectedShop);
            }
        }


        private ICommand addNew = null;
        public ICommand AddNew
        {
            get
            {
                if (addNew == null)
                {
                    addNew = new RelayCommand(
                        arg =>
                        {
                            NavigationVM.CurrentViewModel = new AddShopViewModel(model);
                        },
                        arg => true
                        );
                }
                return addNew;
            }
        }

        private ICommand back = null;
        public ICommand Back
        {
            get
            {
                if (back == null)
                {
                    back = new RelayCommand(
                        arg =>
                        {
                            NavigationVM.CurrentViewModel = new HomeViewModel(model);
                        },
                        arg => true
                        );
                }
                return back;
            }
        }
    }
}
