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
        public ObservableCollection<Has> ShopHasDiscount { get; set; } = new ObservableCollection<Has>();
        public ObservableCollection<Rating> Ratings { get; set; } = new ObservableCollection<Rating>();
        public ObservableCollection<Shop> Shops { get; set; } = new ObservableCollection<Shop>();

        private AccountManager accManager = AccountManager.Instance;
        public Model()
        {
            // get data from repository
            //var categories = CategoryRepo.GetCategories();
            //foreach (var category in categories)
            //    Categories.Add(category);

            //var comments = CommentRepo.GetComments();
            //foreach (var comment in comments)
            //    Comments.Add(comment);

            //var discounts = DiscountRepo.GetDiscounts();
            //foreach (var discount in discounts)
            //    Discounts.Add(discount);

            //var hases = HasRepo.GetHas();
            //foreach (var has in hases)
            //    ShopHasDiscount.Add(has);

            //var ratings = RatingRepo.GetRating();
            //foreach (var rating in ratings)
            //    Ratings.Add(rating);

            //var shops = ShopRepo.GetShops();
            //foreach (var shop in shops)
            //    Shops.Add(shop);

            ExampleData();
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
            // TODO: dodac że dana promke dodaje uzytkownik i do jakiego sklepu i kategorii należy
            if (!DiscountExists(discount))
            {
                discount.Id_category = category.Id;
                discount.Id_user = user.Id;
                //DiscountRepo.AddDiscount(discount);
                Discounts.Add(discount);
                //var has = new Has(shop.Id, discount.Id);
                //HasRepo.AddHas(has);
                //ShopHasDiscount.Add(has);
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
                // ADD SHOP TO DB
                //ShopRepo.AddShop(shop);
                Shops.Add(shop);
                return true;
            }
            return false;
        }

        public bool ShopExists(Shop shop) => Shops.Contains(shop);

        public Shop GetShopOfDiscount(Discount discount)
        {
            Shop shop = null;
            var has = ShopHasDiscount.FirstOrDefault(h => h.Id_discount == discount.Id);
            if (has != null)
                shop = Shops.FirstOrDefault(s => s.Id == has.Id_shop);
            return shop;
        }

        #endregion

        #region Rates

        public bool AddRate(Rating rate)
        {
            if (!RateExists(rate))
            {
                //RatingRepo.AddRating(rate);
                Ratings.Add(rate);
                return true;
            }
            return false;
        }

        public bool UpdateRate(Rating rate)
        {
            if (RateExists(rate))
            {
                //TODO: Dodać do bazy
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
            //CommentRepo.AddComment(comment);
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

        public void ExampleData()
        {
            var shop1 = new Shop("x-kom") { Id = 1 };
            var shop2 = new Shop("Media Expert") { Id = 2 };
            Shops.Add(shop1);
            Shops.Add(shop2);

            Categories.Add(new Category("Laptopy"));
            Categories.Add(new Category("Smartfony"));

            var discount1 = new Discount("laptopaaaalaptopaaaalaptopaaaalaptopaaaalaptopaaaa", "fajny laptop w fajnej cenie", 120.99f, 150.99f,
            "www.google.pl", "HELLO", new DateTime(2021, 06, 14), new DateTime(2021, 07, 20), null, 1, 1)
            { Id = 1 };
            var discount2 = new Discount("sluchawki", "koks sluchawy", 20.99f, 50.99f, "www.google.pl", "HELLO2",
                new DateTime(2021, 06, 14), new DateTime(2021, 07, 20), null, 2, 2)
            { Id = 2 };
            Discounts.Add(discount1);
            Discounts.Add(discount2);

            ShopHasDiscount.Add(new Has(1, 1));
            ShopHasDiscount.Add(new Has(2, 2));

            Comments.Add(new Comment("fajny") { Date = DateTime.Now, Id_discount = 2, Id_user = 1 });
            Comments.Add(new Comment("super!\nNaprawde\nPOLECAM!!!") { Date = DateTime.Now, Id_discount = 2, Id_user = 2 });

        }

    }
}
