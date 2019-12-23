using System;
using System.Linq;

namespace Zupa.Test.Booking.ViewModels
{
    public static class BasketExtensionMethods
    {
        public static Models.Order ToOrderModel(this Basket basket, double discount)
        {
            return new Models.Order
            {
                ID = Guid.NewGuid(),
                GrossTotal = basket.Items.Sum(item => item.GrossPrice*(1- discount)),
                TaxTotal = basket.Items.Sum(item => item.NetPrice * item.TaxRate),
                Items = basket.Items.ToOrderItemModels()
            };
        }

        public static Basket ToBasketViewModel(this Models.Basket basket)
        {
            return new Basket
            {
                Items = basket.Items.ToBasketItemViewModels(),
                GrossTotal = basket.GrossTotal      
            };
        }
    }
}
