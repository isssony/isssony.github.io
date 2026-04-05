using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DictionaryLibrary
{
    public class DictionaryService
    {
        private List<string> words = new List<string>();

        public IReadOnlyList<string> Words => words;

        // Загрузка словаря
        public void Load(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден");

            words = File.ReadAllLines(filePath)
                        .Select(w => w.Trim().ToLower())
                        .Where(w => !string.IsNullOrEmpty(w))
                        .Distinct()
                        .ToList();
        }

        // Добавление слова
        public bool AddWord(string word)
        {
            word = word.ToLower();

            if (words.Contains(word))
                return false;

            words.Add(word);
            return true;
        }

        // Удаление
        public bool RemoveWord(string word)
        {
            return words.Remove(word.ToLower());
        }

        // Поиск по началу строки
        public List<string> SearchByPrefix(string prefix)
        {
            var result = words
                .Where(w => w.StartsWith(prefix.ToLower()))
                .OrderBy(w => w)
                .ToList();

            if (!result.Any())
                throw new Exception("Ничего не найдено");

            return result;
        }
        public List<string> FindDoubleConsonants()
        {
            string consonants = "бвгджзйклмнпрстфхцчшщ";

            var result = new List<string>();

            foreach (var word in words)
            {
                for (int i = 0; i < word.Length - 1; i++)
                {
                    if (word[i] == word[i + 1] &&
                        consonants.Contains(word[i]))
                    {
                        result.Add(word);
                        break;
                    }
                }
            }

            if (result.Count == 0)
                throw new Exception("Слова не найдены");

            return result;
        }

        // 🔹 Левенштейн
        public List<string> FuzzySearch(string input, int maxDistance = 3)
        {
            var result = words
                .Where(w => Levenshtein(w, input) <= maxDistance)
                .ToList();

            if (!result.Any())
                throw new Exception("Нечеткий поиск не дал результатов");

            return result;
        }

        private int Levenshtein(string s1, string s2)
        {
            int[,] d = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++)
                d[i, 0] = i;

            for (int j = 0; j <= s2.Length; j++)
                d[0, j] = j;

            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[s1.Length, s2.Length];
        }

        // Сохранение
        public void Save(string path)
        {
            File.WriteAllLines(path, words);
        }

        // Сохранение результатов поиска
        public void SaveResults(string path, List<string> results)
        {
            File.WriteAllLines(path, results);
        }
    }
}