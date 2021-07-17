using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spicy.Model
{
    static class ValidationService
    {
        public static bool IsStringLink(string link)
        {
            Regex regex = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch(link);
        }
    }
}
