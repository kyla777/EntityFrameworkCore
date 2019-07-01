using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickReach.Ecommerce.API.ViewModel
{
    public class SearchItemViewModel
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
