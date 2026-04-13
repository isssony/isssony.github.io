using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ZoologicalLotto
{
    public class XmlAnimalLoader
    {
        private string xmlFilePath;
        private string imageBasePath;

        public XmlAnimalLoader(string xmlPath, string imagesPath)
        {
            xmlFilePath = xmlPath;
            imageBasePath = imagesPath;
        }

        public List<Animal> LoadAnimals()
        {
            List<Animal> animals = ReadAnimalsFromFile();

            // Ensure each category has enough animals for all difficulty levels (up to 9)
            var categoryGroups = animals.GroupBy(a => (a.Category ?? string.Empty).Trim(), StringComparer.OrdinalIgnoreCase);
            bool needRecreate = false;
            foreach (var grp in categoryGroups)
            {
                if (grp.Count() < 9)
                {
                    needRecreate = true;
                    break;
                }
            }

            if (needRecreate)
            {
                // Recreate sample xml with extended set and reload
                CreateSampleXml();
                animals = ReadAnimalsFromFile();
            }

            return animals;
        }

        private List<Animal> ReadAnimalsFromFile()
        {
            List<Animal> animals = new List<Animal>();

            if (!File.Exists(xmlFilePath))
            {
                return animals;
            }

            using (XmlReader xmlReader = XmlReader.Create(xmlFilePath))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.Name == "animal" && xmlReader.NodeType == XmlNodeType.Element)
                    {
                        Animal animal = new Animal();
                        animal.Name = xmlReader.GetAttribute("name") ?? string.Empty;
                        animal.Category = xmlReader.GetAttribute("category") ?? string.Empty;

                        xmlReader.Read(); // войти внутрь animal

                        while (xmlReader.Name != "animal" && !xmlReader.EOF)
                        {
                            if (xmlReader.NodeType == XmlNodeType.Element)
                            {
                                switch (xmlReader.Name)
                                {
                                    case "image":
                                        if (xmlReader.Read() && xmlReader.NodeType == XmlNodeType.Text)
                                            animal.ImagePath = Path.Combine(imageBasePath, xmlReader.Value.Trim());
                                        break;
                                    case "habitat":
                                        if (xmlReader.Read() && xmlReader.NodeType == XmlNodeType.Text)
                                            animal.Habitat = xmlReader.Value.Trim();
                                        break;
                                    case "description":
                                        if (xmlReader.Read() && xmlReader.NodeType == XmlNodeType.Text)
                                            animal.Description = xmlReader.Value.Trim();
                                        break;
                                }
                                xmlReader.Read(); // выйти из узла
                            }
                            else
                            {
                                xmlReader.Read();
                            }
                        }
                        animals.Add(animal);
                    }
                }
            }

            return animals;
        }

        private void CreateSampleXml()
        {
            string directory = Path.GetDirectoryName(xmlFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(xmlFilePath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("animals");

                // Птицы
                WriteAnimal(writer, "Орел", "птицы", "orel.jpg", "горы и степи", "Крупная хищная птица с мощным клювом и острым зрением");
                WriteAnimal(writer, "Сова", "птицы", "sova.jpg", "леса", "Ночная птица с большими глазами и бесшумным полетом");
                WriteAnimal(writer, "Пингвин", "птицы", "pingvin.jpg", "Антарктида", "Нелетающая птица, отлично плавающая");
                WriteAnimal(writer, "Колибри", "птицы", "kolibri.jpg", "Америка", "Самая маленькая птица в мире");
                WriteAnimal(writer, "Синица", "птицы", "sinitsa.jpg", "Леса и парки", "Желтая грудка");
                // Additional birds
                WriteAnimal(writer, "Ласточка", "птицы", "lastochka.jpg", "около воды", "Маленькая проворная птица с длинными крыльями");
                WriteAnimal(writer, "Воробей", "птицы", "vorobey.jpg", "в городах и полях", "Небольшая серая птица, широко распространённая");
                WriteAnimal(writer, "Голубь", "птицы", "golub.jpg", "в городах", "Птица, часто встречающаяся в населённых пунктах");
                WriteAnimal(writer, "Журавль", "птицы", "zhuravl.jpg", "болота и луга", "Высокая птица с длинными ногами и шеей");
                WriteAnimal(writer, "Фламинго", "птицы", "flamingo.jpg", "солёные озёра", "Розовая птица, стоящая на одной ноге");
                WriteAnimal(writer, "Утка", "птицы", "utka.jpg", "вокруг воды", "Водоплавающая птица небольшого размера");

                // Млекопитающие
                WriteAnimal(writer, "Лев", "млекопитающие", "lev.jpg", "саванна", "Царь зверей, живет прайдами");
                WriteAnimal(writer, "Слон", "млекопитающие", "slon.jpg", "джунгли и саванна", "Крупнейшее наземное животное");
                WriteAnimal(writer, "Волк", "млекопитающие", "volk.jpg", "леса и степи", "Хищник, живущий стаей");
                // Additional mammals
                WriteAnimal(writer, "Тигр", "млекопитающие", "tigr.jpg", "джунгли", "Крупный полосатый хищник");
                WriteAnimal(writer, "Медведь", "млекопитающие", "medved.jpg", "леса", "Крупное всеядное животное");
                WriteAnimal(writer, "Жираф", "млекопитающие", "zhiraf.jpg", "сафари", "Высокое животное с длинной шеей");
                WriteAnimal(writer, "Кенгуру", "млекопитающие", "kenguru.jpg", "Австралия", "Млекопитающее с сумкой для детёныша");
                WriteAnimal(writer, "Заяц", "млекопитающие", "zayac.jpg", "поля и луга", "Быстрое животное с длинными ушами");
                WriteAnimal(writer, "Олень", "млекопитающие", "olen.jpg", "леса и поля", "Рога у самцов, стройное тело");

                // Рептилии
                WriteAnimal(writer, "Крокодил", "рептилии", "krokodil.jpg", "реки и болота", "Крупное пресмыкающееся с мощными челюстями");
                WriteAnimal(writer, "Ящерица", "рептилии", "yasheritsa.jpg", "пустыни и леса", "Может отбрасывать хвост для защиты");
                WriteAnimal(writer, "Змея", "рептилии", "zmeya.jpg", "разные места", "Пресмыкающееся без ног");
                // Additional reptiles
                WriteAnimal(writer, "Игуана", "рептилии", "iguana.jpg", "тропики", "Зелёная большая ящерица");
                WriteAnimal(writer, "Черепаха", "рептилии", "cherepakha.jpg", "пустыни и воды", "Пресмыкающееся с панцирем");
                WriteAnimal(writer, "Гадюка", "рептилии", "gadyuka.jpg", "леса и горы", "Ядовитая змея");
                WriteAnimal(writer, "Хамелеон", "рептилии", "hameleon.jpg", "тропики", "Может менять окраску");
                WriteAnimal(writer, "Анаконда", "рептилии", "anaconda.jpg", "реки тропиков", "Огромная водная змея");
                WriteAnimal(writer, "Агама", "рептилии", "agama.jpg", "скалы и пустыни", "Небольшая ящерица с яркой окраской");

                // Насекомые
                WriteAnimal(writer, "Бабочка", "насекомые", "babochka.jpg", "луга и сады", "Насекомое с яркими крыльями");
                WriteAnimal(writer, "Муравей", "насекомые", "muravey.jpg", "везде", "Общественное насекомое, живет колониями");
                WriteAnimal(writer, "Стрекоза", "насекомые", "strekoza.jpg", "около воды", "Быстро летающее насекомое");
                // Additional insects
                WriteAnimal(writer, "Божья коровка", "насекомые", "bozhyakorovka.jpg", "сады", "Маленькое жучок с точками");
                WriteAnimal(writer, "Пчела", "насекомые", "pchela.jpg", "сады и поля", "Жужжалка, производящая мёд");
                WriteAnimal(writer, "Комар", "насекомые", "komar.jpg", "около воды", "Кусающий мелкий кровосос");
                WriteAnimal(writer, "Моль", "насекомые", "mol.jpg", "в домах", "Ночное насекомое, портит ткани");
                WriteAnimal(writer, "Жук", "насекомые", "zhuk.jpg", "лес", "Разнообразная группа с твёрдым панцирем");
                WriteAnimal(writer, "Муха", "насекомые", "mukha.jpg", "везде", "Часто летающее насекомое");

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private void WriteAnimal(XmlWriter writer, string name, string category, string image, string habitat, string description)
        {
            writer.WriteStartElement("animal");
            writer.WriteAttributeString("name", name);
            writer.WriteAttributeString("category", category);

            writer.WriteElementString("image", image);
            writer.WriteElementString("habitat", habitat);
            writer.WriteElementString("description", description);

            writer.WriteEndElement();
        }
    }
}