using System.Linq;
using FormsPlugin.Iconize;
using Xamarin.Forms;

namespace rodriguez
{
	public partial class App : Application
	{
		public IconTabbedPage tabbedPage { get; set; }

		public App()
		{
			InitializeComponent();
			tabbedPage = new IconTabbedPage { Title = "Supermercado Rodriguez", BarTextColor = Color.White, BarBackgroundColor = Color.Red };


			tabbedPage.Children.Add(new BonosView
			{
				Title = "Bonos",
				Icon = "fa-money"
			});

			tabbedPage.Children.Add(new ComprasView
			{
				Title = "Compras",
				Icon = "fa-shopping-cart"
			});

			tabbedPage.Children.Add(new ConfigView
			{
				Title = "Configuración",
				Icon = "fa-cogs"
			});

			MainPage = new IconNavigationPage(tabbedPage) { BarTextColor = Color.White, BarBackgroundColor = Color.Red };

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
