using System;
namespace rodriguez.Data.PayPal
{
    public class PayPalPayment
    {
        public string Id { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public string State { get; set; }
        public string Intent { get; set; }
        //
        public PayPalPayer Payer { get; set; }

        public string DisplayError { get; set; }
    }
}
