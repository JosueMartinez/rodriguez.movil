using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Ioc;
using XLabs.Platform.Device;
using rodriguez.Data.PayPal;
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;

namespace rodriguez.Droid
{
    [Activity(Label = "rodriguez.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //adding iconize package
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar, Resource.Id.tabMode);

            #region paypal-conf
            PayPal.Forms.CrossPayPalManager.Init(new PayPalConfiguration(PayPalEnvironment.NoNetwork, "AbpLXvsoTb4Qrd1qQbGl6QsllrYC-QSumRWB3rlM6nbBtx01ngomIDdiyF94lZaz47lVsY7Mt5MveM20")  //"Your PayPal ID from https://developer.paypal.com/developer/applications/"

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
                ShippingAddressOption = ShippingAddressOption.None,

                // OPTIONAL - Language: Default languege for PayPal Plug-In
                Language = "es",

                // OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
                PhoneCountryCode = "809",
            }
            );
            #endregion


            LoadApplication(new App());
        }

    }
}
