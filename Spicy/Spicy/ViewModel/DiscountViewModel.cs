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

    class DiscountViewModel : BaseViewModel
    {
        private readonly Model model;
        private readonly Navigation NavigationVM;
        private readonly AccountManager accountManager;

        public DiscountViewModel(Model model, Discount discount)
        {
            this.model = model;
            NavigationVM = Navigation.Instance;
            accountManager = AccountManager.Instance;
            presentingDiscount = discount;
            CategoryOfDiscount = model.GetCategoryOfDiscount(presentingDiscount);
            ShopOfDiscount = model.GetShopOfDiscount(presentingDiscount);
            OwnerOfDiscount = model.GetOwnerOfDiscount(presentingDiscount);
            LoadRatesOfDiscount();
            UserRate = model.GetUserRateOfDiscount(accountManager.CurrentUser, presentingDiscount);

            CommentsOfDiscounts = model.GetCommentsOfDiscount(presentingDiscount);
            UserCommentsOfDiscounts = new ObservableCollection<UserComment>();

            foreach (var comment in CommentsOfDiscounts)
            {
                UserCommentsOfDiscounts.Add(new UserComment(accountManager.GetUserById(comment.Id_user).Nickname,
                    comment.CommentText, comment.Date));
            }
            // TODO: DODAĆ KOMENTARZE
        }

        #region PRIVATE COMPONENTS
        private Discount presentingDiscount;
        private long rates;
        private ObservableCollection<Comment> CommentsOfDiscounts { get; set; }
        private string newCommentText = "";
        #endregion

        #region PROP FOR VIEW
        public DiscountInfo PresentingDiscount { get => new DiscountInfo(presentingDiscount); }
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
                if (value.Length <= Constants.MAX_COMMENT_LENGTH)
                    newCommentText = value;
                onPropertyChanged(nameof(NewCommentText));
            }
        }

        public string EditDiscountVisibility
        {
            get => OwnerOfDiscount.Id == accountManager.CurrentUser.Id ? "Visible" : "Hidden";
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
                            Comment comment = new Comment(accountManager.CurrentUser.Id, presentingDiscount.Id, newCommentText, DateTime.Now);
                            model.AddComment(comment);
                            UserCommentsOfDiscounts.Add(new UserComment(accountManager.GetUserById(comment.Id_user).Nickname, comment.CommentText, comment.Date));
                            onPropertyChanged(nameof(UserCommentsOfDiscounts));
                            NewCommentText = "";
                        },
                        arg => !string.IsNullOrEmpty(NewCommentText)
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
                                UserRate = new Rating(Rate.positive, accountManager.CurrentUser.Id, presentingDiscount.Id);
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
                                UserRate = new Rating(Rate.negative, accountManager.CurrentUser.Id, presentingDiscount.Id);
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

        private ICommand editDiscount = null;
        public ICommand EditDiscount
        {
            get
            {
                if (editDiscount == null)
                {
                    editDiscount = new RelayCommand(
                        arg =>
                        {
                            NavigationVM.CurrentViewModel = new AddDiscountViewModel(model, presentingDiscount);
                        },
                        arg => OwnerOfDiscount.Id == accountManager.CurrentUser.Id
                        );
                }
                return editDiscount;
            }
        }


        private ICommand gotoDiscount = null;
        public ICommand GotoDiscount
        {
            get
            {
                if (gotoDiscount == null)
                {
                    gotoDiscount = new RelayCommand(
                        arg =>
                        {
                            if(Validation.IsStringLink(presentingDiscount.Link))
                                System.Diagnostics.Process.Start(presentingDiscount.Link);
                        },
                        arg => OwnerOfDiscount.Id == accountManager.CurrentUser.Id
                        );
                }
                return gotoDiscount;
            }
        }
        #endregion

        #region METHODS
        public void LoadRatesOfDiscount()
        {
            Rates = model.GetOverallRateOfDiscount(presentingDiscount);
        }
        #endregion
    }
}
