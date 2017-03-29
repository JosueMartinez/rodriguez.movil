using Xamarin.Forms;

namespace rodriguez
{
	public partial class rodriguezPage : TabbedPage
	{
		public rodriguezPage()
		{
			InitializeComponent();
			//tab colors
			BarBackgroundColor = Color.Red;
			BarTextColor = Color.White;

			Children.Add(new BonosView());
			Children.Add(new ComprasView());
			Children.Add(new ConfigView());
		}
	}
}
