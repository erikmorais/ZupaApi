using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Services
{
    public class CalcEnginePromoCodeService : ICalcEngineService<Basket>
    {

        public async Task<Basket> CalculateTotals(Basket basket)
        {

            if (basket.RedeemCode != null)
            {
                basket.GrossTotal = Math.Round(basket.Items.Sum(item => item.Quantity * (item.GrossPrice * (1 - basket.RedeemCode.Discount))), 2);
            }
            else
            {
                basket.GrossTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.GrossPrice), 2);
            }

            basket.TaxTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.Quantity * item.TaxRate), 2);
            basket.NetTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.NetPrice), 2);

            return await Task.FromResult(basket);
        }
    }
}
