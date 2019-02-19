using System;
using System.Collections.Generic;
namespace rodriguez
{
    public static class Constants
    {
        //public const string baseUrl = "http://smrodriguez.azurewebsites.net/api/";
        public const string baseUrl = "http://10.0.10.3:81/rodriguez.api/api/";


        //PayPal
        // Base Api url
        public const string PayPalApiUrl = "https://api.sandbox.paypal.com/v1/";

        // Client Id
        public const string PayPalClientId = "AbpLXvsoTb4Qrd1qQbGl6QsllrYC-QSumRWB3rlM6nbBtx01ngomIDdiyF94lZaz47lVsY7Mt5MveM20";//"ASpkpVUsuaE73F-3glCVNVZO31eJgWPmv0DFDUoPAozHy9qPX_HKQQ_igzvLHFe7Vz5pvr66VNItscFy";

        // Secret
        public const string PayPalSecret = "EOGvMzgbRC6Bf9YhTwVPQFNNzpHtkKdE_eWlBYMUQhh01CGrvjqYrLTEDZL1w-xt6XNdYfY-Im0qpZ9V";

        public const string ReturnUrl = "http://www.google.com/";
        public const string CancelUrl = "http://www.bing.com/";

        public static string ReturnHost = new Uri(Constants.ReturnUrl).Host;
        public static string CancelHost = new Uri(Constants.CancelUrl).Host;
        //Fin PayPal
    }
}
