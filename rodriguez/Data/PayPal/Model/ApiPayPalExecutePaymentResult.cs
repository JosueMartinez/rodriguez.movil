using System;

namespace rodriguez.Data.PayPal
{
    public class PayPalExecutePaymentResult
    {
        public string Url { get; set; }
        public string AccessToken { get; set; }

        public string DisplayError { get; set; }
        public int errorCode { get; set; }
    }
}

