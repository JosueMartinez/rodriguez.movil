﻿using System;
using Xamarin.Forms;

namespace rodriguez.UI
{

    public class WaitingPage : ContentPage
    {
        public static readonly BindableProperty IsWaitingProperty = BindableProperty.Create("IsWaiting", typeof(bool), typeof(WaitingPage), false);
        public static readonly BindableProperty ShowLoadingFrameProperty = BindableProperty.Create("ShowLoadingFrame", typeof(bool), typeof(WaitingPage), false);
        public static readonly BindableProperty ShowLoadingMessageProperty = BindableProperty.Create("ShowLoadingMessage", typeof(bool), typeof(WaitingPage), false);
        public static readonly BindableProperty ShadeBackgroundProperty = BindableProperty.Create("ShadeBackground", typeof(bool), typeof(WaitingPage), false);
        public static readonly BindableProperty LoadingMessageProperty = BindableProperty.Create("LoadingMessage", typeof(string), typeof(WaitingPage), "Loading...");
        public static readonly BindableProperty WaitingOrientationProperty = BindableProperty.Create("WaitingOrientation", typeof(StackOrientation), typeof(WaitingPage), StackOrientation.Vertical);

        public bool IsWaiting
        {
            get
            {
                return (bool)GetValue(IsWaitingProperty);
            }
            set
            {
                if (value)
                {
                    ShowIndicator();
                }
                else
                {
                    HideIndicator();
                }
            }
        }

        public string LoadingMessage
        {
            get
            {
                return (string)GetValue(LoadingMessageProperty);
            }
            set
            {
                SetValue(LoadingMessageProperty, value);
            }
        }

        public bool ShowLoadingMessage
        {
            get
            {
                return (bool)GetValue(ShowLoadingMessageProperty);
            }
            set
            {
                SetValue(ShowLoadingMessageProperty, value);
            }
        }

        public bool ShowLoadingFrame
        {
            get
            {
                return (bool)GetValue(ShowLoadingFrameProperty);
            }
            set
            {
                SetValue(ShowLoadingFrameProperty, value);
            }
        }

        public bool ShadeBackground
        {
            get
            {
                return (bool)GetValue(ShadeBackgroundProperty);
            }
            set
            {
                SetValue(ShadeBackgroundProperty, value);
            }
        }

        public StackOrientation WaitingOrientation
        {
            get
            {
                return (StackOrientation)GetValue(WaitingOrientationProperty);
            }
            set
            {
                SetValue(WaitingOrientationProperty, value);
            }
        }

        public new View Content
        {
            set
            {
                WaitingPageContent.Content = value;
            }
        }

        public ActivityIndicator Indicator { get; set; }

        private ContentView WaitingPageContent;
        private Grid ContentLayout;
        private Frame FrameLayout;
        private Color ShadedBackgroundColor = Color.Black.MultiplyAlpha(0.2);
        private Color TransparentBackgroundColor = Color.Transparent;
        private Color WhiteBackgroundColor = Color.FromRgba(255, 255, 255, 1.0);    // Background color of White, Opaque is required for Android

        public WaitingPage()
        {
            WaitingPageContent = new ContentView
            {
                Content = null,
            };

            ContentLayout = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 0, 0, 0),
                BackgroundColor = ShadeBackground ? ShadedBackgroundColor : TransparentBackgroundColor,
            };

            ContentLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            ContentLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            base.Content = ContentLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Indicator == null)
            {
                Indicator = new ActivityIndicator
                {
                    Color = Color.Black,
                    Scale = 1.5,
                    IsEnabled = true,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                };
            }

            if (IsWaiting)
            {
                ShowIndicator();
            }

            BuildIndicatorFrame();

            ContentLayout.Children.Add(WaitingPageContent, 0, 0);
            ContentLayout.Children.Add(FrameLayout, 0, 0);
        }

        private void BuildIndicatorFrame()
        {
            var loadingLabel = new Label
            {
                TextColor = Color.Black,
                Text = LoadingMessage,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };

            var stack = new StackLayout
            {
                Spacing = 15,
                Orientation = WaitingOrientation,
                Children =
                {
                    Indicator
                }
            };

            switch (WaitingOrientation)
            {
                case StackOrientation.Vertical:
                    if (ShowLoadingMessage)
                    {
                        stack.Children.Insert(0, loadingLabel);
                    }
                    break;
                case StackOrientation.Horizontal:
                    if (ShowLoadingMessage)
                    {
                        stack.Children.Add(loadingLabel);
                    }
                    break;
            }

            FrameLayout = new Frame
            {
                BackgroundColor = ShowLoadingFrame ? WhiteBackgroundColor : TransparentBackgroundColor,
                OutlineColor = ShowLoadingFrame ? Color.Black : TransparentBackgroundColor,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                IsVisible = IsWaiting,
                HasShadow = false,
                Content = stack,
            };
        }

        private void ShowIndicator()
        {
            if (Indicator != null)
            {
                Indicator.IsRunning = true;
            }

            if (FrameLayout != null)
            {
                FrameLayout.IsVisible = true;
            }

            ContentLayout.BackgroundColor = ShadeBackground ? ShadedBackgroundColor : TransparentBackgroundColor;
        }

        private void HideIndicator()
        {
            if (Indicator != null)
            {
                Indicator.IsRunning = false;
            }

            if (FrameLayout != null)
            {
                FrameLayout.IsVisible = false;
            }

            ContentLayout.BackgroundColor = TransparentBackgroundColor;
        }
    }
}


