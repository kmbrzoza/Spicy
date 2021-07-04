using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Spicy.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        //public static readonly RoutedEvent DblClickedEvent =
        //EventManager.RegisterRoutedEvent("DblClicked",
        //     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
        //     typeof(HomeView));

        //public event RoutedEventHandler DblClicked
        //{
        //    add { AddHandler(DblClickedEvent, value); }
        //    remove { RemoveHandler(DblClickedEvent, value); }
        //}

        //void RaiseDblClicked()
        //{
        //    RoutedEventArgs newEventArgs =
        //            new RoutedEventArgs(HomeView.DblClickedEvent);
        //    RaiseEvent(newEventArgs);
        //}

        //private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    RaiseDblClicked();
        //}
    }
}
