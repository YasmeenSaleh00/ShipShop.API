using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public List<CartItemModel> Items { get; set; }
    }
}
