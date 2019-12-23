using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RedeemCodeModel>> AddToBasket([FromBody]string redeemCode)
        {
            var basket = await _basketsRepository.ReadAsync();
            if (redeemCode != null)
            {
                var redeem = await _redeemCodesRepository.ReadAsync(redeemCode);


                if (redeem == null)
                {
                    var err = new RedeemCodeModel { Id = "", Discount = 0, Error = "Redeem Code Not Found!: " + redeemCode };
                    return StatusCode(StatusCodes.Status404NotFound, err);
                }

                if (redeem.Used)
                {
                    string error = "";
                    if (basket.RedeemCode != null && basket.RedeemCode.id == redeem.id)
                    {
                        error = "Redeem Code Already Applied " + redeem.id;
                    }
                    else
                    {
                        error = "Redeem Code Already Used: " + redeem.id;
                    }

                    var err = new RedeemCodeModel { Id = "", Discount = 0, Error = error };
                    return StatusCode(StatusCodes.Status404NotFound, err);
                }

                if (basket.RedeemCode == null)
                {
                    await _redeemCodesRepository.UseRedeenCode(redeemCode);
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
            return new RedeemCodeModel { Id = basket.RedeemCode.id, Discount = basket.RedeemCode.Discount, Error = "" };
        }
    }
}