using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace AvaloniaApp;

class Program
{
    // Точка входа
    public static void Main(string[] args)
        => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

    // Конфигурация Avalonia
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}