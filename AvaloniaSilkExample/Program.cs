using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace AvaloniaSilkExample
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                // setting moved to https://github.com/AvaloniaUI/Avalonia/commit/29d3c7670be216f704f001ee6e28efc0adbe2e83#diff-2b27b9ef6c0ae41c77f35ca59abc2d20472b00e6528e5c3cd2c8f0ba57f383e7R22
		        .With(new Win32PlatformOptions { RenderingMode = new [] { Win32RenderingMode.Wgl }})
                .LogToTrace();
    }
}
