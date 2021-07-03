using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Services
{
    using System.ComponentModel;
    using ViewModel.BaseClass;
    class Navigation
    {
        public event Action CurrentVMChanged;

        private static Navigation instance = null;
        public static Navigation Instance => instance ?? (instance = new Navigation());

        private BaseViewModel currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => currentViewModel;
            set { currentViewModel = value; CurrentVMChanged?.Invoke(); }
        }

        private Navigation()
        {

        }
    }
}
