using System;

using Xamarin.Forms;

namespace rodriguez
{
	public class Notifications : ContentPage
	{
		public Notifications()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

