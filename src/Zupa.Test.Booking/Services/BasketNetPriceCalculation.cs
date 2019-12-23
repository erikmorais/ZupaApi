using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Services
{
    public class BasketNetPriceCalculation : IBasketNetPriceCalculation
    {
        public BasketNetPriceCalculation(IRedeemCodesRepository redeemCodeRepository)
        {
            RedeemCodeRepository = redeemCodeRepository;
        }

        public IRedeemCodesRepository RedeemCodeRepository { get; }

        //public async Task<Basket> CalculateTotals(Basket basket, RedeemCode redeemCode)
        //{
        //    var redeem = await RedeemCodeRepository.ReadAsync(redeemCode.id);

        //    basket = await this.CalculateTotals(basket);
        //    basket.GrossTotal = basket.GrossTotal * (1 - redeemCode.Discount);
        //    return await Task.FromResult(basket);
        //}

        public async Task<Basket> CalculateTotals(Basket basket)
        {
            basket.NetTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.NetPrice), 2);
            basket.GrossTotal = Math.Round(basket.Items.Sum(item => item.Quantity * item.GrossPrice), 2);

            if(basket.RedeemCode!=null)
            {
                basket.GrossTotal = basket.GrossTotal * (1 - basket.RedeemCode.Discount);

            }
            return await Task.FromResult(basket);
        }
    }
}
