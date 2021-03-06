﻿using System.Collections.Generic;

namespace Zupa.Test.Booking.ViewModels
{
    public class Basket
    {
        public IEnumerable<BasketItem> Items { get; set; }
        public double GrossTotal { get; set; }
        public double NetTotal { get; set; }
        public double TaxTotal { get; set; }
        public string RedeemCode { get; set; }
    }
}
