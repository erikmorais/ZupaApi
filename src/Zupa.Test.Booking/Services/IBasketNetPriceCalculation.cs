using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Services
{
    public interface IBasketNetPriceCalculation
    {
       // Task<Basket> CalculateTotals(Basket basket, RedeemCode redeemCode);
        Task<Basket> CalculateTotals(Basket basket);
    }
}
