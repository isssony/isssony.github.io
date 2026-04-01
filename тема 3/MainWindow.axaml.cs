using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;
using System;

namespace AvaloniaApp;

public partial class MainWindow : Window
{
    private readonly Ellipse _circle = new() { Width = 40, Height = 40 };

    private double _x, _y;
    private double _step = 5;
    private int _direction = 0;
    private double _offset = 30;
    private double _side;

    private DispatcherTimer? _timer;

    private IBrush _color = Brushes.Red;

    public MainWindow()
    {
        InitializeComponent();

        MainCanvas.Children.Add(_circle);
        _circle.Fill = _color;

        this.Opened += (_, __) =>
        {
            UpdateSize();
            StartTimer();
        };

        this.SizeChanged += (_, __) => UpdateSize();
    }

    private void UpdateSize()
    {
        _side = Math.Min(MainCanvas.Bounds.Width, MainCanvas.Bounds.Height) - 2 * _offset;
        _x = _offset;
        _y = _offset;
    }

    private void StartTimer()
    {
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(50)
        };

        _timer.Tick += Move;
        _timer.Start();
    }

    private void Move(object? sender, EventArgs e)
    {
        double max = _offset + _side;

        switch (_direction)
        {
            case 0: _x += _step; if (_x >= max) { _x = max; _direction = 1; } break;
            case 1: _y += _step; if (_y >= max) { _y = max; _direction = 2; } break;
            case 2: _x -= _step; if (_x <= _offset) { _x = _offset; _direction = 3; } break;
            case 3: _y -= _step; if (_y <= _offset) { _y = _offset; _direction = 0; } break;
        }

        Draw();
    }

    private void Draw()
    {
        Canvas.SetLeft(_circle, _x - 20);
        Canvas.SetTop(_circle, _y - 20);
    }

    private async void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.S)
        {
            var settings = new SettingsWindow(_color, _step);
            var result = await settings.ShowDialog<(IBrush, double)>(this);

            _color = result.Item1;
            _step = result.Item2;
            _circle.Fill = _color;
        }
        else
        {
            Close();
        }
    }
}