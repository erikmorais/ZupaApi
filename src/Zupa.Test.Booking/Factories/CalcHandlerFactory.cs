using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Services.CalServicesChain;

namespace Zupa.Test.Booking.Factories
{
    public class CalcHandlerFactory : ICalcHandlerFactory
    {
        public CalcHandlerBase Create()
        {
            CalcHandlerBase calcHandlerPromo = new CalcHandlerPromoCode();
            CalcHandlerBase calcHandlerDefault = new CalcHandlerDefault();
            calcHandlerPromo.SetSuccessor(calcHandlerDefault);
            return calcHandlerPromo;
        }
    }
}
