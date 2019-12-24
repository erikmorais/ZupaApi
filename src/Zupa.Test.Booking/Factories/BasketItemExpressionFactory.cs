using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Factories
{
    public class BasketItemExpressionFactory : IBasketItemExpressionFactory
    {
        public Expression<Func<BasketItem, bool>> AreEqual(BasketItem other) => i => i.Id == other.Id && i.NetPrice == other.NetPrice && i.TaxRate == other.TaxRate;
    }
}
