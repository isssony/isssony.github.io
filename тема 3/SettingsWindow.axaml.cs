using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaApp;

public partial class SettingsWindow : Window
{
    public SettingsWindow(IBrush color, double speed)
    {
        InitializeComponent();

        SpeedSlider.Value = speed;
        ColorBox.SelectedIndex = 0;
    }

    private void Save(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        IBrush brush = Brushes.Red;

        switch ((ColorBox.SelectedItem as ComboBoxItem)?.Content?.ToString())
        {
            case "Blue": brush = Brushes.Blue; break;
            case "Green": brush = Brushes.Green; break;
            case "Black": brush = Brushes.Black; break;
        }

        Close((brush, SpeedSlider.Value));
    }
}