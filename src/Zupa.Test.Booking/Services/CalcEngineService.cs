using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.Factories;
using Zupa.Test.Booking.Models;
using Zupa.Test.Booking.Services.CalServicesChain;

namespace Zupa.Test.Booking.Services
{
    /// <summary>
    /// It uses chain of responsability to dedide with calculation engine it will use.
    /// </summary>
    public class CalcEngineService : ICalcEngineService<Basket>
    {
        private readonly ICalcHandlerFactory calcHandlerFactory;
        private readonly CalcHandlerBase calcHandlerBase;

        public CalcEngineService(ICalcHandlerFactory calcHandlerFactory)
        {
            this.calcHandlerFactory = calcHandlerFactory;
            calcHandlerBase = this.calcHandlerFactory.Create();
        }
        public async Task<Basket> CalculateTotals(Basket basket)
        {
            await calcHandlerBase.HandleCalc(basket);
            return await Task.FromResult(basket);
        }
    }
}
