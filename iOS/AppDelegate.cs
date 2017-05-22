using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using UserNotifications;

using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;
using PayPal.Forms;

namespace rodriguez.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //adding Iconize package
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());
            //end Iconize

            global::Xamarin.Forms.Forms.Init();

            CrossPayPalManager.Init(new PayPalConfiguration(
                    PayPal.Forms.Abstractions.Enum.PayPalEnvironment.NoNetwork,
                    "AbpLXvsoTb4Qrd1qQbGl6QsllrYC - QSumRWB3rlM6nbBtx01ngomIDdiyF94lZaz47lVsY7Mt5MveM20"
                    )
            {
                //If you want to accept credit cards
                AcceptCreditCards = true,
                //Your business name
                MerchantName = "Test Store",
                //Your privacy policy Url
                MerchantPrivacyPolicyUri = "https://www.example.com/privacy",
                //Your user agreement Url
                MerchantUserAgreementUri = "https://www.example.com/legal",

                // OPTIONAL - ShippingAddressOption (Both, None, PayPal, Provided)
                ShippingAddressOption = ShippingAddressOption.Both,

                // OPTIONAL - Language: Default languege for PayPal Plug-In
                Language = "es",

                // OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
                PhoneCountryCode = "52",
            }
            );


            FormsPlugin.Iconize.iOS.IconControls.Init();
            // Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif



            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
