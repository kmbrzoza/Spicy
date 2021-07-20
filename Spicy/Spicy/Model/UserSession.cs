using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Model
{
    class UserSession
    {
        private UserSession()
        {
            Home_ShowEnded = false;
            Home_SearchBy = "";
            Home_SelectedIndexOfDiscount = -1;
            Home_Filters = false;
        }
        private static UserSession instance = null;

        public static UserSession Instance { get => instance ?? (instance = new UserSession()); }

        public bool Home_ShowEnded { get; set; }
        public string Home_SearchBy { get; set; }
        public int Home_SelectedIndexOfDiscount { get; set; }
        public bool Home_Filters { get; set; }
        public int Home_CategoriesFilter { get; set; }
        public int Home_ShopsFilter { get; set; }

        public static void NewSession()
        {
            instance = new UserSession();
        }
    }
}
