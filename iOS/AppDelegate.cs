using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace rodriguez.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			//adding Iconize package
			Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

			global::Xamarin.Forms.Forms.Init();

			// Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
#endif

			//Load Iconize
			FormsPlugin.Iconize.iOS.IconControls.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
