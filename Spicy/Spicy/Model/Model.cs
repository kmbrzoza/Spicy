using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Model
{
    using DAL.Entities;
    using System.Collections.ObjectModel;
    class Model
    {
        // state of data base
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public ObservableCollection<Discount> Discounts { get; set; } = new ObservableCollection<Discount>();
        public ObservableCollection<Shop> Shops { get; set; } = new ObservableCollection<Shop>();
        public ObservableCollection<Rating> Ratings { get; set; } = new ObservableCollection<Rating>();
        public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();


        public Model()
        {
            // get database from repository
        }

    }
}
