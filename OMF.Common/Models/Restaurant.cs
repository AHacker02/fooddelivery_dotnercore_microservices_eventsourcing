using System;
using System.Collections.Generic;

namespace OMF.Common.Models
{
    public class Restaurant
    {
        public Restaurant()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public string Location { get; set; }
        public string ListedCity { get; set; }
        public decimal ApproxCost { get; set; }
        public List<Menu> Menu { get; set; }
    }

    public class Menu
    {
        public Menu()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
