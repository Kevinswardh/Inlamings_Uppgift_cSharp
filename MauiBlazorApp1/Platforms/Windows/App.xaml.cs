using Microsoft.Maui.Hosting;
using Microsoft.UI.Xaml;

namespace MauiBlazorApp1.WinUI
{
    public partial class App : MauiWinUIApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            // Directly remove the title bar after launching the window
            var window = Microsoft.UI.Xaml.Window.Current;
            if (window != null)
            {
                window.SetTitleBar(null); // This removes the title bar
            }

            // Wait for the window to be fully loaded (optional for further UI adjustments)
            Microsoft.Maui.Controls.Application.Current.MainPage.Appearing += OnMainPageAppearing;
        }

        private void OnMainPageAppearing(object sender, EventArgs e)
        {
            // Remove title bar again just in case
            var window = Microsoft.UI.Xaml.Window.Current;
            if (window != null)
            {
                window.SetTitleBar(null); // This ensures title bar is removed
            }

            // Unsubscribe to avoid multiple calls
            Microsoft.Maui.Controls.Application.Current.MainPage.Appearing -= OnMainPageAppearing;
        }
    }
}
