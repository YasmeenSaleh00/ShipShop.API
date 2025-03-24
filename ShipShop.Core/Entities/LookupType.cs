using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class LookupType:MainEntity
    {
        public string Name { get; set; }
        public List<LookupItem> Items { get; set; }

    }
}
