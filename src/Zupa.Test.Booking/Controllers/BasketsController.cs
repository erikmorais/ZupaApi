using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.Services;
using Zupa.Test.Booking.ViewModels;

namespace Zupa.Test.Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketsRepository _basketsRepository;
        private readonly IRedeemCodesRepository _redeemCodesRepository;

        public BasketsController(IBasketsService basketsRepository, IRedeemCodesRepository redeemCodesRepository)
        {
            _basketsRepository = basketsRepository;
            _redeemCodesRepository = redeemCodesRepository;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Basket>> AddToBasket([FromBody]BasketItem basketItem)
        {
            var item = basketItem.ToBasketItemModel();
            var basket = await _basketsRepository.AddToBasketAsync(item);

            return basket.ToBasketViewModel();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Basket>> GetBasket(string redeemCode)
        {
            var basket = await _basketsRepository.ReadAsync();
            if (redeemCode != null)
            {
                var redeem = await _redeemCodesRepository.ReadAsync(redeemCode);
                basket.RedeemCode = redeem;
            }

            return basket.ToBasketViewModel();
        }
    }
}