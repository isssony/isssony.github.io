using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void buttonCalc_Click(object sender, EventArgs e)
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
                // a(n+1) = a(n) * x * n / (n+1)
                term = prevTerm * x * n / (n + 1);

                n++;

            } while (Math.Abs(term - prevTerm) >= eps);

            // Вывод результатов
            labelFunc.Text = "Math.Log(1 - x) = " + funcValue.ToString("F10");
            labelSum.Text = "Сумма ряда = " + sum.ToString("F10");
            labelN.Text = "Количество членов = " + n;
        }
    }


    }

