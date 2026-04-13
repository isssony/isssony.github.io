using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ZoologicalLotto
{
    public class AnimalCardsPanel : FlowLayoutPanel
    {
        private List<Animal> animals;
        private List<Button> animalButtons;

        public event EventHandler<Animal> AnimalSelected;

        public AnimalCardsPanel()
        {
            this.FlowDirection = FlowDirection.TopDown;
            this.WrapContents = false;
            this.AutoScroll = true;
            this.Size = new Size(200, 400);
            this.BackColor = Color.Beige;
            this.BorderStyle = BorderStyle.FixedSingle;

            animalButtons = new List<Button>();
        }

        public void SetAnimals(List<Animal> animalsList)
        {
            this.animals = animalsList;
            this.Controls.Clear();
            animalButtons.Clear();

            foreach (Animal animal in animalsList)
            {
                Button btn = new Button();
                btn.Text = animal.Name;
                btn.Size = new Size(180, 40);
                btn.Margin = new Padding(5);
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
                btn.BackColor = Color.LightBlue;
                btn.Tag = animal;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                // No thumbnail images next to names — keep buttons text-only

                btn.MouseDown += AnimalButton_MouseDown;
                btn.Click += AnimalButton_Click;

                this.Controls.Add(btn);
                animalButtons.Add(btn);
            }
        }

        private void AnimalButton_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is Animal animal)
            {
                // Начинаем drag-and-drop
                DoDragDrop(animal, DragDropEffects.Copy);
            }
        }

        private void AnimalButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is Animal animal)
            {
                AnimalSelected?.Invoke(this, animal);
            }
        }

        public void HighlightAnimal(string animalName)
        {
            foreach (Button btn in animalButtons)
            {
                if (btn.Text == animalName)
                {
                    btn.BackColor = Color.Yellow;
                    Timer timer = new Timer();
                    timer.Interval = 1000;
                    timer.Tick += (s, e) => { btn.BackColor = Color.LightBlue; timer.Stop(); };
                    timer.Start();
                    break;
                }
            }
        }
    }
}