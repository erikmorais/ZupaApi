using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Services.CalServicesChain
{

    public class CalcHandlerPromoCode : CalcHandlerBase
    {
        private Basket CalculateValues(Basket basket)
        {
            return basket;
        }

        private async Task<Basket> CalculateTotals(Basket basket)
        {
            basket.GrossTotal = Math.Round(basket.Items.Sum(item => item.Quantity * (item.GrossPrice * (1 - basket.RedeemCode.Discount))), 2);
            basket.TaxTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.Quantity * item.TaxRate), 2);
            basket.NetTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.NetPrice), 2);

            return await Task.FromResult(basket);
        }

        public override async Task HandleCalc(Basket basket)
        {
            if (basket.RedeemCode != null)
                await CalculateTotals(basket);
            else if (_successor != null)
                await Task.FromResult(_successor.HandleCalc(basket));
        }
    }
}
