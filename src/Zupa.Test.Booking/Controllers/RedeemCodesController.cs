using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.ViewModels;

namespace Zupa.Test.Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedeemCodesController : Controller
    {
        private readonly IBasketsRepository _basketsRepository;
        private readonly IRedeemCodesRepository _redeemCodesRepository;

        public RedeemCodesController(IBasketsRepository basketsRepository, IRedeemCodesRepository redeemCodesRepository)
        {
            _basketsRepository = basketsRepository;
            _redeemCodesRepository = redeemCodesRepository;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Basket>> AddToBasket([FromBody]string redeemCode)
        {
            var basket = await _basketsRepository.ReadAsync();
            if (redeemCode != null)
            {
                var redeem = await _redeemCodesRepository.ReadAsync(redeemCode);

                if (redeem.Used)
                {
                    throw new Exception("Redeem Code Already used: " + redeemCode);
                }

                if (basket.RedeemCode == null)
                {
                    basket.RedeemCode = redeem;
                }
                else
                {
                    await _redeemCodesRepository.ReUseRedeenCode(basket.RedeemCode.id);

                    await _redeemCodesRepository.UseRedeenCode(redeemCode);

                    basket.RedeemCode = redeem;

                }
            }
            basket = await _basketsRepository.ReadAsync();
            return basket.ToBasketViewModel();
        }
    }
}