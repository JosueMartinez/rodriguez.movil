using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using UserNotifications;

namespace rodriguez.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			//adding Iconize package
			Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());
			FormsPlugin.Iconize.iOS.IconControls.Init();
			//end Iconize

			//allowing local notifications
			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{
				// Ask the user for permission to get notifications on iOS 10.0+
				UNUserNotificationCenter.Current.RequestAuthorization(
						UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
						(approved, error) => { });
			}
			else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				// Ask the user for permission to get notifications on iOS 8.0+
				var settings = UIUserNotificationSettings.GetSettingsForTypes(
						UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
						new NSSet());

				UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			}
			//end notifications

			global::Xamarin.Forms.Forms.Init();

			// Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
#endif



			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
