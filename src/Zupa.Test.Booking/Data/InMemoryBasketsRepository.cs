using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;
using Zupa.Test.Booking.Services;

namespace Zupa.Test.Booking.Data
{
    internal class InMemoryBasketsRepository : IBasketsRepository
    {
        private Basket _basket;

        public IBasketNetPriceCalculation BasketNetPriceCalculation { get; }

        public InMemoryBasketsRepository(IBasketNetPriceCalculation basketNetPriceCalculation)
        {
            _basket = new Basket();
            BasketNetPriceCalculation = basketNetPriceCalculation;
        }

        public async Task<Basket> ReadAsync()
        {
            _basket = await BasketNetPriceCalculation.CalculateTotals(_basket);
            return await Task.FromResult(_basket);
        }

        public Task ResetBasketAsync()
        {
            return Task.FromResult(_basket = new Basket());
        }

        public async Task<Basket> AddToBasketAsync(BasketItem item)
        {
            var items = _basket.Items.ToList();
            items.Add(item);
            _basket.Items = items;
            _basket = await BasketNetPriceCalculation.CalculateTotals(_basket);
            return await Task.FromResult(_basket);
        }
    }
}
