using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

namespace CityApp
{
    public partial class Form1 : Form
    {
        City city = new City();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                District d = new District()
                {
                    Name = txtDistrict.Text,
                    Area = double.Parse(txtArea.Text, CultureInfo.InvariantCulture),
                    Population = int.Parse(txtPopulation.Text)
                };

                city.Name = txtCity.Text;
                city.Districts.Add(d);

                UpdateTable();
            }
            catch
            {
                MessageBox.Show("Ошибка ввода!");
            }
        }

        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();

            foreach (var d in city.Districts)
            {
                dataGridView1.Rows.Add(
                    city.Name,
                    d.Name,
                    d.Area,
                    d.Population,
                    d.Density.ToString("F3")  
                );
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            sfd.DefaultExt = "txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(city.Name ?? "Неизвестный город");
                        sw.WriteLine(city.Districts.Count);

                        foreach (var d in city.Districts)
                        {
                            sw.WriteLine(d.Name ?? "Неизвестный район");
                            sw.WriteLine(d.Area.ToString(CultureInfo.InvariantCulture));
                            sw.WriteLine(d.Population);
                        }
                    }
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(ofd.FileName, System.Text.Encoding.UTF8))
                    {
                        city = new City();
                        city.Name = sr.ReadLine()?.Trim();

                        string countLine = sr.ReadLine()?.Trim();
                        if (string.IsNullOrEmpty(countLine) || !int.TryParse(countLine, out int count) || count < 0)
                        {
                            MessageBox.Show("Ошибка: неверный формат количества районов.\nОжидается целое неотрицательное число.");
                            return;
                        }

                        for (int i = 0; i < count; i++)
                        {
                            string nameLine = sr.ReadLine()?.Trim();
                            string areaLine = sr.ReadLine()?.Trim();
                            string popLine = sr.ReadLine()?.Trim();

                            if (string.IsNullOrEmpty(nameLine) || string.IsNullOrEmpty(areaLine) || string.IsNullOrEmpty(popLine))
                            {
                                MessageBox.Show($"Ошибка: неполные данные для района #{i + 1}");
                                return;
                            }

                            if (!double.TryParse(areaLine, NumberStyles.Float, CultureInfo.InvariantCulture, out double area) ||
                                !int.TryParse(popLine, out int population))
                            {
                                MessageBox.Show($"Ошибка: неверный формат числовых данных для района #{i + 1}\nПлощадь: {areaLine}, Население: {popLine}");
                                return;
                            }

                            District d = new District
                            {
                                Name = nameLine,
                                Area = area,
                                Population = population
                            };
                            city.Districts.Add(d);
                        }
                    }
                    UpdateTable();
                    MessageBox.Show("Данные успешно загружены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void BuildChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            chart1.ChartAreas.Add(area);

            Series series = new Series("Плотность");
            series.ChartType = SeriesChartType.Column;

            // Показываем подписи с округлением до 3 знаков
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "F3";

            foreach (var d in city.Districts)
            {
                series.Points.AddXY(d.Name, Math.Round(d.Density, 3));
            }

            chart1.Series.Add(series);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                BuildChart();
            }
        }
    }
}