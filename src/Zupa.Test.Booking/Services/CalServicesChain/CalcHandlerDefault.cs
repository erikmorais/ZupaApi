using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Services.CalServicesChain
{
    public class CalcHandlerDefault : CalcHandlerBase
    {
        private async Task<Basket> CalculateTotals(Basket basket)
        {
            basket.GrossTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.GrossPrice), 2);
            basket.TaxTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.Quantity * item.TaxRate), 2);
            basket.NetTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.NetPrice), 2);

            return await Task.FromResult(basket);
        }
        public override async Task HandleCalc(Basket basket)
        {
            await CalculateTotals(basket);
        }
    }
}
