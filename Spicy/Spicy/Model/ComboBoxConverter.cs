using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Spicy.Model
{
    public class ComboBoxConverter : IValueConverter
    {
        private class EmptyItem : DynamicObject
        {
            public string Name { get; set; } = Constants.NOT_SELECTED;
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = null;
                return true;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable container = value as IEnumerable;

            if (container != null)
            {
                IEnumerable<object> genericContainer = container.OfType<object>();
                IEnumerable<object> emptyItem = new object[] { new EmptyItem() };
                return emptyItem.Concat(genericContainer);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is EmptyItem ? null : value;
        }
    }
}
