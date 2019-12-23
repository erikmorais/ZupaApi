using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Data
{
    public interface IRedeemCodesRepository
    {
        Task<RedeemCode[]> ReadAllAsync();
        Task<RedeemCode> ReadAsync(string id);
        Task UseRedeenCode(string id);
        Task ReUseRedeenCode(string id);
    }
}
