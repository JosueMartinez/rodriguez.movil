using Xamarin.Forms;

namespace rodriguez
{
	public partial class rodriguezPage : TabbedPage
	{
		public rodriguezPage()
		{
			InitializeComponent();
			Children.Add(new BonosView());
			Children.Add(new ComprasView());
			Children.Add(new ConfigView());
		}
	}
}
