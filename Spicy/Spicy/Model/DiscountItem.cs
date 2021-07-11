using Spicy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Model
{
    class DiscountItem
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
        public DiscountItem(uint discountId, string name, byte[] image, float? currPrice, float? prevPrice, DateTime endDate)
        {
            DiscountId = discountId;
            Name = name;
            Image = image;
            currentPrice = currPrice;
            previousPrice = prevPrice;
            end_Date = endDate;
        }



    }
}
