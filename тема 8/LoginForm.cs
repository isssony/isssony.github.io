using rabota8;
using System;
using System.Windows.Forms;

namespace AnimalGuessGame
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Введите логин.");
                return;
            }

            Hide();
            var form = new Form1(login);
            form.FormClosed += (s, args) => Close();
            form.Show();
        }
    }
}