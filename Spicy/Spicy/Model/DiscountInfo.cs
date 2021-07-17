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
        private double? currentPrice;
        public string CurrentPrice
        {
            get => currentPrice == null ? "" : currentPrice + " " + Constants.MONEY_UNIT;
        }
        private double? previousPrice;
        public string PreviousPrice
        {
            get => previousPrice == null ? "" : previousPrice + " " + Constants.MONEY_UNIT;
        }
        private DateTime end_Date;
        public string End_Date { get => Constants.DISCOUNT_ONLY_TO + " " + end_Date.Year + "-" + end_Date.Month + "-" + end_Date.Day; }
        private DateTime start_Date;
        public string Start_Date { get => Constants.DISCOUNT_SINCE + " " + start_Date.Year + "-" + start_Date.Month + "-" + start_Date.Day; }
        public string Link { get; private set; }
        private string code;
        public string Code { get => !string.IsNullOrEmpty(code) ? Constants.DISCOUNT_CODE + " " + code : ""; }
        public string Description { get; private set; }
        public DiscountInfo(Discount discount)
        {
            DiscountId = discount.Id;
            Name = discount.Name;
            Image = discount.Image;
            currentPrice = discount.CurrentPrice;
            previousPrice = discount.PreviousPrice;
            end_Date = discount.End_Date;
            start_Date = discount.Start_Date;
            Link = discount.Link;
            Description = discount.Description;
            code = discount.Code;
        }



    }
}
