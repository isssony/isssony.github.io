using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AnimalGuessGame
{
    public static class DataStore
    {
        public static string TraitsFile => "traits.txt";
        public static string AnimalsFile => "animals.txt";
        public static string ResultsFile => "results.bin";
        public static string SettingsFile => "settings.bin";

        public static List<string> LoadTraits()
        {
            if (!File.Exists(TraitsFile))
                return new List<string>
                {
                    "покрыто шерстью",
                    "есть копыта",
                    "хищник",
                    "травоядное",
                    "большие уши",
                    "есть хобот",
                    "есть клыки",
                    "питается падалью"
                };

            return new List<string>(File.ReadAllLines(TraitsFile));
        }

        public static List<AnimalRecord> LoadAnimals()
        {
            var list = new List<AnimalRecord>();

            if (!File.Exists(AnimalsFile))
                return list;

            var lines = File.ReadAllLines(AnimalsFile);
            for (int i = 0; i < lines.Length; i++)
            {
                string name = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(name)) continue;

                if (i + 1 >= lines.Length) break;
                string traitsLine = lines[++i];

                var animal = new AnimalRecord { Name = name };
                foreach (var part in traitsLine.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (int.TryParse(part, out int idx))
                        animal.Traits.Add(idx);
                }

                list.Add(animal);
            }

            return list;
        }

        public static void SaveAnimals(List<AnimalRecord> animals)
        {
            using (var sw = new StreamWriter(AnimalsFile, false))
            {
                foreach (var a in animals)
                {
                    sw.WriteLine(a.Name);
                    sw.WriteLine(string.Join(" ", a.Traits));
                }
            }
        }

        public static void SaveResults(List<GameResult> results)
        {
            using (FileStream fs = new FileStream(ResultsFile, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, results);
            }
        }

        public static List<GameResult> LoadResults()
        {
            if (!File.Exists(ResultsFile))
                return new List<GameResult>();

            using (FileStream fs = new FileStream(ResultsFile, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<GameResult>)bf.Deserialize(fs);
            }
        }

        public static void SaveSettings(GameSettings settings)
        {
            using (FileStream fs = new FileStream(SettingsFile, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, settings);
            }
        }

        public static GameSettings LoadSettings()
        {
            if (!File.Exists(SettingsFile))
                return new GameSettings();

            using (FileStream fs = new FileStream(SettingsFile, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (GameSettings)bf.Deserialize(fs);
            }
        }
    }
}