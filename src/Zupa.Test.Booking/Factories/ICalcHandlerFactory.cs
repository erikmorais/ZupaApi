using Zupa.Test.Booking.Services.CalServicesChain;

namespace Zupa.Test.Booking.Factories
{
    public interface ICalcHandlerFactory
    {
        CalcHandlerBase Create();
    }
}