using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class WishListModel
    {
        public int Id { get; set; }
        public string PrpductName { get; set; }
        public float UnitPrice { get; set; }
        public string ProductStatus { get; set; }

        public string CreatedOn { get; set; }



    }
}
