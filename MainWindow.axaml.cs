using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.IO;

namespace AvaloniaApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSolveABCClick(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = double.Parse(TextBoxA.Text ?? "0");
                double b = double.Parse(TextBoxB.Text ?? "0");
                double c = double.Parse(TextBoxC.Text ?? "0");

                var result = QuadraticSolver.Solve(a, b, c);

                if (result.hasRoots)
                    LabelResult.Text = $"x1 = {result.x1}, x2 = {result.x2}";
                else
                    LabelResult.Text = "Нет действительных корней";
            }
            catch (Exception ex)
            {
                LabelResult.Text = "Ошибка: " + ex.Message;
            }
        }

        private void OnSolveStringClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var parsed = QuadraticSolver.ParseEquation(TextBoxEquation.Text ?? "");

                var result = QuadraticSolver.Solve(parsed.a, parsed.b, parsed.c);

                if (result.hasRoots)
                    LabelResult.Text = $"x1 = {result.x1}, x2 = {result.x2}";
                else
                    LabelResult.Text = "Нет действительных корней";
            }
            catch (Exception ex)
            {
                LabelResult.Text = "Ошибка: " + ex.Message;
            }
        }

        private void OnLoadFileClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var path = "test.txt"; // временно без диалога

                if (!File.Exists(path))
                {
                    LabelResult.Text = "Файл test.txt не найден";
                    return;
                }

                ListBoxResults.Items.Clear();

                var lines = File.ReadAllLines(path);

                foreach (var line in lines)
                {
                    try
                    {
                        var parsed = QuadraticSolver.ParseEquation(line);
                        var res = QuadraticSolver.Solve(parsed.a, parsed.b, parsed.c);

                        if (res.hasRoots)
                            ListBoxResults.Items.Add($"{line} → x1={res.x1}, x2={res.x2}");
                        else
                            ListBoxResults.Items.Add($"{line} → нет корней");
                    }
                    catch
                    {
                        ListBoxResults.Items.Add($"{line} → ошибка");
                    }
                }
            }
            catch (Exception ex)
            {
                LabelResult.Text = ex.Message;
            }
        }
    }
}