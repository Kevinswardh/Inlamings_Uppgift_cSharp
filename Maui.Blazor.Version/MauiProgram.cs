using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(windows =>
            {
                windows.OnWindowCreated((window) =>
                {
                    var nativeWindow = window.Handler.PlatformView;
                    var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(nativeWindow.GetWindowHandle()));

                    if (appWindow != null)
                    {
                        appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                    }
                });
            });
        });
#endif

        return builder.Build();
    }
}
