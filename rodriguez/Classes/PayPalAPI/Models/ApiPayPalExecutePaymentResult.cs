using System;

namespace rodriguez
{
    public class PayPalExecutePaymentResult
    {
        public string Url { get; set; }
        public string AccessToken { get; set; }

        public string DisplayError { get; set; }
    }
}

