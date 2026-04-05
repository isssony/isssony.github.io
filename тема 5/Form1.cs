using DictionaryLibrary;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace rabota5
{
    public partial class Form1 : Form
    {
        private DictionaryService dictionary = new DictionaryService();
        private List<string> lastResults = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dictionary.Load(ofd.FileName);
                MessageBox.Show("Словарь загружен");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dictionary.AddWord(txtWord.Text))
                MessageBox.Show("Слово уже есть");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            dictionary.RemoveWord(txtWord.Text);
        }

        private void btnSearchPrefix_Click(object sender, EventArgs e)
        {
            try
            {
                var result = dictionary.SearchByPrefix(txtSearch.Text);
                listBox1.DataSource = result;
                lastResults = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDoubleConsonants_Click(object sender, EventArgs e)
        {
            try
            {
                var result = dictionary.FindDoubleConsonants();
                listBox1.DataSource = result;
                lastResults = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFuzzy_Click(object sender, EventArgs e)
        {
            try
            {
                var result = dictionary.FuzzySearch(txtSearch.Text);
                listBox1.DataSource = result;
                lastResults = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveResults_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                dictionary.SaveResults(sfd.FileName, lastResults);
            }
        }

        private void btnSaveDictionary_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Сохранить изменения?",
                "Подтверждение",
                MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    dictionary.Save(sfd.FileName);
                }
            }
        }
    }
}