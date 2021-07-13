using Spicy.DAL.Entities;
using Spicy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Model
{
    class DiscountInfo
    {
        public uint DiscountId { get; private set; }
        public string Name { get; private set; }
        public byte[] Image { get; private set; }
        private float? currentPrice;
        public string CurrentPrice
        {
            get => currentPrice == null ? "" : currentPrice + " " + Consts.MONEY_UNIT;
        }
        private float? previousPrice;
        public string PreviousPrice
        {
            get => previousPrice == null ? "" : previousPrice + " " + Consts.MONEY_UNIT;
        }
        private DateTime end_Date;
        public string End_Date { get => Consts.DISCOUNT_ONLY_TO + " " + end_Date.Year + "-" + end_Date.Month + "-" + end_Date.Day; }
        public DiscountInfo(Discount discount)
        {
            DiscountId = discount.Id;
            Name = discount.Name;
            Image = discount.Image;
            currentPrice = discount.CurrentPrice;
            previousPrice = discount.PreviousPrice;
            end_Date = discount.End_Date;
        }



    }
}
