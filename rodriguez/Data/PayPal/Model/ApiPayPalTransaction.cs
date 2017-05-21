using System;

namespace rodriguez.Data.PayPal
{
    public class PayPalTransaction
    {
        public PayPalAmount amount { get; set; }
        public PayPalItemList item_list { get; set; }
        public string description { get; set; }
    }
}

