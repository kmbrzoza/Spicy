using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Services
{
    static class Consts
    {
        public const int MAX_USER_NICKNAME_LENGTH = 20;
        public const int MAX_USER_PASSWORD_LENGTH = 30;
        public const int MAX_DISCOUNT_TITLE_LENGTH = 50;
        public const int MAX_COMMENT_LENGTH = 400;
        public const int MAX_SHOP_NAME = 40;
        public const int MAX_SHOP_DESCRIPTION = 400;
        public const string IMAGE_EXTENSIONS = "Image Files(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
        public const string IMAGE_NOT_SELECTED = "Nie wybrano zdjecia";
        public const string MONEY_UNIT = "zł";
        public const string DISCOUNT_CODE = "Kod rabatowy:";
        public const string DISCOUNT_ONLY_TO = "Tylko do:";
        public const string DISCOUNT_EXISTS = "Taka promocja już istnieje!";
        public const string WARNING = "Uwaga!";
    }
}
