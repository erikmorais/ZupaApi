using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Factories
{
    public interface IBasketItemExpressionFactory
    {
        Expression<Func<BasketItem, bool>> AreEqual(BasketItem other);
    }
}
