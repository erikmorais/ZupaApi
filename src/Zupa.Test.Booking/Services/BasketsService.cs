using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Services
{
    /// <summary>
    /// Implementing decorator patttern to extend the calculation of shopping cart values
    /// </summary>
    public class BasketsService : IBasketsService
    {
        private readonly IBasketsRepository basketsRepository;
        private readonly ICalcEngineService<Basket> calcEngineService;

        public BasketsService (IBasketsRepository basketsRepository, ICalcEngineService<Basket> calcEngineService)
        {
            this.basketsRepository = basketsRepository;
            this.calcEngineService = calcEngineService;
        }
        public async Task<Basket> AddToBasketAsync(BasketItem item)
        {
            var basket = await basketsRepository.AddToBasketAsync(item);
            return await calcEngineService.CalculateTotals(basket);
        }

        public async Task<Basket> ReadAsync()
        {
            var basket =  await  basketsRepository.ReadAsync();
            return await calcEngineService.CalculateTotals(basket);
        }

        public Task ResetBasketAsync()
        {
            return basketsRepository.ResetBasketAsync();
        }
    }
}
