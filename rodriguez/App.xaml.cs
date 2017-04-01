using Xamarin.Forms;

namespace rodriguez
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			NavigationPage nav = new NavigationPage(new rodriguezPage())
			{
				BarBackgroundColor = Color.Red,
				BarTextColor = Color.White,

			};
			MainPage = nav;
		}

		protected override void OnStart()
		{

		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
