using System;

namespace rodriguez.Data.PayPal
{

    public class PayPalGetTokenResponse
    {

        //		public string Scope { get; set; }
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public string App_Id { get; set; }
        public int Expires_In { get; set; }

        public string DisplayError { get; set; }
        public int errorCode { get; set; }
    }
}

