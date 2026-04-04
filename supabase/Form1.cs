using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Supabase;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace isssupabase
{
    public partial class Form1 : Form
    {
        private Supabase.Client supabase;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;

            // Привязка кнопок (если дизайнер их не привязал)
            buttonRegister.Click += async (s, e) => await RegisterClicked();
            buttonLogin.Click += async (s, e) => await LoginClicked();

    }

        // Модель таблицы users
        [Table("users")]
        public class UserModel : BaseModel
        {
            [PrimaryKey("id")]
            [Column("id")]
            public string Id { get; set; }

            [Column("pass")]
            public string Pass { get; set; }

            [Column("created_at")]
            public DateTime CreatedAt { get; set; }
        }

        // Инициализация Supabase
        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var url = "https://ailndpxbmmwgebuvlsix.supabase.co"; // твой URL
                var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFpbG5kcHhibW13Z2VidXZsc2l4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzUzMTQxMzAsImV4cCI6MjA5MDg5MDEzMH0.AOEi1JukyXR_7EhYX8JyG8fQ0qurIELghkHXKI_OGFs"; // anon key

                supabase = new Supabase.Client(url, key);
                await supabase.InitializeAsync();

                MessageBox.Show("Supabase подключён!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }

        // Метод регистрации
        private async Task RegisterClicked()
        {
            string username = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            try
            {
                if (supabase == null)
                {
                    MessageBox.Show("Supabase не подключён!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Введите логин и пароль!");
                    return;
                }

                // Проверка существующего пользователя
                var existing = await supabase
                    .From<UserModel>()
                    .Where(x => x.Id == username)
                    .Get();

                if (existing.Models.Count > 0)
                {
                    MessageBox.Show("Пользователь уже существует");
                    return;
                }

                // Создаём пользователя
                // Явно передаём поля в запросе, чтобы не отправлять null для "id"
                await supabase.From<UserModel>().Insert(new[] {
                    new UserModel {
                        Id = username,
                        Pass = password,
                        CreatedAt = DateTime.Now
                    }
                });

                MessageBox.Show($"Привет, {username}! Регистрация успешна.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка регистрации: " + ex.Message);
            }
        }

        // Метод входа
        private async Task LoginClicked()
        {
            string username = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            try
            {
                if (supabase == null)
                {
                    MessageBox.Show("Supabase не подключён!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Введите логин и пароль!");
                    return;
                }

                var result = await supabase
                    .From<UserModel>()
                    .Where(x => x.Id == username)
                    .Get();

                var user = result.Models.FirstOrDefault();

                if (user != null && user.Pass == password)
                {
                    MessageBox.Show($"Привет, {username}!");
                }
                else
                {
                    MessageBox.Show("Неверное имя или пароль");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка входа: " + ex.Message);
            }
        }

    }
}