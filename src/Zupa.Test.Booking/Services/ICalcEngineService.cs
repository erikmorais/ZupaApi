using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Services
{
    public interface ICalcEngineService<T>
    {
        Task<T> CalculateTotals(T basket);
    }
}
