using Microsoft.Win32;
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
    /// Interaction logic for FileDialog.xaml
    /// </summary>
    public partial class FileDialog : UserControl
    {
        public FileDialog()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SelectedFileProperty = DependencyProperty.Register(
        "SelectedFile", typeof(string), typeof(FileDialog), new FrameworkPropertyMetadata(null));
        public string SelectedFile
        {
            get { return (string)GetValue(SelectedFileProperty); }
            set { SetValue(SelectedFileProperty, value); }
        }

        public static readonly DependencyProperty ExtentionsProperty = DependencyProperty.Register(
        "Extentions", typeof(string), typeof(FileDialog), new FrameworkPropertyMetadata(null));
        public string Extentions
        {
            get { return (string)GetValue(ExtentionsProperty); }
            set { SetValue(ExtentionsProperty, value); }
        }

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register(
        "ButtonText", typeof(string), typeof(FileDialog), new FrameworkPropertyMetadata(null));
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (!string.IsNullOrEmpty(Extentions))
                ofd.Filter = Extentions;
            bool? result = ofd.ShowDialog();
            if (result == true)
                SelectedFile = ofd.FileName;
        }
    }
}
