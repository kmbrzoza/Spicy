﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Model
{
    using DAL.Entities;
    using Spicy.Services;
    using System.Collections.ObjectModel;
    class Model
    {
        // state of data base
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
        public ObservableCollection<Discount> Discounts { get; set; } = new ObservableCollection<Discount>();
        public ObservableCollection<Has> ShopHasDiscount { get; set; } = new ObservableCollection<Has>();
        public ObservableCollection<IsIn> DiscountIsInCategory { get; set; } = new ObservableCollection<IsIn>();
        public ObservableCollection<Published> UserPublishedDiscounts { get; set; } = new ObservableCollection<Published>();
        public ObservableCollection<Rating> Ratings { get; set; } = new ObservableCollection<Rating>();
        public ObservableCollection<Shop> Shops { get; set; } = new ObservableCollection<Shop>();

        private AccountManager accManager = AccountManager.Instance;
        public Model()
        {
            // get data from repository
            ExampleData();
        }

        #region Users
        public User GetOwnerOfComment(Comment comment)
        {
            return accManager.GetUserById(comment.Id_user);
        }

        // GetOwnerOFDiscount
        public User GetOwnerOfDiscount(Discount discount)
        {
            User owner = null;
            var published = UserPublishedDiscounts.FirstOrDefault(p => p.Id_discount == discount.Id);
            if (published != null)
                owner = accManager.GetUserById(published.Id_user);
            return owner;
        }

        #endregion

        #region Discounts

        public bool AddDiscount(Discount discount)
        {
            // TODO: dodac że dana promke dodaje uzytkownik i do jakiego sklepu i kategorii należy
            if (!DiscountExists(discount))
            {
                // ADD DISCOUNT TO DB
                Discounts.Add(discount);
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
                // ADD RATE TO DB
                Ratings.Add(rate);
                return true;
            }
            return false;
        }

        public bool RateExists(Rating rate) => Ratings.Contains(rate);

        public Rating GetUserRateOfDiscount(User user, Discount discount)
        {
            return Ratings.FirstOrDefault(r => r.Id_user == user.Id && r.Id_discount == discount.Id);
        }

        // GetOverallRateOfDiscount

        #endregion

        #region Comments

        public bool AddComment(Comment comment)
        {
            // ADD COMMENT TO DB
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
            Category cat = null;
            var isin = DiscountIsInCategory.FirstOrDefault(i => i.Id_discount == discount.Id);
            if (isin != null)
                cat = Categories.FirstOrDefault(c => c.Id == isin.Id_category);
            return cat;
        }

        #endregion

        public void ExampleData()
        {
            var discount1 = new Discount("laptopaaaalaptopaaaalaptopaaaalaptopaaaalaptopaaaa", "fajny laptop w fajnej cenie", 120.99f, 150.99f, "www.google.pl", "HELLO", new DateTime(2021, 06, 14), new DateTime(2021, 07, 20)) { Id = 1 };
            var discount2 = new Discount("sluchawki", "koks sluchawy", 20.99f, 50.99f, "www.google.pl", "HELLO2", new DateTime(2021, 06, 14), new DateTime(2021, 07, 20)) { Id = 2 };
            Discounts.Add(discount1);
            Discounts.Add(discount2);

            var shop1 = new Shop("x-kom") { Id = 1 };
            var shop2 = new Shop("Media Expert") { Id = 2 };
            Shops.Add(shop1);
            Shops.Add(shop2);

            Categories.Add(new Category("Laptopy"));
            Categories.Add(new Category("Smartfony"));

            Comments.Add(new Comment("fajny") { Date = DateTime.Now, Id_discount = 2, Id_user = 1 });
            Comments.Add(new Comment("super!\nNaprawde\nPOLECAM!!!") { Date = DateTime.Now, Id_discount = 2, Id_user = 2 });

        }

    }
}
