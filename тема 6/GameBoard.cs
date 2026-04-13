using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ZoologicalLotto
{
    public class GameBoard : Panel
    {
        private PictureBox[,] cells;
        private Label[,] nameLabels;
        private int gridSize;
        private int cellSize = 140;
        private List<Animal> currentAnimals;
        private List<Animal> shuffledAnimals;
        private int matchedCount = 0;
        private int placedCount = 0;

        public event EventHandler GameCompleted;
        public event EventHandler<Animal> HintRequested;

        public int MatchedCount => matchedCount;
        public int TotalCount => placedCount;

        public GameBoard(int size)
        {
            gridSize = size;
            cells = new PictureBox[gridSize, gridSize];
            nameLabels = new Label[gridSize, gridSize];
            currentAnimals = new List<Animal>();
            shuffledAnimals = new List<Animal>();

            this.Size = new Size(gridSize * cellSize + 20, gridSize * cellSize + 20);
            this.BackColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;

            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    int x = j * cellSize + 5;
                    int y = i * cellSize + 5;

                    // Контейнер для ячейки
                    Panel cellPanel = new Panel();
                    cellPanel.Size = new Size(cellSize, cellSize);
                    cellPanel.Location = new Point(x, y);
                    cellPanel.BorderStyle = BorderStyle.FixedSingle;
                    cellPanel.BackColor = Color.White;
                    cellPanel.AllowDrop = true;
                    cellPanel.Tag = new Point(i, j);

                    cellPanel.DragEnter += Cell_DragEnter;
                    cellPanel.DragDrop += Cell_DragDrop;

                    // PictureBox для картинки
                    cells[i, j] = new PictureBox();
                    cells[i, j].Size = new Size(cellSize - 10, cellSize - 40);
                    cells[i, j].Location = new Point(5, 5);
                    cells[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    cells[i, j].BackColor = Color.White;
                    cells[i, j].Image = Properties.Resources.question_mark ?? CreatePlaceholderImage();

                    // Label для названия
                    nameLabels[i, j] = new Label();
                    nameLabels[i, j].Size = new Size(cellSize - 10, 30);
                    nameLabels[i, j].Location = new Point(5, cellSize - 35);
                    nameLabels[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    nameLabels[i, j].Font = new Font("Arial", 8);
                    nameLabels[i, j].BackColor = Color.White;
                    nameLabels[i, j].Text = "?";

                    cellPanel.Controls.Add(cells[i, j]);
                    cellPanel.Controls.Add(nameLabels[i, j]);
                    this.Controls.Add(cellPanel);
                }
            }
        }

        private Image CreatePlaceholderImage()
        {
            Bitmap bmp = new Bitmap(80, 70);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                g.DrawString("?", new Font("Arial", 20), Brushes.Gray, new PointF(35, 25));
            }
            return bmp;
        }

        public void SetAnimals(List<Animal> animals)
        {
            currentAnimals.Clear();
            currentAnimals.AddRange(animals);

            // Создаем перемешанный список для отображения
            shuffledAnimals = new List<Animal>(animals);
            Shuffle(shuffledAnimals);

            // Заполняем поле картинками
            int index = 0;
            placedCount = 0;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (index < shuffledAnimals.Count)
                    {
                        Animal animal = shuffledAnimals[index];
                        cells[i, j].Tag = animal;
                        LoadImage(cells[i, j], animal.ImagePath);
                        nameLabels[i, j].Tag = animal.Name;
                        placedCount++;
                    }
                    else
                    {
                        cells[i, j].Tag = null;
                        cells[i, j].Image = Properties.Resources.question_mark ?? CreatePlaceholderImage();
                        nameLabels[i, j].Tag = null;
                        nameLabels[i, j].Text = "?";
                    }
                    index++;
                }
            }

            matchedCount = 0;
        }

        private void LoadImage(PictureBox pb, string imagePath)
        {
            try
            {
                if (System.IO.File.Exists(imagePath))
                {
                    pb.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pb.Image = CreatePlaceholderImage();
                }
            }
            catch
            {
                pb.Image = CreatePlaceholderImage();
            }
        }

        public void CheckMatch(Animal draggedAnimal, Point cellPosition)
        {
            int i = cellPosition.X;
            int j = cellPosition.Y;

            Animal cellAnimal = cells[i, j].Tag as Animal;

            if (cellAnimal != null && draggedAnimal.Name == cellAnimal.Name)
            {
                // Правильное соответствие
                nameLabels[i, j].Text = draggedAnimal.Name;
                nameLabels[i, j].ForeColor = Color.Green;
                nameLabels[i, j].Font = new Font("Arial", 8, FontStyle.Bold);

                // Убираем возможность повторного перетаскивания
                cells[i, j].Tag = null;
                matchedCount++;

                if (matchedCount == TotalCount)
                {
                    OnGameCompleted();
                }
            }
            else
            {
                // Неправильный ответ - показываем подсказку
                if (cellAnimal != null)
                {
                    ShowHint(cellAnimal);
                }
            }
        }

        private void ShowHint(Animal animal)
        {
            HintRequested?.Invoke(this, animal);
        }

        private void Cell_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Animal)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Cell_DragDrop(object sender, DragEventArgs e)
        {
            Panel cellPanel = sender as Panel;
            if (cellPanel != null && cellPanel.Tag is Point position)
            {
                Animal draggedAnimal = e.Data.GetData(typeof(Animal)) as Animal;
                if (draggedAnimal != null)
                {
                    CheckMatch(draggedAnimal, position);
                }
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public void ResetGame()
        {
            SetAnimals(currentAnimals);
        }

        protected virtual void OnGameCompleted()
        {
            GameCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}