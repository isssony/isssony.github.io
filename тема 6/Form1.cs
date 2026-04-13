using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ZoologicalLotto
{
    public partial class Form1 : Form
    {
        private XmlAnimalLoader animalLoader;
        private List<Animal> allAnimals;
        private List<Animal> currentGameAnimals;
        private GameBoard gameBoard;
        private AnimalCardsPanel cardsPanel;
        private ComboBox categoryCombo;
        private ComboBox difficultyCombo;
        private Button startButton;
        private Label statusLabel;
        private Label scoreLabel;
        private Panel hintPanel;
        private Label hintLabel;

        private int currentDifficulty = 0; // 0=easy,1=medium,2=hard
        private int maxUnlockedDifficulty = 0; // start with easy unlocked

        public Form1()
        {
            InitializeComponent();
            InitializeGameComponents();
            LoadAnimals();
        }


        private void InitializeGameComponents()
        {
            // Панель управления
            Panel controlPanel = new Panel();
            controlPanel.Size = new Size(880, 60);
            controlPanel.Location = new Point(10, 10);
            controlPanel.BackColor = Color.LightSteelBlue;

            Label categoryLabel = new Label();
            categoryLabel.Text = "Тема:";
            categoryLabel.Location = new Point(10, 20);
            categoryLabel.Size = new Size(50, 25);

            categoryCombo = new ComboBox();
            categoryCombo.Location = new Point(60, 18);
            categoryCombo.Size = new Size(150, 25);
            categoryCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryCombo.Items.Add("Все");
            categoryCombo.SelectedIndex = 0;

            Label diffLabel = new Label();
            diffLabel.Text = "Сложность:";
            diffLabel.Location = new Point(230, 20);
            diffLabel.Size = new Size(70, 25);

            difficultyCombo = new ComboBox();
            difficultyCombo.Location = new Point(300, 18);
            difficultyCombo.Size = new Size(120, 25);
            difficultyCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            difficultyCombo.Items.Add("Легко (3 картинок)");
            difficultyCombo.Items.Add("Средне (6 картинок)");
            difficultyCombo.Items.Add("Сложно (9 картинок)");
            // Attach handler and set initial to Easy (locked progression will control access)
            difficultyCombo.SelectedIndexChanged += DifficultyCombo_SelectedIndexChanged;
            difficultyCombo.SelectedIndex = 0;
            RefreshDifficultyItems();

            startButton = new Button();
            startButton.Text = "Начать игру";
            startButton.Location = new Point(440, 15);
            startButton.Size = new Size(120, 30);
            startButton.BackColor = Color.LightGreen;
            startButton.Click += StartButton_Click;

            statusLabel = new Label();
            statusLabel.Text = "Готов к игре";
            statusLabel.Location = new Point(580, 20);
            statusLabel.Size = new Size(150, 25);

            scoreLabel = new Label();
            scoreLabel.Text = "Счёт: 0/0";
            scoreLabel.Location = new Point(740, 20);
            scoreLabel.Size = new Size(120, 25);
            scoreLabel.Font = new Font("Arial", 10, FontStyle.Bold);

            controlPanel.Controls.AddRange(new Control[] {
                categoryLabel, categoryCombo, diffLabel, difficultyCombo,
                startButton, statusLabel, scoreLabel
            });

            // Панель подсказок
            hintPanel = new Panel();
            hintPanel.Size = new Size(880, 80);
            hintPanel.Location = new Point(10, 80);
            hintPanel.BackColor = Color.LightYellow;
            hintPanel.BorderStyle = BorderStyle.FixedSingle;

            Label hintTitle = new Label();
            hintTitle.Text = "Подсказка:";
            hintTitle.Location = new Point(10, 10);
            hintTitle.Size = new Size(70, 20);
            hintTitle.Font = new Font("Arial", 9, FontStyle.Bold);

            hintLabel = new Label();
            hintLabel.Text = "Нажмите на название животного или перетащите его на картинку";
            hintLabel.Location = new Point(10, 35);
            hintLabel.Size = new Size(850, 35);
            hintLabel.Font = new Font("Arial", 9);

            hintPanel.Controls.AddRange(new Control[] { hintTitle, hintLabel });

            this.Controls.AddRange(new Control[] { controlPanel, hintPanel });
        }

        private void LoadAnimals()
        {
            string appPath = Application.StartupPath;
            string xmlPath = Path.Combine(appPath, "Data", "animals.xml");
            string imagesPath = Path.Combine(appPath, "Images");

            // Создаем директории если их нет
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            animalLoader = new XmlAnimalLoader(xmlPath, imagesPath);
            allAnimals = animalLoader.LoadAnimals();

            // Заполняем категории
            var categories = allAnimals.Select(a => (a.Category ?? string.Empty).Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct(StringComparer.OrdinalIgnoreCase);
            foreach (var cat in categories)
            {
                categoryCombo.Items.Add(cat);
            }
            categoryCombo.SelectedIndex = 0;

            // Генерируем тестовые изображения
            GenerateSampleImages(imagesPath);
        }

        private void GenerateSampleImages(string imagesPath)
        {
            // Создаем простые изображения-заглушки
            string[] imageNames = {
                // birds
                "orel.jpg", "sova.jpg", "pingvin.jpg", "kolibri.jpg", "sinitsa.jpg",
                "lastochka.jpg", "vorobey.jpg", "golub.jpg", "zhuravl.jpg", "flamingo.jpg", "utka.jpg",
                // mammals
                "lev.jpg", "slon.jpg", "volk.jpg", "tigr.jpg", "medved.jpg", "zhiraf.jpg", "kenguru.jpg", "zayac.jpg", "olen.jpg",
                // reptiles
                "krokodil.jpg", "yasheritsa.jpg", "zmeya.jpg", "iguana.jpg", "cherepakha.jpg", "gadyuka.jpg", "hameleon.jpg", "anaconda.jpg", "agama.jpg",
                // insects
                "babochka.jpg", "muravey.jpg", "strekoza.jpg", "bozhyakorovka.jpg", "pchela.jpg", "komar.jpg", "mol.jpg", "zhuk.jpg", "mukha.jpg"
            };

            foreach (string imgName in imageNames)
            {
                string fullPath = Path.Combine(imagesPath, imgName);
                if (!File.Exists(fullPath))
                {
                    CreateSimpleImage(fullPath, Path.GetFileNameWithoutExtension(imgName));
                }
            }
        }

        private void CreateSimpleImage(string path, string text)
        {
            Bitmap bmp = new Bitmap(140, 140);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightBlue);
                g.DrawRectangle(Pens.Black, 0, 0, 139, 139);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(text, new Font("Arial", 12), Brushes.Black,
                    new RectangleF(0, 0, 140, 140), sf);
            }
            bmp.Save(path);
            bmp.Dispose();
        }

        private void DifficultyCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = difficultyCombo.SelectedIndex;
            if (idx > maxUnlockedDifficulty)
            {
                MessageBox.Show("Этот уровень заблокирован. Пройдите предыдущий уровень, чтобы разблокировать.", "Уровень заблокирован", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // revert selection
                difficultyCombo.SelectedIndexChanged -= DifficultyCombo_SelectedIndexChanged;
                difficultyCombo.SelectedIndex = maxUnlockedDifficulty;
                difficultyCombo.SelectedIndexChanged += DifficultyCombo_SelectedIndexChanged;
                return;
            }
            currentDifficulty = idx;
        }

        private void RefreshDifficultyItems()
        {
            // Update the visible text to show locked/unlocked state
            for (int i = 0; i < difficultyCombo.Items.Count; i++)
            {
                string baseText = i == 0 ? "Легко (3 картинок)" : (i == 1 ? "Средне (6 картинок)" : "Сложно (9 картинок)");
                if (i > maxUnlockedDifficulty)
                    difficultyCombo.Items[i] = baseText + " — заблокирован";
                else
                    difficultyCombo.Items[i] = baseText;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            // Фильтруем животных по категории
            string selectedCategory = categoryCombo.SelectedItem?.ToString() ?? "Все";
            List<Animal> filteredAnimals;

            if (string.Equals(selectedCategory, "Все", StringComparison.OrdinalIgnoreCase))
            {
                filteredAnimals = new List<Animal>(allAnimals);
            }
            else
            {
                filteredAnimals = allAnimals
                    .Where(a => string.Equals((a.Category ?? string.Empty).Trim(), selectedCategory.Trim(), StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Определяем количество нужных животных
            int neededCount = GetNeededAnimalCount();

            if (filteredAnimals.Count < neededCount)
            {
                // Для выбранной категории используем только животных этой категории.
                // Если их меньше, показываем предупреждение и не начинаем игру.
                MessageBox.Show($"Недостаточно животных в выбранной категории. Нужно: {neededCount}, доступно: {filteredAnimals.Count}",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Выбираем случайных животных
            Random rnd = new Random();
            currentGameAnimals = filteredAnimals.OrderBy(x => rnd.Next()).Take(neededCount).ToList();

            // Удаляем старое игровое поле
            if (gameBoard != null)
                this.Controls.Remove(gameBoard);
            if (cardsPanel != null)
                this.Controls.Remove(cardsPanel);

            // Создаем новое игровое поле
            int gridSize = GetGridSize();
            gameBoard = new GameBoard(gridSize);
            gameBoard.Location = new Point(220, 170);
            gameBoard.GameCompleted += GameBoard_GameCompleted;
            gameBoard.HintRequested += GameBoard_HintRequested;

            // Создаем панель с карточками
            cardsPanel = new AnimalCardsPanel();
            cardsPanel.Location = new Point(10, 170);
            cardsPanel.SetAnimals(currentGameAnimals);
            cardsPanel.AnimalSelected += CardsPanel_AnimalSelected;

            gameBoard.SetAnimals(currentGameAnimals);

            this.Controls.Add(gameBoard);
            this.Controls.Add(cardsPanel);

            UpdateScore();
            statusLabel.Text = "Игра начата! Перетаскивайте названия на картинки";
        }

        private int GetNeededAnimalCount()
        {
            switch (currentDifficulty)
            {
                case 0: return 3; // easy
                case 1: return 6; // medium
                default: return 9; // hard
            }
        }

        private int GetGridSize()
        {
            int needed = GetNeededAnimalCount();
            // choose the smallest square grid that can contain needed items
            if (needed <= 4) return 2;
            if (needed <= 9) return 3;
            return 4;
        }

        private void UpdateScore()
        {
            if (gameBoard != null)
            {
                scoreLabel.Text = $"Счёт: {gameBoard.MatchedCount}/{gameBoard.TotalCount}";
            }
        }

        private void GameBoard_GameCompleted(object sender, EventArgs e)
        {
            statusLabel.Text = "Поздравляем! Вы полностью собрали лото!";
            MessageBox.Show("Поздравляем! Вы отлично знаете животных!",
                "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Unlock next difficulty if available
            if (currentDifficulty < 2 && maxUnlockedDifficulty == currentDifficulty)
            {
                maxUnlockedDifficulty = currentDifficulty + 1;
                RefreshDifficultyItems();
                MessageBox.Show("Следующий уровень сложности разблокирован!", "Разблокировано", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GameBoard_HintRequested(object sender, Animal animal)
        {
            hintLabel.Text = $"Подсказка: {animal.Name} - {animal.Description} " +
                $"Среда обитания: {animal.Habitat}";
            hintPanel.BackColor = Color.LightGoldenrodYellow;

            // Подсвечиваем соответствующую кнопку
            if (cardsPanel != null)
            {
                cardsPanel.HighlightAnimal(animal.Name);
            }

            // Сбрасываем цвет панели через 3 секунды
            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += (s, e) => { hintPanel.BackColor = Color.LightYellow; timer.Stop(); };
            timer.Start();
        }

        private void CardsPanel_AnimalSelected(object sender, Animal animal)
        {
            hintLabel.Text = $"Вы выбрали: {animal.Name}. {animal.Description}. " +
                $"Обитает: {animal.Habitat}. Перетащите на нужную картинку!";
        }
    }
}