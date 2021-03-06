using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Model
{
    using DAL.Entities;
    using Spicy.Services;
    using DAL.Repositories;
    using System.Collections.ObjectModel;
    class Model
    {
        // state of data base
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
        public ObservableCollection<Discount> Discounts { get; set; } = new ObservableCollection<Discount>();
        public ObservableCollection<Rating> Ratings { get; set; } = new ObservableCollection<Rating>();
        public ObservableCollection<Shop> Shops { get; set; } = new ObservableCollection<Shop>();

        private AccountManager accManager = AccountManager.Instance;
        public Model()
        {
           // get data from repository
           var categories = CategoryRepo.GetCategories();
            foreach (var category in categories)
                Categories.Add(category);

            var comments = CommentRepo.GetComments();
            foreach (var comment in comments)
                Comments.Add(comment);

            var discounts = DiscountRepo.GetDiscounts();
            foreach (var discount in discounts)
                Discounts.Add(discount);

            var ratings = RatingRepo.GetRating();
            foreach (var rating in ratings)
                Ratings.Add(rating);

            var shops = ShopRepo.GetShops();
            foreach (var shop in shops)
                Shops.Add(shop);

        }

        #region Users
        public User GetOwnerOfComment(Comment comment)
        {
            return accManager.GetUserById(comment.Id_user);
        }

        public User GetOwnerOfDiscount(Discount discount)
        {
            User owner = null;
            if (discount != null)
                owner = accManager.GetUserById(discount.Id_user);
            return owner;
        }

        #endregion

        #region Discounts

        public bool AddDiscount(Discount discount, Category category, Shop shop, User user)
        {
            if (!DiscountExists(discount))
            {
                discount.Id_category = category.Id;
                discount.Id_user = user.Id;
                discount.Id_shop = shop.Id;
                if(!DiscountRepo.AddDiscount(discount)) return false;
                Discounts.Add(discount);

                return true;
            }
            return false;
        }
        public bool UpdateDiscount(Discount discount, Category category, Shop shop)
        {
            var oldDiscount = Discounts.FirstOrDefault(d => d.Id == discount.Id);
            if (oldDiscount != null)
            {
                discount.Id_category = category.Id;
                discount.Id_shop = shop.Id;
                if(!DiscountRepo.UpdateDiscount(discount, discount.Id)) return false;
                oldDiscount.Name = discount.Name;
                oldDiscount.Id_category = discount.Id_category;
                oldDiscount.Image = discount.Image;
                oldDiscount.Link = discount.Link;
                oldDiscount.PreviousPrice = discount.PreviousPrice;
                oldDiscount.Start_Date = discount.Start_Date;
                oldDiscount.End_Date = discount.End_Date;
                oldDiscount.Description = discount.Description;
                oldDiscount.CurrentPrice = discount.CurrentPrice;
                oldDiscount.Code = discount.Code;

                return true;
            }
            return false;
        }

        public bool DiscountExists(Discount discount) => Discounts.Contains(discount);

        public Discount GetDiscountById(uint id)
        {
            return Discounts.FirstOrDefault(d => d.Id == id);
        }

        public ObservableCollection<Discount> GetActualDiscounts()
        {
            var actualDiscounts = new ObservableCollection<Discount>();
            var dtNow = DateTime.Now;

            foreach (var disc in Discounts)
            {
                if (disc.End_Date > dtNow)
                    actualDiscounts.Add(disc);
            }

            return actualDiscounts;
        }

        #endregion

        #region Shops
        public bool AddShop(Shop shop)
        {
            if (!ShopExists(shop))
            {
                if(!ShopRepo.AddShop(shop)) return false;
                Shops.Add(shop);
                return true;
            }
            return false;
        }

        public bool ShopExists(Shop shop) => Shops.Contains(shop);

        public Shop GetShopOfDiscount(Discount discount)
        {
            return Shops.FirstOrDefault(s => s.Id == discount.Id_shop);
        }

        #endregion

        #region Rates

        public bool AddRate(Rating rate)
        {
            if (!RateExists(rate))
            {
                if(!RatingRepo.AddRating(rate)) return false;
                Ratings.Add(rate);
                return true;
            }
            return false;
        }

        public bool UpdateRate(Rating rate)
        {
            if (RateExists(rate))
            {
                if(!RatingRepo.UpdateRating(rate, rate.Id_discount, rate.Id_user)) return false;
                Ratings.FirstOrDefault(r => r.Id_discount == rate.Id_discount && r.Id_user == rate.Id_user).Rate = rate.Rate;
                return true;
            }
            return false;
        }

        public bool RateExists(Rating rate) => Ratings.Contains(rate);

        public Rating GetUserRateOfDiscount(User user, Discount discount)
        {
            return Ratings.FirstOrDefault(r => r.Id_user == user.Id && r.Id_discount == discount.Id);
        }
        public long GetOverallRateOfDiscount(Discount discount)
        {
            long negativeRates = Ratings.Where(r => r.Rate == Rate.negative && r.Id_discount == discount.Id).Count();
            long positiveRates = Ratings.Where(r => r.Rate == Rate.positive && r.Id_discount == discount.Id).Count();
            return (-1 * negativeRates) + positiveRates;
        }

        #endregion

        #region Comments

        public bool AddComment(Comment comment)
        {
            CommentRepo.AddComment(comment);
            Comments.Add(comment);
            return true;
        }


        public ObservableCollection<Comment> GetCommentsOfDiscount(Discount discount)
        {
            var comments = new ObservableCollection<Comment>();

            foreach (var comm in Comments)
            {
                if (comm.Id_discount == discount.Id)
                    comments.Add(comm);
            }

            return comments;
        }
        #endregion

        #region Categories

        public Category GetCategoryOfDiscount(Discount discount)
        {
            return Categories.FirstOrDefault(i => i.Id == discount.Id_category);
        }

        #endregion

    }
}
