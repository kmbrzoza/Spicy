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
            LoadRatesOfDiscount();
            UserRate = model.GetUserRateOfDiscount(accountManager.CurrentUser, PresentingDiscount);

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
        private long rates;
        private ObservableCollection<Comment> CommentsOfDiscounts { get; set; }
        private string newCommentText = "";
        #endregion

        #region PROP FOR VIEW
        public Discount PresentingDiscount { get; set; }
        public Category CategoryOfDiscount { get; set; }
        public Shop ShopOfDiscount { get; set; }
        public User OwnerOfDiscount { get; set; }
        public long Rates
        {
            get { return rates; }
            set { rates = value; onPropertyChanged(nameof(Rates)); }
        }
        public Rating UserRate { get; set; }
        public ObservableCollection<UserComment> UserCommentsOfDiscounts { get; set; }
        public string NewCommentText
        {
            get { return newCommentText; }
            set
            {
                if (value.Length <= Consts.MAX_COMMENT_LENGTH)
                    newCommentText = value;
                onPropertyChanged(nameof(NewCommentText));
            }
        }
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

        private ICommand addComment = null;
        public ICommand AddComment
        {
            get
            {
                if (addComment == null)
                {
                    addComment = new RelayCommand(
                        arg =>
                        {
                            Comment comment = new Comment(accountManager.CurrentUser.Id, PresentingDiscount.Id, newCommentText, DateTime.Now);
                            model.AddComment(comment);
                            UserCommentsOfDiscounts.Add(new UserComment(accountManager.GetUserById(comment.Id_user).Nickname, comment.CommentText, comment.Date));
                            NewCommentText = "";
                        },
                        arg => NewCommentText.Length > 0
                        );
                }
                return addComment;
            }
        }

        private ICommand addPositiveRate = null;
        public ICommand AddPositiveRate
        {
            get
            {
                if (addPositiveRate == null)
                {
                    addPositiveRate = new RelayCommand(
                        arg =>
                        {
                            if (UserRate == null)
                            {
                                UserRate = new Rating(Rate.positive, accountManager.CurrentUser.Id, PresentingDiscount.Id);
                                model.AddRate(UserRate);
                            }
                            else
                            {
                                UserRate.Rate = Rate.positive;
                                model.UpdateRate(UserRate);
                            }
                            LoadRatesOfDiscount();
                        },
                        arg => UserRate == null || UserRate.Rate == Rate.negative
                        );
                }
                return addPositiveRate;
            }
        }

        private ICommand addNegativeRate = null;
        public ICommand AddNegativeRate
        {
            get
            {
                if (addNegativeRate == null)
                {
                    addNegativeRate = new RelayCommand(
                        arg =>
                        {
                            if (UserRate == null)
                            {
                                UserRate = new Rating(Rate.negative, accountManager.CurrentUser.Id, PresentingDiscount.Id);
                                model.AddRate(UserRate);
                            }
                            else
                            {
                                UserRate.Rate = Rate.negative;
                                model.UpdateRate(UserRate);
                            }
                            LoadRatesOfDiscount();
                        },
                        arg => UserRate == null || UserRate.Rate == Rate.positive
                        );
                }
                return addNegativeRate;
            }
        }
        #endregion

        #region METHODS
        public void LoadRatesOfDiscount()
        {
            Rates = model.GetOverallRateOfDiscount(PresentingDiscount);
        }
        #endregion
    }
}
