﻿using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;
using Zupa.Test.Booking.Services;

namespace Zupa.Test.Booking.Data
{
    internal class InMemoryBasketsRepository : IBasketsRepository
    {
        private Basket _basket;

        public InMemoryBasketsRepository()
        {
            _basket = new Basket();
        }

        public async Task<Basket> ReadAsync()
        {
            return await Task.FromResult(_basket);
        }

        public Task ResetBasketAsync()
        {
            return Task.FromResult(_basket = new Basket());
        }

        public async Task<Basket> AddToBasketAsync(BasketItem item)
        {
            var items = _basket.Items.ToList();
            var itemExist = items.Where(i => i.Id == item.Id && i.NetPrice == item.NetPrice && i.TaxRate == item.TaxRate).SingleOrDefault() ;
            if (itemExist== null)
            {
                items.Add(item);
            }
            else
            {
                itemExist.Quantity += item.Quantity;
            }
       
            _basket.Items = items;
            return await Task.FromResult(_basket);
        }
    }
}
