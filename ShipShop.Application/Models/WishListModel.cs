using ShipShop.Core.Entities;
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
        public List<WishListItemModel> WishListItems { get; set; } 



    }
}
