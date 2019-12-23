using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Data
{
    public class InMemoryRedeemCodesRepository : IRedeemCodesRepository
    {
        public readonly List<RedeemCode> redeemCodes;
        public InMemoryRedeemCodesRepository()
        {
            redeemCodes = new List<RedeemCode> {
            new RedeemCode{ Discount =0.1,Used=false, id="discount10"},
            new RedeemCode{ Discount =0.5,Used=false, id="discount50"}
            };
        }
        public Task<RedeemCode[]> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RedeemCode> ReadAsync(string id)
        {
            return await Task.FromResult(redeemCodes.Where(r => r.id == id).SingleOrDefault());
        }

        public async Task ReUseRedeenCode(string id)
        {
            var redeem = redeemCodes.Where(r => r.id == id).SingleOrDefault();

            if (redeem == null) throw new Exception("Redeem Code not found");

            redeem.Used = false;

            await Task.CompletedTask;
        }

        public async Task UseRedeenCode(string id)
        {
            var redeem = redeemCodes.Where(r => r.id == id).SingleOrDefault();

            if (redeem == null) throw new Exception("Redeem Code not found");
            if (redeem.Used) throw new Exception("Redeem Code Already Used");

            redeem.Used = true;

            await Task.CompletedTask;
        }
    }
}
