using System;
using System.Linq;

namespace Zupa.Test.Booking.ViewModels
{
    public static class BasketExtensionMethods
    {
        public static Models.Order ToOrderModel(this Basket basket)
        {
            return new Models.Order
            {
                ID = Guid.NewGuid(),
                GrossTotal = basket.GrossTotal,
                TaxTotal = basket.TaxTotal ,
                NetTotal = basket.NetTotal,
                Items = basket.Items.ToOrderItemModels()
            };
        }

        public static Basket ToBasketViewModel(this Models.Basket basket)
        {
            return new Basket
            {
                Items = basket.Items.ToBasketItemViewModels(),
                GrossTotal = basket.GrossTotal ,
                NetTotal =basket.NetTotal,
                TaxTotal =basket.TaxTotal
            };
        }
    }
}
