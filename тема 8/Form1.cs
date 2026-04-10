using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace AnimalGuessGame
{
    public partial class Form1 : Form
    {
        private readonly string _playerLogin;
        private List<string> _traits;
        private List<AnimalRecord> _animals;
        private List<GameResult> _results;
        private GameSettings _settings;

        private List<int> _answers = new List<int>();
        private int _questionIndex = 0;
        private int _questionsAsked = 0;

        private Label lblTitle;
        private Label lblQuestion;
        private Button btnYes;
        private Button btnNo;
        private Button btnGuess;
        private Button btnStartNew;
        private PictureBox pictureBox1;
        private Timer gameTimer;
        private DateTime _sessionStart;

        public Form1(string login)
        {
            _playerLogin = login;
            _traits = DataStore.LoadTraits();
            _animals = DataStore.LoadAnimals();
            _results = DataStore.LoadResults();
            _settings = DataStore.LoadSettings();

            InitializeComponent();
            SetupUI();
            ApplyTheme();
            StartNewGame();
        }

        private void SetupUI()
        {
            Text = $"Игра «Отгадаю животное» — {_playerLogin}";
            Width = 700;
            Height = 500;
            StartPosition = FormStartPosition.CenterScreen;

            MenuStrip menu = new MenuStrip();

            var fileMenu = new ToolStripMenuItem("Файл");
            var miStart = new ToolStripMenuItem("Новая игра");
            var miLoadImage = new ToolStripMenuItem("Загрузить картинку");
            var miSaveResults = new ToolStripMenuItem("Сохранить результаты");
            var miExit = new ToolStripMenuItem("Выход");
            miStart.Click += (s, e) => StartNewGame();
            miLoadImage.Click += (s, e) => ChangeImageFromFile();
            miSaveResults.Click += (s, e) => SaveResults();
            miExit.Click += (s, e) => Close();
            fileMenu.DropDownItems.Add(miStart);
            fileMenu.DropDownItems.Add(miLoadImage);
            fileMenu.DropDownItems.Add(miSaveResults);
            fileMenu.DropDownItems.Add(miExit);

            var settingsMenu = new ToolStripMenuItem("Настройки");
            var miSettings = new ToolStripMenuItem("Параметры игры");
            var miHistory = new ToolStripMenuItem("История результатов");
            miSettings.Click += (s, e) => OpenSettings();
            miHistory.Click += (s, e) => ShowHistory();
            settingsMenu.DropDownItems.Add(miSettings);
            settingsMenu.DropDownItems.Add(miHistory);

            var helpMenu = new ToolStripMenuItem("Справка");
            var miHelp = new ToolStripMenuItem("О программе");
            miHelp.Click += (s, e) => MessageBox.Show("Игра «Отгадаю животное».\nПрограмма задаёт вопросы по признакам и пытается угадать животное.");
            helpMenu.DropDownItems.Add(miHelp);

            menu.Items.Add(fileMenu);
            menu.Items.Add(settingsMenu);
            menu.Items.Add(helpMenu);
            MainMenuStrip = menu;
            Controls.Add(menu);

            lblTitle = new Label { Left = 20, Top = 40, Width = 600, Height = 30, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblQuestion = new Label { Left = 20, Top = 80, Width = 620, Height = 60, Font = new Font("Arial", 11) };

            btnYes = new Button { Left = 20, Top = 150, Width = 120, Text = "Да" };
            btnNo = new Button { Left = 150, Top = 150, Width = 120, Text = "Нет" };
            btnGuess = new Button { Left = 280, Top = 150, Width = 200, Text = "Это моё животное?" };
            btnStartNew = new Button { Left = 490, Top = 150, Width = 150, Text = "Новая игра" };

            btnYes.Click += (s, e) => Answer(true);
            btnNo.Click += (s, e) => Answer(false);
            btnGuess.Click += (s, e) => GuessAnimal();
            btnStartNew.Click += (s, e) => StartNewGame();

            pictureBox1 = new PictureBox
            {
                Left = 20,
                Top = 220,
                Width = 300,
                Height = 200,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            Controls.Add(lblTitle);
            Controls.Add(lblQuestion);
            Controls.Add(btnYes);
            Controls.Add(btnNo);
            Controls.Add(btnGuess);
            Controls.Add(btnStartNew);
            Controls.Add(pictureBox1);

            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
        }

        private void ApplyTheme()
        {
            if (_settings.Theme == ColorTheme.Dark)
            {
                BackColor = Color.FromArgb(40, 40, 40);
                ForeColor = Color.White;
            }
            else if (_settings.Theme == ColorTheme.Blue)
            {
                BackColor = Color.LightSteelBlue;
                ForeColor = Color.DarkBlue;
            }
            else
            {
                BackColor = SystemColors.Control;
                ForeColor = SystemColors.ControlText;
            }
        }

        private void StartNewGame()
        {
            _answers.Clear();
            _questionIndex = 0;
            _questionsAsked = 0;
            _sessionStart = DateTime.Now;
            gameTimer.Start();

            lblTitle.Text = $"Игрок: {_playerLogin}, персонаж: {_settings.CharacterName}";
            if (_traits.Count == 0)
            {
                lblQuestion.Text = "Нет признаков. Добавьте признаки в traits.txt";
                return;
            }

            if (_animals.Count == 0)
            {
                lblQuestion.Text = "Нет животных в базе.";
                return;
            }

            AskCurrentQuestion();
            DrawCharacter();
        }

        private void AskCurrentQuestion()
        {
            if (_questionIndex >= _traits.Count)
            {
                lblQuestion.Text = "Я не смог угадать. Нажмите «Это моё животное?»";
                return;
            }

            lblQuestion.Text = $"У животного есть признак: «{_traits[_questionIndex]}»?";
        }

        private void Answer(bool yes)
        {
            if (_questionIndex >= _traits.Count)
                return;

            _answers.Add(yes ? _questionIndex : -1);
            _questionsAsked++;
            _questionIndex++;

            if (_questionIndex < _traits.Count)
            {
                AskCurrentQuestion();
                return;
            }

            GuessAnimal();
        }

        private void GuessAnimal()
        {
            var yesTraits = Enumerable.Range(0, _questionIndex)
                .Where(i => _answers.Contains(i))
                .ToList();

            AnimalRecord found = _animals.FirstOrDefault(a => yesTraits.All(t => a.Traits.Contains(t)));

            if (found != null)
            {
                var result = MessageBox.Show($"Это {found.Name}?", "Я думаю...", MessageBoxButtons.YesNo);
                bool guessed = result == DialogResult.Yes;

                lblQuestion.Text = guessed ? $"Ура! Я угадал: {found.Name}" : "Тогда введите новое животное.";
                AddResult(guessed, found.Name);

                if (!guessed)
                    LearnNewAnimal(yesTraits);
            }
            else
            {
                lblQuestion.Text = "Я не нашёл подходящее животное. Введите новое.";
                LearnNewAnimal(yesTraits);
            }

            SaveResults();
        }

        private void LearnNewAnimal(List<int> traits)
        {
            string animalName = Prompt.ShowDialog("Введите название животного:", "Новое животное");
            if (string.IsNullOrWhiteSpace(animalName))
                return;

            var animal = new AnimalRecord { Name = animalName };
            animal.Traits.AddRange(traits);

            _animals.Add(animal);
            DataStore.SaveAnimals(_animals);

            lblQuestion.Text = $"Добавлено новое животное: {animalName}";
        }

        private void AddResult(bool guessed, string animalName)
        {
            _results.Add(new GameResult
            {
                PlayerLogin = _playerLogin,
                GameDate = DateTime.Now,
                Guessed = guessed,
                AnimalName = animalName,
                QuestionsAsked = _questionsAsked
            });
        }

        private void SaveResults()
        {
            DataStore.SaveResults(_results);
        }

        private void ShowHistory()
        {
            var list = _results.Where(r => r.PlayerLogin == _playerLogin).ToList();
            if (list.Count == 0)
            {
                MessageBox.Show("У этого игрока пока нет результатов.");
                return;
            }

            string text = "";
            foreach (var r in list)
            {
                text += $"{r.GameDate:g} | {r.AnimalName} | Угадал: {r.Guessed} | Вопросов: {r.QuestionsAsked}\n";
            }

            MessageBox.Show(text, "История результатов");
        }

        private void OpenSettings()
        {
            using (var sf = new SettingsForm(_settings))
            {
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    _settings = sf.Settings;
                    DataStore.SaveSettings(_settings);
                    ApplyTheme();
                    StartNewGame();
                }
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            int elapsed = (int)(DateTime.Now - _sessionStart).TotalMinutes;
            if (elapsed >= _settings.SessionTimeMinutes)
            {
                gameTimer.Stop();
                MessageBox.Show("Время сеанса игры вышло.");
            }
        }

        private void DrawCharacter()
        {
            // If user loaded an image, do not overwrite it
            if (pictureBox1.Image != null)
                return;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (Brush b = new SolidBrush(Color.Orange))
                    g.FillEllipse(b, 50, 30, 180, 120);
                using (Pen p = new Pen(Color.Black, 3))
                    g.DrawEllipse(p, 50, 30, 180, 120);
                g.FillEllipse(Brushes.Black, 90, 70, 20, 20);
                g.FillEllipse(Brushes.Black, 170, 70, 20, 20);
                g.DrawArc(new Pen(Color.Black, 3), 95, 90, 90, 50, 0, 180);
            }
            pictureBox1.Image = bmp;
        }

        private void ChangeImageFromFile()
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
                dlg.Title = "Выберите изображение";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                SetPictureFromFile(dlg.FileName);
            }
        }

        private void SetPictureFromFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("Файл не найден.");
                    return;
                }

                // Dispose old image to release file lock
                var old = pictureBox1.Image;
                Image img;
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    img = Image.FromStream(fs);
                }

                pictureBox1.Image = img;
                old?.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
            }
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent
            };

            Label lbl = new Label() { Left = 20, Top = 20, Width = 340, Text = text };
            TextBox txt = new TextBox() { Left = 20, Top = 50, Width = 340 };
            Button btnOk = new Button() { Text = "OK", Left = 20, Top = 80, Width = 100 };

            string result = "";
            btnOk.Click += (sender, e) => { result = txt.Text; prompt.Close(); };

            prompt.Controls.Add(lbl);
            prompt.Controls.Add(txt);
            prompt.Controls.Add(btnOk);
            prompt.ShowDialog();

            return result;
        }
    }
}