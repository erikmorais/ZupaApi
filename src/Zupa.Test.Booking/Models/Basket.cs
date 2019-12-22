using System.Collections.Generic;
using System.Linq;

namespace Zupa.Test.Booking.Models
{
    public class Basket
    {
        public IEnumerable<BasketItem> Items { get; set; } = new List<BasketItem>();
        public double total => Items.Sum(a => a.GrossPrice);

    }
}
