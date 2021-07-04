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

    class DiscountViewModel : BaseViewModel
    {
        private Model model;
        private Navigation NavigationVM;
        private AccountManager accountManager;

        public DiscountViewModel(Model model, Discount discount)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;
            PresentingDiscount = discount;
            CategoryOfDiscount = model.GetCategoryOfDiscount(PresentingDiscount);
            ShopOfDiscount = model.GetShopOfDiscount(PresentingDiscount);
            OwnerOfDiscount = model.GetOwnerOfDiscount(PresentingDiscount);
            CommentsOfDiscounts = model.GetCommentsOfDiscount(PresentingDiscount);
            UserCommentsOfDiscounts = new ObservableCollection<UserComment>();

            foreach (var comment in CommentsOfDiscounts)
            {
                UserCommentsOfDiscounts.Add(new UserComment(accountManager.GetUserById(comment.Id_user).Nickname,
                    comment.CommentText, comment.Date));
            }
            // TODO: DODAĆ KOMENTARZE
        }

        #region PRIVATE COMPONENTS
        private ObservableCollection<Comment> CommentsOfDiscounts { get; set; }
        #endregion

        #region PROP FOR VIEW
        public Discount PresentingDiscount { get; set; }
        public Category CategoryOfDiscount { get; set; }
        public Shop ShopOfDiscount { get; set; }
        public User OwnerOfDiscount { get; set; }
        public ObservableCollection<UserComment> UserCommentsOfDiscounts { get; set; }
        #endregion

        #region COMMANDS
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
        #endregion

    }
}
