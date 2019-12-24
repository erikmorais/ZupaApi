using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Services.CalServicesChain
{
    public abstract class CalcHandlerBase
    {
        protected CalcHandlerBase _successor;

        public abstract Task HandleCalc(Basket basket);

        public void SetSuccessor(CalcHandlerBase successor)
        {
            _successor = successor;
        }
    }
}
