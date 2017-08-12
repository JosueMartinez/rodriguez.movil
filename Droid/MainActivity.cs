using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;
using PayPal.Forms;
using FormsPlugin.Iconize.Droid;

namespace rodriguez.Droid
{
    [Activity(Label = "rodriguez.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {


            base.OnCreate(bundle);
            //adding iconize package
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

            global::Xamarin.Forms.Forms.Init(this, bundle);
            IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);
            ToolbarResource = Resource.Layout.toolbar;
            TabLayoutResource = Resource.Layout.tabs;


            #region paypal-conf
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
            #endregion


            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //PayPalManagerImplementation.Manager.Destroy();
        }

    }
}
