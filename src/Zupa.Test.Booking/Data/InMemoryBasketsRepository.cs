using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Factories;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Data
{
    internal class InMemoryBasketsRepository : IBasketsRepository
    {
        private Basket _basket;
        private readonly IBasketItemExpressionFactory basketItemExpressionFactory;

        public InMemoryBasketsRepository(IBasketItemExpressionFactory basketItemExpressionFactory)
        {
            _basket = new Basket();
            this.basketItemExpressionFactory = basketItemExpressionFactory;
        }

        public async Task<Basket> ReadAsync()
        {
            return await Task.FromResult(_basket);
        }

        public Task ResetBasketAsync()
        {
            return Task.FromResult(_basket = new Basket());
        }
        /// <summary>
        /// It uses specific pattern to find if the same item have been already inserted to the cart.
        /// It makes flexible for extenting new definitions of the items that are in the same basket are equal. 
        /// For example: Price is a property that make products in the chart of the same product as distinct basket items 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Basket> AddToBasketAsync(BasketItem item)
        {
            var items = _basket.Items.ToList();
            var itemExist = items.AsQueryable().Where(basketItemExpressionFactory.AreEqual(item)).SingleOrDefault();
            if (itemExist == null)
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
