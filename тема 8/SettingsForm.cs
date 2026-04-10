using System;
using System.Windows.Forms;

namespace AnimalGuessGame
{
    public partial class SettingsForm : Form
    {
        private NumericUpDown nudTime;
        private ComboBox cbTheme;
        private TextBox txtCharacter;
        private Button btnSave;

        public GameSettings Settings { get; private set; }

        public SettingsForm(GameSettings current)
        {
            Settings = current ?? new GameSettings();

            Text = "Настройки";
            Width = 320;
            Height = 220;
            StartPosition = FormStartPosition.CenterParent;

            Label l1 = new Label { Left = 20, Top = 20, Width = 200, Text = "Время сеанса (мин):" };
            nudTime = new NumericUpDown { Left = 20, Top = 45, Width = 250, Minimum = 1, Maximum = 60, Value = Settings.SessionTimeMinutes };

            Label l2 = new Label { Left = 20, Top = 75, Width = 200, Text = "Тема:" };
            cbTheme = new ComboBox { Left = 20, Top = 100, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cbTheme.Items.AddRange(Enum.GetNames(typeof(ColorTheme)));
            cbTheme.SelectedItem = Settings.Theme.ToString();

            Label l3 = new Label { Left = 20, Top = 130, Width = 200, Text = "Персонаж:" };
            txtCharacter = new TextBox { Left = 20, Top = 155, Width = 250, Text = Settings.CharacterName };

            btnSave = new Button { Left = 20, Top = 185, Width = 250, Text = "Сохранить" };
            btnSave.Click += BtnSave_Click;

            Controls.Add(l1);
            Controls.Add(nudTime);
            Controls.Add(l2);
            Controls.Add(cbTheme);
            Controls.Add(l3);
            Controls.Add(txtCharacter);
            Controls.Add(btnSave);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Settings.SessionTimeMinutes = (int)nudTime.Value;
            Settings.Theme = (ColorTheme)Enum.Parse(typeof(ColorTheme), cbTheme.SelectedItem.ToString());
            Settings.CharacterName = txtCharacter.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}