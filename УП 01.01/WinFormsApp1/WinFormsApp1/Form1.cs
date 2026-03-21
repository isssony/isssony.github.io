using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonCalc.Enabled = false;
        }

        public void buttonCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double x, eps;

                // Проверка ввода
                if (!double.TryParse(textBoxX.Text, out x) ||
                    !double.TryParse(textBoxEps.Text, out eps))
                {
                    MessageBox.Show("Введите корректные значения!");
                    return;
                }
                if (x <= -1 || x >= 1)
                {
                    MessageBox.Show("X должен быть в диапазоне (-1; 1)");
                    return;
                }
                if (eps <= 0 || eps >= 1)
                {
                    MessageBox.Show("Eps должен быть в диапазоне (0; 1)");
                    return;
                }

                // Левая часть (готовая функция)
                double funcValue = Math.Log(1 - x);

                // Правая часть (ряд)
                double sum = 0;
                double term = -x;   // первый член: -x
                double prevTerm;
                int n = 1;

                do
                {
                    sum += term;

                    prevTerm = term;

                    // Вычисляем следующий член через предыдущий
                    term = prevTerm * x * n / (n + 1);

                    n++;

                    // Искусственное исключение для демонстрации отладчика
                    if (n > 100)
                    {
                        throw new MyCustomException("Ряд не сходится за 100 итераций. Проверьте точность или значение x.");
                    }

                } while (Math.Abs(term - prevTerm) >= eps);

                // Вывод результатов
                labelFunc.Text = "ln(1 - x) = " + funcValue.ToString("F10");
                labelSum.Text = "Сумма ряда = " + sum.ToString("F10");
                labelN.Text = "Количество членов = " + n;
            }
            catch (MyCustomException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Исключение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Общая ошибка: {ex.Message}", "Исключение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxX_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем: цифры, Backspace, запятая, точка, минус
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != (char)Keys.Back &&
                e.KeyChar != ',' &&
                e.KeyChar != '.' &&
                e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Только один минус в начале
            TextBox tb = sender as TextBox;
            if (e.KeyChar == '-' && (tb.SelectionStart != 0 || tb.Text.Contains("-")))
            {
                e.Handled = true;
            }

            // Только одна запятая/точка
            if ((e.KeyChar == ',' || e.KeyChar == '.') &&
                (tb.Text.Contains(",") || tb.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private void textBoxEps_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Для eps не нужен минус
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != (char)Keys.Back &&
                e.KeyChar != ',' &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }

            TextBox tb = sender as TextBox;

            // Только одна запятая/точка
            if ((e.KeyChar == ',' || e.KeyChar == '.') &&
                (tb.Text.Contains(",") || tb.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private void CheckInputs()
        {
            buttonCalc.Enabled =
                !string.IsNullOrWhiteSpace(textBoxX.Text) &&
                !string.IsNullOrWhiteSpace(textBoxEps.Text);
        }

        private void textBoxX_TextChanged(object sender, EventArgs e)
        {
            CheckInputs();
        }

        private void textBoxEps_TextChanged(object sender, EventArgs e)
        {
            CheckInputs();
        }
    }

    // Пользовательское исключение для демонстрации
    public class MyCustomException : Exception
    {
        public MyCustomException(string message) : base(message) { }
    }
}