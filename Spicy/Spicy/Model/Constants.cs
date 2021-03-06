using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Model
{
    static class Constants
    {
        public const int MAX_USER_NICKNAME_LENGTH = 20;
        public const int MAX_USER_PASSWORD_LENGTH = 30;
        public const int MAX_DISCOUNT_TITLE_LENGTH = 50;
        public const int MAX_DISCOUNT_DESCRIPTION_LENGTH = 5000;
        public const int MAX_COMMENT_LENGTH = 400;
        public const int MAX_SHOP_NAME = 40;
        public const int MAX_SHOP_DESCRIPTION = 1000;
        public const int MAX_CODE_LENGTH = 30;
        public const int MAX_LINK_LENGTH = 2048;

        public const string IMAGE_EXTENSIONS = "Image Files(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
        public const string IMAGE_NOT_SELECTED = "Nie wybrano zdjecia";
        public const string MONEY_UNIT = "zł";
        public const string DISCOUNT_CODE = "Kod rabatowy:";
        public const string DISCOUNT_ONLY_TO = "Tylko do:";
        public const string DISCOUNT_SINCE = "Od:";
        public const string DISCOUNT_EXISTS = "Taka promocja już istnieje!";
        public const string WARNING = "Uwaga!";
        public const string NOT_SELECTED = "<Nie wybrano>";

        public const string USER_NICK_PASSWORD_INCORRECT = "Nazwa użytkownika lub hasło zawierają nieprawidłowe znaki!";
        public const string USER_CONNECTION_ERROR = "Nie można zarejestrować użytkownika. Sprawdź połączenie z bazą!";
    }
}
