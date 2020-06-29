using System.Linq;
using FormsPlugin.Iconize;
using rodriguez.Classes;
using rodriguez.UI;
using Xamarin.Forms;
using Xamarin.Forms.OAuth;

namespace rodriguez
{
    public partial class App : Application
    {
        public IconTabbedPage tabbedPage { get; set; }
        static ILoginManager loginManager;
        public static App Current;

        public App()
        {
            Current = this;

            InitializeComponent();

            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") && (bool)Properties["IsLoggedIn"];
            var cliente = Properties.ContainsKey("cliente") ? Properties["cliente"] : null;

            // we remember if they're logged in, and only display the login page if they're not
            if (isLoggedIn && cliente != null)
            {
                tabbedPage = new IconTabbedPage
                {
                    Title = "Supermercado Rodriguez",
                    BarTextColor = Color.White,
                    BarBackgroundColor = Color.Red
                };


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

                MainPage = new IconNavigationPage(tabbedPage)
                {
                    BarTextColor = Color.White,
                    BarBackgroundColor = Color.Red
                };
            }
            else
                MainPage = new Login();
        }

        public void ShowMainPage()
        {
            tabbedPage = new IconTabbedPage
            {
                Title = "Supermercado Rodriguez",
                BarTextColor = Color.White,
                BarBackgroundColor = Color.Red
            };


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

            MainPage = new IconNavigationPage(tabbedPage)
            {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.Red
            };
        }

        public void Logout()
        {
            Properties["IsLoggedIn"] = false; // only gets set to 'true' on the LoginPage
            MainPage = new Login();
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
